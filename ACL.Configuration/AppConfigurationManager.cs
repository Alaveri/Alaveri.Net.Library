using System.Text;

namespace ACL.Configuration
{
    public static class AppConfigurationManager
    {
        /// <summary>
        /// Loads a configuration from a stream.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of configuration to load.</typeparam>
        /// <param name="stream">The stream containing the configuration information.</param>
        /// <param name="format">The format of the configuration data.</param>
        /// <param name="encoding">The encoding to use during serialization.</param>
        /// <param name="xmlExtraTypes">If the format is XML, include these extra types during serialization.</param>
        /// <returns>A new configuration instance loaded from the stream, or a new empty configuration if an error occurred.</returns>
        public static TConfiguration LoadFromStream<TConfiguration>(Stream stream, AppConfigurationFormat format, Encoding? encoding = null, IEnumerable<Type>? xmlExtraTypes = null) where TConfiguration : IAppConfiguration, new()
        {
            try
            {
                encoding ??= Encoding.UTF8;
                using var memoryStream = new MemoryStream();
                {
                    stream.CopyTo(memoryStream);
                }
                var configData = memoryStream.ToArray();
                return AppConfiguration.DeserializeConfigurationData<TConfiguration>(configData, format, encoding, xmlExtraTypes) ?? new TConfiguration();
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
        /// <param name="encoding">The encoding to use during serialization.</param>
        /// <param name="xmlExtraTypes">If the format is XML, include these extra types during serialization.</param>
        /// <returns>A new configuration instance loaded from the stream, or a new empty configuration if an error occurred.</returns>
        public static async Task<TConfiguration> LoadFromStreamAsync<TConfiguration>(Stream stream, AppConfigurationFormat format, Encoding? encoding = null, IEnumerable<Type>? xmlExtraTypes = null) where TConfiguration : IAppConfiguration, new()
        {
            try
            { 
                encoding ??= Encoding.UTF8;
                using var memoryStream = new MemoryStream();
                {
                    await stream.CopyToAsync(memoryStream);
                }
                var configData = memoryStream.ToArray();
                return AppConfiguration.DeserializeConfigurationData<TConfiguration>(configData, format, encoding, xmlExtraTypes) ?? new TConfiguration();
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
        /// <param name="encoding">The encoding to use during serialization.</param>
        /// <param name="xmlExtraTypes">If the format is XML, include these extra types during serialization.</param>
        /// <returns>A new configuration instance loaded from the stream, or a new empty configuration if an error occurred.</returns>
        public static TConfiguration LoadFromFile<TConfiguration>(string filename, AppConfigurationFormat format, Encoding? encoding = null, IEnumerable<Type>? xmlExtraTypes = null) where TConfiguration : IAppConfiguration, new()
        {
            if (!File.Exists(filename))
            {
                return new TConfiguration();
            }
            using var stream = new FileStream(filename, FileMode.Open);
            return LoadFromStream<TConfiguration>(stream, format, encoding, xmlExtraTypes);
        }

        /// <summary>
        /// Loads a configuration from a file.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of configuration to load.</typeparam>
        /// <param name="filename">The name of the file containing the configuration information.</param>
        /// <param name="format">The format of the configuration data.</param>
        /// <param name="encoding">The encoding to use during serialization.</param>
        /// <param name="xmlExtraTypes">If the format is XML, include these extra types during serialization.</param>
        /// <returns>A new configuration instance loaded from the stream, or a new empty configuration if an error occurred.</returns>
        public static async Task<TConfiguration> LoadFromFileAsync<TConfiguration>(string filename, AppConfigurationFormat format, Encoding? encoding = null, IEnumerable<Type>? xmlExtraTypes = null) where TConfiguration : IAppConfiguration, new()
        {
            if (!File.Exists(filename))
            {
                return new TConfiguration();
            }
            using var stream = new FileStream(filename, FileMode.Open);
            return await LoadFromStreamAsync<TConfiguration>(stream, format, encoding, xmlExtraTypes);
        }

        /// <summary>
        /// Saves the configuration to a stream.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of configuration to save.</typeparam>
        /// <param name="stream">The stream to write.</param>
        /// <param name="config">The configuration to save.</param>
        public static void SaveToStream<TConfiguration>(Stream stream, TConfiguration config) where TConfiguration: IAppConfiguration, new()
        {
            var configData = AppConfiguration.SerializeConfigurationData(config);
            using var writer = new BinaryWriter(stream);
                writer.Write(configData);
        }

        /// <summary>
        /// Saves the configuration to a stream asynchronously.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of configuration to save.</typeparam>
        /// <param name="stream">The stream to write.</param>
        /// <param name="config">The configuration to save.</param>
        public static async Task SaveToStreamAsync<TConfiguration>(Stream stream, TConfiguration config) where TConfiguration: IAppConfiguration, new()
        {
            byte[]? configData = null;
            await Task.Run(() =>
            {
                var configData = AppConfiguration.SerializeConfigurationData(config);
            });
            await stream.WriteAsync(configData);
        }


        /// <summary>
        /// Saves the configuration to a file.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of configuration to save.</typeparam>
        /// <param name="stream">The name of the file to write.</param>
        /// <param name="config">The configuration to save.</param>
        public static void SaveToFile<TConfiguration>(string filename, TConfiguration config) where TConfiguration: IAppConfiguration, new()
        {
            using var stream = new FileStream(filename, FileMode.OpenOrCreate);
            SaveToStream(stream, config);
        }

        /// <summary>
        /// Saves the configuration to a file asynchronously.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of configuration to save.</typeparam>
        /// <param name="stream">The name of the file to write.</param>
        /// <param name="config">The configuration to save.</param>

        public static async Task SaveToFileAsync<TConfiguration>(string filename, TConfiguration config) where TConfiguration : IAppConfiguration, new()
        {
            using var stream = new FileStream(filename, FileMode.OpenOrCreate);
            await SaveToStreamAsync(stream, config);
        }
    }
}