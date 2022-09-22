using Alaveri.Core.Enumerations;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Alaveri.Configuration
{
    public interface IAppConfiguration
    {
        /// <summary>
        /// The encoding used during serialization and deserialization.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        Encoding Encoding { get; }

        /// <summary>
        /// If the configuration file format is XML, these are the namespaces to include in the XML.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        XmlSerializerNamespaces XmlNamespaces { get; }

        /// <summary>
        /// If the configuration file format is XML, these extra types will be used during serialization.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        IEnumerable<Type> XmlExtraTypes { get; }

        /// <summary>
        /// If the configuration file format is XML, this is the defualt namespace.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        string XmlDefaultNamespace { get; }

        /// <summary>
        /// The name of the organization authoring the application.  By default, it is used as part of the configuration path for reading and writing the configuration (for example: an OrganizationName of 'Alaveri' would be 
        /// used in the ConfigurationPath as %appdata%\Alaveri\[ConfigurationName].[ConfigurationExtension].
        /// </summary>
        static string OrganizationName { get; set; } 

        /// <summary>
        /// The format of the configuration data.  If custom, override the TransformConfigurationData method to generate the configuration data.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        AppConfigurationFormat Format { get; }

        /// <summary>
        /// The name of the application.  By default, ConfigurationName returns the AppName.
        /// </summary>
        static string AppName { get; set; }

        /// <summary>
        /// The name of the configuration file.  By default, the ConfigurationName returns the AppName.  ConfigurationName is used as part of the configuration path for reading and writing the 
        /// configuration (for example: an ConfigurationName of 'Emerald' would be used in the ConfigurationPath as %appdata%\[Organization]\Emerald.[ConfigurationExtension].
        /// </summary>
        static string ConfigurationName { get; }

        /// <summary>
        /// The path to the configuration file.  Defaults to %appdata%\[OrganizationName]\.
        /// </summary>
        static string ConfigurationPath { get; }

        /// <summary>
        /// Saves the configuration to a stream.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of configuration to save.</typeparam>
        /// <param name="stream">The stream to write.</param>
        /// <param name="config">The configuration to save.</param>
        /// <param name="encoding">The encoding to use.</param>
        void SaveToStream(Stream stream, Encoding encoding);

        /// <summary>
        /// Saves the configuration to a stream asynchronously.
        /// </summary>
        /// <param name="stream">The stream to write.</param>
        /// <param name="encoding">The encoding to use.</param>
        Task SaveToStreamAsync(Stream stream, Encoding encoding);

        /// <summary>
        /// Saves the configuration to a file.
        /// </summary>
        /// <param name="filename">The name of the file to write.</param>
        /// <param name="encoding">The encoding to use.</param>
        void SaveToFile(string filename, Encoding encoding);

        /// <summary>
        /// Saves the configuration to a file asynchronously.
        /// </summary>
        /// <param name="stream">The name of the file to write.</param>
        /// <param name="encoding">The encoding to use.</param>
        Task SaveToFileAsync(string filename, Encoding encoding);

        /// <summary>
        /// Saves the configuration to a file using the default configuration filename.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of configuration to save.</typeparam>
        void Save();

        /// <summary>
        /// Saves the configuration to a file using the default configuration filename.
        /// </summary>
        void SaveAsync();
    }
}