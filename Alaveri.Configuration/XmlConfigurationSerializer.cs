﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;

namespace Alaveri.Configuration
{
    /// <summary>
    /// Represents a configuration serializer that uses XML serialization.
    /// </summary>
    /// <seealso cref="Alaveri.Configuration.ConfigurationSerializer" />
    public class XmlConfigurationSerializer : TextConfigurationSerializer
    {
        /// <summary>
        /// Gets the file extension for this serialization format.
        /// </summary>
        /// <value>The file extension for this serialization format.</value>
        public override string FileExtension => ".xml"; 

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlConfigurationSerializer"/> class using the specified encoding default namespace, namespaces and extra types.  
        /// Defaults to UTF8.
        /// </summary>
        /// <param name="encoding">The encoding to use while serializing the configuration data.</param>
        /// <param name="defaultNamespace">The default namespace.</param>
        /// <param name="namespaces">The namespaces to include in the XML.</param>
        /// <param name="extraTypes">The extra types to serialize.</param>
        public XmlConfigurationSerializer(Encoding? encoding = null, string? defaultNamespace = null, XmlSerializerNamespaces? namespaces = null, IEnumerable<Type>? extraTypes = null) 
            : base(encoding)           
        {
            DefaultNamespace = defaultNamespace;
            Namespaces = namespaces;
            ExtraTypes = extraTypes;
        }

        /// <summary>
        /// Gets or sets the XML namespaces to include in the XML.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public XmlSerializerNamespaces? Namespaces { get; set; }

        /// <summary>
        /// Gets or sets a type array of object types serialize.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        IEnumerable<Type>? ExtraTypes { get; }

        /// <summary>
        /// Gets or sets defualt XML namespace.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        string? DefaultNamespace { get; }

        /// <summary>
        /// Serializes a configuration into XML format.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of the configuration to serialize.</typeparam>
        /// <param name="config">The configuration to serialize.</param>
        /// <returns>a byte array containing the serialized configuration data.</returns>
        public override byte[] SerializeConfigurationData<TConfiguration>(TConfiguration config)
        {
            var serializer = new XmlSerializer(typeof(TConfiguration), null, ExtraTypes?.ToArray(), null, DefaultNamespace);
            using var stringWriter = new StringWriter();
            using var writer = new XmlTextWriter(stringWriter) { Formatting = Formatting.Indented };
            serializer.Serialize(writer, config, Namespaces);
            return Encoding.GetBytes(stringWriter.ToString());
        }

        /// <summary>
        /// Deserializes a configuration from XML format.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of the configuration to deserialize.</typeparam>
        /// <param name="configurationData">The configuration data to deserialize.</param>
        /// <returns>a deserialized Configuration object of type <typeparamref name="TConfiguration" />.</returns>
        public override TConfiguration DeserializeConfigurationData<TConfiguration>(byte[] configurationData)
        {
            var serializer = new XmlSerializer(typeof(TConfiguration), ExtraTypes?.ToArray());
            using var reader = new StringReader(Encoding.GetString(configurationData));
            return (TConfiguration)(serializer.Deserialize(reader) ?? new TConfiguration());
        }
    }
}
