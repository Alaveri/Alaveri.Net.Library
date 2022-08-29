using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using ACL.Core.Enumerations;
using Formatting = Newtonsoft.Json.Formatting;

namespace ACL.Configuration
{
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
        public virtual XmlSerializerNamespaces? XmlNamespaces => null;

        /// <summary>
        /// If the configuration file format is XML, these extra types will be used during serialization.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public virtual IEnumerable<Type>? XmlExtraTypes => null;

        /// <summary>
        /// If the configuration file format is XML, this is the defualt namespace.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public virtual string? XmlDefaultNamespace => null;

        /// <summary>
        /// The file extension used for the configuration. For example, if the Organization name is Alaveri, the configuration filename would be %appdata%\Alaveri\[AppName].[ConfigurationExtension].
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public virtual string ConfigurationExtension => EnumHelper.GetAdditionalData(Format) ?? string.Empty;

        /// <summary>
        /// The name of the organization authoring the application.  By default, it is used as part of the configuration path for reading and writing the configuration (for example: an OrganizationName of 'Alaveri' would be 
        /// used in the ConfigurationPath as %appdata%\Alaveri\[ConfigurationName].[ConfigurationExtension].
        /// </summary>
        public abstract string OrganizationName { get; }

        /// <summary>
        /// The format of the configuration data.  If custom, override the TransformConfigurationData method to generate the configuration data.
        /// </summary>
        [JsonIgnore]
        public virtual AppConfigurationFormat Format => AppConfigurationFormat.Json;

        /// <summary>
        /// The name of the application.  By default, ConfigurationName returns the AppName.
        /// </summary>
        public abstract string AppName { get; }

        /// <summary>
        /// The name of the configuration file.  By default, the ConfigurationName returns the AppName.  ConfigurationName is used as part of the configuration path for reading and writing the 
        /// configuration (for example: an ConfigurationName of 'Emerald' would be used in the ConfigurationPath as %appdata%\[Organization]\Emerald.[ConfigurationExtension].
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public virtual string ConfigurationName => AppName;

        /// <summary>
        /// The path to the configuration file.  Defaults to %appdata%\[OrganizationName]\.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public virtual string ConfigurationPath => $"{Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), OrganizationName).TrimEnd(Path.DirectorySeparatorChar)}{Path.DirectorySeparatorChar}";

        /// <summary>
        /// The full path and filename of the configuration file.  By default, the full path and filename is %appdata%\[Organization]\[ConfigurationName].[ConfigurationExtension].
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public virtual string ConfigurationFilename => Path.Combine(ConfigurationPath, AppName, ConfigurationName) + ConfigurationExtension;

        /// <summary>
        /// If the configuration format is Custom, override this method to return the configuration data used for saving and loading the configuration.
        /// </summary>
        /// <param name="config">The configuration to serialize.</param>
        /// <typeparam name="TConfiguration">The type of the configuration to serialize.</typeparam>
        /// <returns>A byte array containing the serialized configuration data.</returns>
        public static byte[] SerializeConfigurationData<TConfiguration>(TConfiguration config) where TConfiguration : IAppConfiguration, new()
        {
            switch (config.Format)
            {
                case AppConfigurationFormat.Json: return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(config, Formatting.Indented));
                case AppConfigurationFormat.Xml:
                    {
                        var serializer = new XmlSerializer(typeof(TConfiguration), null, config.XmlExtraTypes?.ToArray(), null, config.XmlDefaultNamespace);
                        using var stringWriter = new StringWriter();
                        using var writer = new XmlTextWriter(stringWriter) { Formatting = System.Xml.Formatting.Indented };
                        serializer.Serialize(writer, config, config.XmlNamespaces);
                        return config.Encoding.GetBytes(stringWriter.ToString());
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
        public static TConfiguration? DeserializeConfigurationData<TConfiguration>(byte[] configurationData, AppConfigurationFormat format,
            Encoding? encoding = null, IEnumerable<Type>? xmlExtraTypes = null) where TConfiguration : IAppConfiguration, new()
        {
            encoding ??= Encoding.UTF8;
            switch (format)
            {
                case AppConfigurationFormat.Json: return JsonConvert.DeserializeObject<TConfiguration>(Encoding.UTF8.GetString(configurationData));
                case AppConfigurationFormat.Xml:
                    {
                        var serializer = new XmlSerializer(typeof(TConfiguration), xmlExtraTypes?.ToArray());
                        using var reader = new StringReader(encoding.GetString(configurationData));
                        return (TConfiguration?)serializer.Deserialize(reader);
                    }
                default: throw new SerializationException("Serialization format not supported.");
            };
        }
    }
}
