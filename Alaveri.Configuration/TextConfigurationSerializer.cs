using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaveri.Configuration
{
    /// <summary>
    /// Base class for text-based serializers.
    /// </summary>
    public abstract class TextConfigurationSerializer : ConfigurationSerializer
    {
        /// <summary>
        /// Gets or sets the configuration encoding.
        /// </summary>
        /// <value>The configuration encoding.</value>
        public virtual Encoding Encoding { get; set; } = Encoding.UTF8;

        public TextConfigurationSerializer(Encoding? encoding = null) 
        { 
            encoding ??= Encoding.UTF8;
            Encoding = encoding;
        }
    }
}
