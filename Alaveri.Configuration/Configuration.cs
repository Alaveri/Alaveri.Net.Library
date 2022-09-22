using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Alaveri.Core.Enumerations;
using Formatting = Newtonsoft.Json.Formatting;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System;

namespace Alaveri.Configuration;

/// <summary>
/// The type of configuration.  Determines where the configuration will be stored, either in %programdata% or %appdata%.
/// </summary>
public enum ConfigurationType
{
    /// <summary>
    /// Store the configuration in the %programdata% folder.
    /// </summary>
    Application,

    /// <summary>
    /// Store the configuration in the user's %appdata% folder.
    /// </summary>
    User
}

public delegate string GetConfigurationNameFunc();

/// <summary>
/// Represents a base class for holding and persisting application or user configuration information.
/// </summary>
public abstract class Configuration : IConfiguration
{
    /// <summary>
    /// Gets or sets the configuration filename.
    /// </summary>
    [JsonIgnore]
    [XmlIgnore]
    public string ConfigurationFilename { get; set; }

    /// <summary>
    /// Gets or sets the serializer used to serialize the configuration.  Defaults to JSON with UTF8 encoding.
    /// </summary>The serializer.</value>
    [JsonIgnore]
    [XmlIgnore]
    public IConfigurationSerializer Serializer { get; set; } = new JsonConfigurationSerializer();

    /// <summary>
    /// The type of configuration.  Determines where the configuration will be stored, either in %programdata% or %appdata%.
    /// </summary>
    [JsonIgnore]
    [XmlIgnore]
    public virtual ConfigurationType ConfigurationType { get; set; } = ConfigurationType.User;

    /// <summary>
    /// Saves the configuration to a stream.
    /// </summary>
    /// <typeparam name="TConfiguration">The type of configuration to save.</typeparam>
    /// <param name="stream">The stream to write.</param>
    public void SaveToStream(Stream stream)
    {
        var configData = Serializer.SerializeConfigurationData(this);
        stream.Write(configData);
    }

    /// <summary>
    /// Saves the configuration to a stream asynchronously.
    /// </summary>
    /// <param name="stream">The stream to write.</param>
    public async Task SaveToStreamAsync(Stream stream)
    {
        var configData = Serializer.SerializeConfigurationData(this);
        await stream.WriteAsync(configData);
    }


    /// <summary>
    /// Saves the configuration to a file.
    /// </summary>
    /// <param name="filename">The name of the file to write.</param>
    /// <exception cref="ArgumentNullException"
    public void SaveToFile(string filename)
    {
        if (filename is null)
            throw new ArgumentNullException(nameof(filename));

        var dir = Path.GetDirectoryName(filename) + Path.DirectorySeparatorChar;
        if (!Directory.Exists(dir))
           Directory.CreateDirectory(dir);
        using var stream = new FileStream(filename, FileMode.Create, FileAccess.Write);
        SaveToStream(stream);
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
        await SaveToStreamAsync(stream);
    }

    /// <summary>
    /// Saves the configuration to a file using the default configuration filename.
    /// </summary>
    /// <typeparam name="TConfiguration">The type of configuration to save.</typeparam>
    public void Save()
    {
        SaveToFile(ConfigurationFilename);
    }

    /// <summary>
    /// Saves the configuration to a file using the default configuration filename.
    /// </summary>
    public void SaveAsync()
    {
        SaveToFile(ConfigurationFilename);
    }
}

