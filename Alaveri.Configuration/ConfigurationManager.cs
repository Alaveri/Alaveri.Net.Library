using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaveri.Configuration
{
    public static class ConfigurationManager
    {
        /// <summary>
        /// Gets the path to the configuration file.  Defaults to %appdata%\[OrganizationName]\[ or %programdata%\[OrganizationName]\ depending on ConfigurationType.
        /// </summary>
        /// <param name="type">The configuration type.</param>
        /// <param name="organizationName">Name of the organization.</param>
        /// <param name="appName">Name of the application.</param>
        /// <returns>the path to the configuration file.</returns></returnss></returns>
        public static string GetConfigurationPath(ConfigurationType type, string organizationName, string appName)
        {
            var path = type == ConfigurationType.User ?
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) :
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            return $"{Path.Combine(path, organizationName).TrimEnd(Path.DirectorySeparatorChar)}{Path.DirectorySeparatorChar}{appName}{Path.DirectorySeparatorChar}";
        }

        /// <summary>
        /// The full path and filename of the configuration file.  By default, the full path and filename is %appdata%\[Organization]\[ConfigurationName].[ConfigurationExtension], or
        /// %programdata%\[Organization]\[ConfigurationName].[ConfigurationExtension] depending on the ConfigurationType.
        /// </summary>
        public static string GetConfigurationFilename(ConfigurationType type, string organizationName, string appName, string extension, string configurationName = "")
        {
            return Path.Combine(GetConfigurationPath(type, organizationName, appName), string.IsNullOrWhiteSpace(configurationName) ? appName : configurationName) + extension;
        }

        /// <summary>
        /// Loads a configuration from a stream.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of configuration to load.</typeparam>
        /// <param name="stream">The stream containing the configuration information.</param>
        /// <param name="serializer">The serializer used to serialize the configuration data.</param>
        /// <param name="size">The size of the configuration data.</param>
        /// <returns>A new configuration instance loaded from the stream, or a new empty configuration if an error occurred.</returns>
        public static TConfiguration LoadFromStream<TConfiguration>(Stream stream, IConfigurationSerializer serializer, int size) where TConfiguration : IConfiguration, new()
        {
            try
            {
                var data = new byte[size];
                stream.Read(data, 0, size);
                return serializer.DeserializeConfigurationData<TConfiguration>(data) ?? new TConfiguration();
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
        /// <param name="serializer">The serializer used to deserialize configuration data.</param>
        /// <param name="size">The size of the configuration data.</param>
        /// <returns>A new configuration instance loaded from the stream, or a new empty configuration if an error occurred.</returns>
        public static async Task<TConfiguration> LoadFromStreamAsync<TConfiguration>(Stream stream, ConfigurationSerializer serializer, int size) where TConfiguration : IConfiguration, new()
        {
            try
            {
                var data = new byte[size];
                await stream.ReadAsync(data.AsMemory(0, size));
                return serializer.DeserializeConfigurationData<TConfiguration>(data);
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
        /// <param name="serializer">The serializer used to deserialize the configuration data.</param>
        /// <returns>A new configuration instance loaded from the stream, or a new empty configuration if an error occurred.</returns>
        public static TConfiguration LoadFromFile<TConfiguration>(string filename, ConfigurationSerializer serializer) where TConfiguration : IConfiguration, new()
        {
            if (!File.Exists(filename))
                return new TConfiguration();
            using var stream = new FileStream(filename, FileMode.Open);
            var result = LoadFromStream<TConfiguration>(stream, serializer, (int)stream.Length);
            return result;
        }

        /// <summary>
        /// Loads a configuration from a file asynchronously.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of configuration to load.</typeparam>
        /// <param name="filename">The name of the file containing the configuration information.</param>
        /// <param name="serializer">The serializer used to deserialize the configuration data.</param>
        /// <returns>A new configuration instance loaded from the stream, or a new empty configuration if an error occurred.</returns>
        public static async Task<TConfiguration> LoadFromFileAsync<TConfiguration>(string filename, ConfigurationSerializer serializer) where TConfiguration : IConfiguration, new()
        {
            if (filename == null)
                throw new ArgumentNullException(nameof(filename));

            if (!File.Exists(filename))
                return new TConfiguration();
            using var stream = new FileStream(filename, FileMode.Open);
            return await LoadFromStreamAsync<TConfiguration>(stream, serializer, (int)stream.Length);
        }

        /// <summary>
        /// Loads a configuration from a file with the default Configuration Filename asynchronously.
        /// 
        /// The configuration file name is built in the following way:
        /// 
        /// User configuration: %appdata%/[organizationName]/[appName]/[configurationName].[serializer.FileExtension].
        /// Program configuration: %programdata%/[organizationName]/[appName]/[configurationName].[serializer.FileExtension].
        /// 
        /// if configurationName is null or whitespace, [appName] is used for the file name.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of configuration to load.</typeparam>
        /// <param name="serializer">The serializer used to deserialize the configuration data.  If not provided, JSON serialization is used.</param>
        /// <param name="type">The type of configuration to load.</param>
        /// <param name="appName">The name of the application.</param>
        /// <param name="organizationName">The name of the organization.</param>
        /// <param name="configurationName">The name of the configuration file.</param>
        /// <returns>A new configuration instance loaded from the stream, or a new empty configuration if the configuration file doesn't exist.</returns>
        public static async Task<TConfiguration> LoadAsync<TConfiguration>(ConfigurationType type, string organizationName, string appName, 
            ConfigurationSerializer? serializer = null, string configurationName = "") where TConfiguration : IConfiguration, new()
        {
            serializer ??= new JsonConfigurationSerializer();
            var filename = GetConfigurationFilename(type, organizationName, appName, serializer.FileExtension, configurationName); 
            var config = await LoadFromFileAsync<TConfiguration>(filename, serializer);
            config.ConfigurationFilename = filename;
            config.Serializer = serializer;
            return config;
        }

        /// <summary>
        /// Loads a configuration from a file with the default Configuration Filename synchronously.
        /// 
        /// The configuration file name is built in the following way:
        /// 
        /// User configuration: %appdata%/[organizationName]/[appName]/[configurationName].[serializer.FileExtension].
        /// Program configuration: %programdata%/[organizationName]/[appName]/[configurationName].[serializer.FileExtension].
        /// 
        /// if configurationName is null or whitespace, [appName] is used for the file name.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of configuration to load.</typeparam>
        /// <param name="serializer">The serializer used to deserialize the configuration data.  If not provided, JSON serialization is used.</param>
        /// <param name="type">The type of configuration to load.</param>
        /// <param name="appName">The name of the application.</param>
        /// <param name="organizationName">The name of the organization.</param>
        /// <param name="configurationName">The name of the configuration file.</param>
        /// <returns>A new configuration instance loaded from the stream, or a new empty configuration if the configuration file doesn't exist.</returns>
        public static TConfiguration Load<TConfiguration>(ConfigurationType type, string organizationName, string appName,
            ConfigurationSerializer? serializer = null, string configurationName = "") where TConfiguration : IConfiguration, new()
        {
            serializer ??= new JsonConfigurationSerializer();
            var filename = GetConfigurationFilename(type, organizationName, appName, serializer.FileExtension, configurationName);
            var config = LoadFromFile<TConfiguration>(filename, serializer);
            config.ConfigurationFilename = filename;
            config.Serializer = serializer;
            return config;
        }

    }
}
