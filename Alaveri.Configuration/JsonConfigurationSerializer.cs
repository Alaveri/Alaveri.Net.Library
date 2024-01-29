using Newtonsoft.Json;
using System.Text;

namespace Alaveri.Configuration
{
    /// <summary>
    /// Represents a configuration serializer that uses JSON serialization.
    /// </summary>
    /// <seealso cref="Alaveri.Configuration.ConfigurationSerializer" />
    public class JsonConfigurationSerializer : TextConfigurationSerializer
    {
        /// <summary>
        /// Gets the file extension for this serialization format.
        /// </summary>
        /// <value>The file extension for this serialization format.</value>
        public override string FileExtension => ".json";


        /// <summary>
        /// Initializes a new instance of the <see cref="JsonConfigurationSerializer"/> class using the specified encoding.  Defaults to UTF8.
        /// </summary>
        /// <param name="encoding">The encoding to use when serializing the configuration data.</param>
        public JsonConfigurationSerializer(Encoding? encoding = null) : base(encoding)
        {
        }

        /// <summary>
        /// Serializes a configuration to JSON.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of the configuration to serialize.</typeparam>
        /// <param name="config">The configuration to serialize.</param>
        /// <returns>a byte array containing the serialized configuration data.</returns>
        public override byte[] SerializeConfigurationData<TConfiguration>(TConfiguration config)
        {
            return Encoding.GetBytes(JsonConvert.SerializeObject(config, Formatting.Indented)); 
        }

        /// <summary>
        /// Deserializes a configuration from JSON.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of the configuration to deserialize.</typeparam>
        /// <param name="configurationData">The configuration data to deserialize.</param>
        /// <returns>a deserialized Configuration object of type <typeparamref name="TConfiguration" />.</returns>
        public override TConfiguration DeserializeConfigurationData<TConfiguration>(byte[] configurationData)
        {
            return JsonConvert.DeserializeObject<TConfiguration>(Encoding.GetString(configurationData)) ?? new TConfiguration();
        }

    }
}
