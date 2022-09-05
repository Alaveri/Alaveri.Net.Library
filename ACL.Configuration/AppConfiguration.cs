using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using ACL.Core.Enumerations;
using Formatting = Newtonsoft.Json.Formatting;
using System.IO;

namespace ACL.Configuration;

/// <summary>
/// The serialization format for the Application Configurations.
/// </summary>
public enum AppConfigurationFormat
{
    /// <summary>
    /// JSON Format.
    /// </summary>
    [EnumDescriptor(Description = "JavaScript Object Notation", Identifier = "json", AdditionalData = ".json")]
    Json,
    /// <summary>
    /// XML Format.
    /// </summary>
    [EnumDescriptor(Description = "eXtensible Markup Language", Identifier = "xml", AdditionalData = ".xml")]
    Xml,
    /// <summary>
    /// Custom format.
    /// </summary>
    [EnumDescriptor(Description = "Custom Format", Identifier = "custom", AdditionalData = ".bin")]
    Custom
}

/// <summary>
/// Represents a base class for holding and persisting application configuration information.
/// </summary>
public abstract class AppConfiguration : IAppConfiguration
{
    /// <summary>
    /// The encoding used during serialization and deserialization.
    /// </summary>
    [JsonIgnore]
    [XmlIgnore]
    public virtual Encoding Encoding => Encoding.UTF8;

    /// <summary>
    /// If the configuration file format is XML, these are the namespaces to include in the XML.
    /// </summary>
    [JsonIgnore]
    [XmlIgnore]
    public virtual XmlSerializerNamespaces XmlNamespaces => null;

    /// <summary>
    /// If the configuration file format is XML, these extra types will be used during serialization.
    /// </summary>
    [JsonIgnore]
    [XmlIgnore]
    public virtual IEnumerable<Type> XmlExtraTypes => null;

    /// <summary>
    /// If the configuration file format is XML, this is the defualt namespace.
    /// </summary>
    [JsonIgnore]
    [XmlIgnore]
    public virtual string XmlDefaultNamespace => null;

    /// <summary>
    /// The file extension used for the configuration. For example, if the Organization name is Alaveri, the configuration filename would be %appdata%\Alaveri\[AppName].[ConfigurationExtension].
    /// </summary>
    public static string ConfigurationExtension(AppConfigurationFormat format) => EnumHelper.GetAdditionalData(format) ?? string.Empty;

    /// <summary>
    /// The name of the organization authoring the application.  By default, it is used as part of the configuration path for reading and writing the configuration (for example: an OrganizationName of 'Alaveri' would be 
    /// used in the ConfigurationPath as %appdata%\Alaveri\[ConfigurationName].[ConfigurationExtension].
    /// </summary>
    public static string OrganizationName { get; set;  } = string.Empty;

    /// <summary>
    /// The format of the configuration data.  If custom, override the TransformConfigurationData method to generate the configuration data.
    /// </summary>
    [JsonIgnore]
    [XmlIgnore]
    public virtual AppConfigurationFormat Format => AppConfigurationFormat.Json;

    /// <summary>
    /// The name of the application.  By default, ConfigurationName returns the AppName.
    /// </summary>
    public static string AppName { get; set; } = string.Empty;

    /// <summary>
    /// The name of the configuration file.  By default, the ConfigurationName returns the AppName.  ConfigurationName is used as part of the configuration path for reading and writing the 
    /// configuration (for example: an ConfigurationName of 'Emerald' would be used in the ConfigurationPath as %appdata%\[Organization]\Emerald.[ConfigurationExtension].
    /// </summary>
    public static string ConfigurationName => AppName;

    /// <summary>
    /// The path to the configuration file.  Defaults to %appdata%\[OrganizationName]\.
    /// </summary>
    public static string ConfigurationPath => $"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), OrganizationName).TrimEnd(Path.DirectorySeparatorChar)}{Path.DirectorySeparatorChar}";

    /// <summary>
    /// The full path and filename of the configuration file.  By default, the full path and filename is %appdata%\[Organization]\[ConfigurationName].[ConfigurationExtension].
    /// </summary>
    public static string ConfigurationFilename(AppConfigurationFormat format) => Path.Combine(ConfigurationPath, AppName, ConfigurationName) + ConfigurationExtension(format);

    /// <summary>
    /// Loads a configuration from a stream.
    /// </summary>
    /// <typeparam name="TConfiguration">The type of configuration to load.</typeparam>
    /// <param name="stream">The stream containing the configuration information.</param>
    /// <param name="format">The format of the configuration data.</param>
    /// <param name="encoding">The encoding to use.</param>
    /// <param name="xmlExtraTypes">If the format is XML, include these extra types during serialization.</param>
    /// <returns>A new configuration instance loaded from the stream, or a new empty configuration if an error occurred.</returns>
    public static TConfiguration LoadFromStream<TConfiguration>(Stream stream, AppConfigurationFormat format, Encoding encoding = null, IEnumerable<Type> xmlExtraTypes = null) where TConfiguration : IAppConfiguration, new()
    {
        try
        {
            encoding ??= Encoding.UTF8;
            using var reader = new StreamReader(stream, encoding); 
            return DeserializeConfigurationData<TConfiguration>(reader.ReadToEnd(), format, encoding, xmlExtraTypes) ?? new TConfiguration();
        }
        catch
        {
            return new TConfiguration();
        }
    }

    /// <summary>
    /// Loads a configuration from a stream asynchronously.
    /// </summary>
    /// <typeparam name="TConfiguration">The type of configuration to load.</typeparam>
    /// <param name="stream">The stream containing the configuration information.</param>
    /// <param name="format">The format of the configuration data.</param>
    /// <param name="encoding">The encoding to use.</param>
    /// <param name="xmlExtraTypes">If the format is XML, include these extra types during serialization.</param>
    /// <returns>A new configuration instance loaded from the stream, or a new empty configuration if an error occurred.</returns>
    public static async Task<TConfiguration> LoadFromStreamAsync<TConfiguration>(Stream stream, AppConfigurationFormat format, Encoding encoding = null, IEnumerable<Type> xmlExtraTypes = null) where TConfiguration : IAppConfiguration, new()
    {
        try
        {
            encoding ??= Encoding.UTF8;
            using var reader = new StreamReader(stream, encoding);
            return DeserializeConfigurationData<TConfiguration>(await reader.ReadToEndAsync(), format, encoding, xmlExtraTypes) ?? new TConfiguration();

        }
        catch
        {
            return new TConfiguration();
        }
    }

    /// <summary>
    /// Loads a configuration from a file.
    /// </summary>
    /// <typeparam name="TConfiguration">The type of configuration to load.</typeparam>
    /// <param name="filename">The name of the file containing the configuration information.</param>
    /// <param name="format">The format of the configuration data.</param>
    /// <param name="encoding">The encoding to use.</param>
    /// <param name="xmlExtraTypes">If the format is XML, include these extra types during serialization.</param>
    /// <returns>A new configuration instance loaded from the stream, or a new empty configuration if an error occurred.</returns>
    public static TConfiguration LoadFromFile<TConfiguration>(string filename, AppConfigurationFormat format, Encoding encoding = null, IEnumerable<Type> xmlExtraTypes = null) where TConfiguration : IAppConfiguration, new()
    {
        if (!File.Exists(filename))
        {
            return new TConfiguration();
        }
        using var stream = new FileStream(filename, FileMode.Open);
        var result = LoadFromStream<TConfiguration>(stream, format, encoding, xmlExtraTypes);
        return result;
    }

    /// <summary>
    /// Loads a configuration from a file asynchronously.
    /// </summary>
    /// <typeparam name="TConfiguration">The type of configuration to load.</typeparam>
    /// <param name="filename">The name of the file containing the configuration information.</param>
    /// <param name="format">The format of the configuration data.</param>
    /// <param name="encoding">The encoding to use.</param>
    /// <param name="xmlExtraTypes">If the format is XML, include these extra types during serialization.</param>
    /// <returns>A new configuration instance loaded from the stream, or a new empty configuration if an error occurred.</returns>
    public static async Task<TConfiguration> LoadFromFileAsync<TConfiguration>(string filename, AppConfigurationFormat format, Encoding encoding = null, IEnumerable<Type> xmlExtraTypes = null) where TConfiguration : IAppConfiguration, new()
    {
        if (filename == null)
            throw new ArgumentNullException(nameof(filename));

        if (!File.Exists(filename))
            return new TConfiguration();
        using var stream = new FileStream(filename, FileMode.Open);
        return await LoadFromStreamAsync<TConfiguration>(stream, format, encoding, xmlExtraTypes);
    }

    /// <summary>
    /// Loads a configuration from a file with the default Configuration Filename asynchronously.
    /// </summary>
    /// <typeparam name="TConfiguration">The type of configuration to load.</typeparam>
    /// <param name="format">The format of the configuration data.</param>
    /// <param name="encoding">The encoding to use during serialization.</param>
    /// <param name="xmlExtraTypes">If the format is XML, include these extra types during serialization.</param>
    /// <returns>A new configuration instance loaded from the stream, or a new empty configuration if an error occurred.</returns>
    public static async Task<TConfiguration> LoadAsync<TConfiguration>(AppConfigurationFormat format, Encoding encoding = null, IEnumerable<Type> xmlExtraTypes = null) where TConfiguration : IAppConfiguration, new()
    {
        return await LoadFromFileAsync<TConfiguration>(ConfigurationFilename(format), format, encoding, xmlExtraTypes);
    }

    /// <summary>
    /// Loads a configuration from a file with the default Configuration Filename.
    /// </summary>
    /// <typeparam name="TConfiguration">The type of configuration to load.</typeparam>
    /// <param name="format">The format of the configuration data.</param>
    /// <param name="encoding">The encoding to use.</param>
    /// <param name="xmlExtraTypes">If the format is XML, include these extra types during serialization.</param>
    /// <returns>A new configuration instance loaded from the stream, or a new empty configuration if an error occurred.</returns>
    public static TConfiguration Load<TConfiguration>(AppConfigurationFormat format, Encoding encoding = null, IEnumerable<Type> xmlExtraTypes = null) where TConfiguration : IAppConfiguration, new()
    {
        return LoadFromFile<TConfiguration>(ConfigurationFilename(format), format, encoding, xmlExtraTypes);
    }

    /// <summary>
    /// Saves the configuration to a stream.
    /// </summary>
    /// <typeparam name="TConfiguration">The type of configuration to save.</typeparam>
    /// <param name="stream">The stream to write.</param>
    /// <param name="encoding">The encoding to use.</param>
    /// <param name="config">The configuration to save.</param>
    public void SaveToStream(Stream stream, Encoding encoding)
    {
        var configData = SerializeConfigurationData(this);
        using var writer = new StreamWriter(stream);
        writer.Write(configData);
    }

    /// <summary>
    /// Saves the configuration to a stream asynchronously.
    /// </summary>
    /// <param name="stream">The stream to write.</param>
    /// <param name="encoding">The encoding to use.</param>
    public async Task SaveToStreamAsync(Stream stream, Encoding encoding)
    {
        var configData = SerializeConfigurationData(this);
        using var writer = new StreamWriter(stream, encoding);
        await writer.WriteAsync(configData);
    }


    /// <summary>
    /// Saves the configuration to a file.
    /// </summary>
    /// <param name="filename">The name of the file to write.</param>
    /// <param name="encoding">The encoding to use.</param>
    public void SaveToFile(string filename, Encoding encoding)
    {
        var dir = Path.GetDirectoryName(filename) + Path.DirectorySeparatorChar;
        if (!Directory.Exists(dir))
           Directory.CreateDirectory(dir);
        using var stream = new FileStream(filename, FileMode.Create, FileAccess.Write);
        SaveToStream(stream, encoding);
    }

    /// <summary>
    /// Saves the configuration to a file asynchronously.
    /// </summary>
    /// <param name="stream">The name of the file to write.</param>
    public async Task SaveToFileAsync(string filename, Encoding encoding)
    {
        var dir = Path.GetDirectoryName(filename);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
        using var stream = new FileStream(filename, FileMode.Create, FileAccess.Write);
        await SaveToStreamAsync(stream, encoding);
    }

    /// <summary>
    /// Saves the configuration to a file using the default configuration filename.
    /// </summary>
    /// <typeparam name="TConfiguration">The type of configuration to save.</typeparam>
    public void Save()
    {
        SaveToFile(ConfigurationFilename(Format), Encoding.UTF8);
    }

    /// <summary>
    /// Saves the configuration to a file using the default configuration filename.
    /// </summary>
    public void SaveAsync()
    {
        SaveToFile(ConfigurationFilename(Format), Encoding.UTF8);
    }

    /// <summary>
    /// If the configuration format is Custom, override this method to return the configuration data used for saving and loading the configuration.
    /// </summary>
    /// <param name="config">The configuration to serialize.</param>
    /// <typeparam name="TConfiguration">The type of the configuration to serialize.</typeparam>
    /// <returns>A byte array containing the serialized configuration data.</returns>
    public static string SerializeConfigurationData<TConfiguration>(TConfiguration config) where TConfiguration : IAppConfiguration
    {
        switch (config.Format)
        {
            case AppConfigurationFormat.Json:
                return JsonConvert.SerializeObject(config, Formatting.Indented);
            case AppConfigurationFormat.Xml:
                {
                    var serializer = new XmlSerializer(typeof(TConfiguration), null, config.XmlExtraTypes?.ToArray(), null, config.XmlDefaultNamespace);
                    using var stringWriter = new StringWriter();
                    using var writer = new XmlTextWriter(stringWriter) { Formatting = System.Xml.Formatting.Indented };
                    serializer.Serialize(writer, config, config.XmlNamespaces);
                    return stringWriter.ToString();
                }
            default: throw new SerializationException("Serialization format not supported.");
        }
    }

    /// <summary>
    /// If the configuration format is Custom, override this method to return a new configuration deserialized from the specified configurationData.
    /// </summary>
    /// <typeparam name="TConfiguration">The type of the configuration to deserialize.</typeparam>
    /// <param name="format">The format of the configuration data.</param>
    /// <param name="configurationData">The configuration data to deserialize.</param>
    /// <param name="encoding">The encoding to use during serialization.</param>
    /// <param name="xmlExtraTypes">If the format is XML, include these extra types during serialization.</param>
    /// <returns>A deserialized Configuration object of type <typeparamref name="TConfiguration"/>.</returns>
    public static TConfiguration DeserializeConfigurationData<TConfiguration>(string configurationData, AppConfigurationFormat format,
        Encoding encoding = null, IEnumerable<Type> xmlExtraTypes = null) where TConfiguration : IAppConfiguration, new()
    {
        encoding ??= Encoding.Unicode;
        switch (format)
        {
            case AppConfigurationFormat.Json: return JsonConvert.DeserializeObject<TConfiguration>(configurationData);
            case AppConfigurationFormat.Xml:
                {
                    var serializer = new XmlSerializer(typeof(TConfiguration), xmlExtraTypes?.ToArray());
                    using var reader = new StringReader(configurationData);
                    return (TConfiguration)serializer.Deserialize(reader);
                }
            default: throw new SerializationException("Serialization format not supported.");
       };
    }
}

