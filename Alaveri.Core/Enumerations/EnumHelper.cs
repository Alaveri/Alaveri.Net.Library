using Alaveri.Core.Extensions.Conversion;
using System.Runtime.CompilerServices;

namespace Alaveri.Core.Enumerations
{
    /// <summary>
    /// Provides functions for working with Enums.
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Retreieves the attribute of the specified type and enum value.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute to find.</typeparam>
        /// <param name="value">The enum value to find.</param>
        /// <returns>The attribute of the specified type and enum value, or null if none found.</returns>
        public static TAttribute? GetEnumAttribute<TAttribute>(Enum value) where TAttribute : Attribute
        {
            var type = value.GetType();
            var info = type.GetMember(value.ToString());
            var attributes = info.FirstOrDefault()?.GetCustomAttributes(typeof(TAttribute), false) ?? null;
            if (attributes == null)
                return null;
            return (attributes.Length > 0) ? (TAttribute)attributes[0] : null;
        }

        /// <summary>
        /// Returns the enum descriptor attribute of an enum value, if it exists.
        /// </summary>
        /// <param name="value">The enumeration value to find.</param>
        /// <returns>The EnumDescriptor attribute of the specified enum value, or null if none found.</returns>
        public static EnumDescriptorAttribute? GetDescriptor(Enum value)
        {
            return GetEnumAttribute<EnumDescriptorAttribute>(value);
        }

        /// <summary>
        /// If an Enum is decorated with an EnumDescriptor attribute, this will return the value of the Identifier.
        /// </summary>
        /// <param name="enumValue">The enum value to use.</param>
        /// <returns>The value of the Identifier or null if not found.</returns>
        public static string? GetIdentifier(Enum enumValue)
        {
            return GetEnumAttribute<EnumDescriptorAttribute>(enumValue)?.Identifier;
        }

        /// <summary>
        /// If an Enum is decorated with an EnumDescriptor attribute, this will return the value of the Description.
        /// </summary>
        /// <param name="enumValue">The enum value to use.</param>
        /// <returns>The value of the Description or null if not found.</returns>
        public static string? GetDescription(Enum enumValue)
        {
            return GetEnumAttribute<EnumDescriptorAttribute>(enumValue)?.Description;
        }

        /// <summary>
        /// If an Enum is decorated with an EnumDescriptor attribute, this will return the value of the AdditionalData.
        /// </summary>
        /// <param name="enumValue">The enum value to use.</param>
        /// <returns>The value of the AdditionalData or null if not found.</returns>
        public static string? GetAdditionalData(Enum enumValue)
        {
            return GetEnumAttribute<EnumDescriptorAttribute>(enumValue)?.AdditionalData;
        }

        /// <summary>
        /// Retrives an enum value with an EnumDecriptor attribute by matching identifier.
        /// </summary>
        /// <typeparam name="TEnum">The type of enum to search.</typeparam>
        /// <param name="identifier">The identifier to find.</param>
        /// <returns>The enum value matching the identifier, or null if not found.</returns>
        public static TEnum? GetEnumValueByIdentifier<TEnum>(string identifier) where TEnum : struct, Enum
        {
            return typeof(TEnum).GetEnumValues()
                .Cast<TEnum>()
                .FirstOrDefault(value => GetEnumAttribute<EnumDescriptorAttribute>(value)?.Identifier == identifier);
        }

        /// <summary>
        /// Gets the values identifiers of an enum type where the enum values are decorated with an EnumDescriptor attribute as a Dictionary.   
        /// </summary>
        /// <typeparam name="TEnum">The enumerable type.</typeparam>
        /// <returns>The identifier and value combinations of the enum.</returns>
        public static IDictionary<TEnum, string?>? GetValuesAndIdentifiers<TEnum>() where TEnum : struct, Enum
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .ToDictionary(value => value, value => GetIdentifier(value));
        }

        /// <summary>
        /// Returns true if the specified enum has the specified attribute.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute.</typeparam>
        /// <param name="enumValue">The value of the enum to check.</param>
        /// <returns>true if the specified enum has the specified attribute</returns>
        public static bool HasAttribute<TAttribute>(Enum enumValue) where TAttribute: Attribute
        {
            return GetEnumAttribute<TAttribute>(enumValue) != null;
        }            
    }
}
