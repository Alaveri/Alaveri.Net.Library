namespace Alaveri.Core.Enumerations
{
    /// <summary>
    /// Contains metadata about a single Enum value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class EnumDescriptorAttribute : Attribute
    {
        /// <summary>
        /// A human readable description of this Enum value.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A string identifier used to match this enum value to another value, such as error or status text returned by a function.
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Additional data associated with this enum value.
        /// </summary>
        public string AdditionalData { get; set; }

        /// <summary>
        /// Initializes an instance of the EnumDescriptorAttribute class using the specified parameters.
        /// </summary>
        /// <param name="description">The human readable description of this Enum value.</param>
        /// <param name="identifier">The string identifier used to match this enum value to another value, such as error or status text returned by a function.</param>
        /// <param name="additionalData">Additional data associated with this enum value.</param>
        public EnumDescriptorAttribute(string description = "", string identifier = "", string additionalData = "")
        {
            Description = description;
            Identifier = identifier;
            AdditionalData = additionalData;
        }        
    }
}
