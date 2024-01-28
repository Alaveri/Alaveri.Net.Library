using System.Globalization;

namespace Alaveri.Core.Extensions.Conversion
{
    /// <summary>
    /// Provides extension methods related to safe conversions.
    /// </summary>
    public static class SafeConvertExtensions
    {
        /// <summary>
        /// Converts a variable from one type to another and returns a default if the conversion is invalid, null or DBNull.
        /// </summary>
        /// <typeparam name="TResult">The type to which the value will be converted.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value of the value is invalid.</param>
        /// <param name="culture">The culture used for culture-specific formatting information during conversions.</param>
        /// <returns>The convrerted value or the default.</returns>
        public static TResult? ChangeType<TResult>(this object value, TResult defaultValue, CultureInfo culture) where TResult : IConvertible
        {
            return SafeConvert.ChangeType(value, defaultValue, culture);
        }

        /// <summary>
        /// Converts a variable from one type to another and returns a default if the conversion is invalid, null or DBNull.  Uses
        /// the current culture of the current thread for culture-specific formatting information used in conversions.
        /// </summary>
        /// <typeparam name="TResult">The type to which the value will be converted.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value of the value is invalid.</param>
        /// <returns>The converted value or the default.</returns>
        public static TResult? ChangeType<TResult>(this object value, TResult defaultValue) where TResult : IConvertible
        {
            return SafeConvert.ChangeType(value, defaultValue, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts an object from one type to another and returns a default if the conversion is invalid, null or DBNull.
        /// </summary>
        /// <typeparam name="TResult">The type to which the value will be converted.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value of the value is invalid.</param>
        /// <returns>The converted value or the default.</returns>
        public static TResult? ConvertObject<TResult>(this object value, TResult defaultValue) where TResult : IConvertible
        {
            return SafeConvert.ConvertObject(value, defaultValue);
        }

        /// <summary>
        /// Converts an object to Int16.  Returns the default value if the conversion is invalid, null, or DBNull.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The converted object or the default.</returns>
        public static short AsInt16(this object value, short defaultValue = default)
        {
            return SafeConvert.ToInt16(value, defaultValue);
        }

        /// <summary>
        /// Converts an object to Int32.  Returns the default value if the conversion is invalid, null, or DBNull.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The converted object or the default.</returns>
        public static int AsInt32(this object value, int defaultValue = default)
        {
            return SafeConvert.ToInt32(value, defaultValue);
        }

        /// <summary>
        /// Converts an object to Int64.  Returns the default value if the conversion is invalid, null, or DBNull.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The converted object or the default.</returns>
        public static long AsInt64(this object value, long defaultValue = default)
        {
            return SafeConvert.ToInt64(value, defaultValue);
        }

        /// <summary>
        /// Converts an object to UInt16.  Returns the default value if the conversion is invalid, null, or DBNull.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The converted object or the default.</returns>
        public static ushort AsUInt16(this object value, ushort defaultValue = default)
        {
            return SafeConvert.ToUInt16(value, defaultValue);
        }

        /// <summary>
        /// Converts an object to UInt32.  Returns the default value if the conversion is invalid, null, or DBNull.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The converted object or the default.</returns>
        public static uint AsUInt32(this object value, uint defaultValue = default)
        {
            return SafeConvert.ToUInt32(value, defaultValue);
        }

        /// <summary>
        /// Converts an object to UInt64.  Returns the default value if the conversion is invalid, null, or DBNull.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The converted object or the default.</returns>
        public static ulong AsUInt64(this object value, ulong defaultValue = default)
        {
            return SafeConvert.ToUInt64(value, defaultValue);
        }

        /// <summary>
        /// Converts an object to Single.  Returns the default value if the conversion is invalid, null, or DBNull.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The converted object or the default.</returns>
        public static float AsSingle(this object value, float defaultValue = default)
        {
            return SafeConvert.ToSingle(value, defaultValue);
        }

        /// <summary>
        /// Converts an object to Double.  Returns the default value if the conversion is invalid, null, or DBNull.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The converted object or the default.</returns>
        public static double AsDouble(this object value, double defaultValue = default)
        {
            return SafeConvert.ToDouble(value, defaultValue);
        }

        /// <summary>
        /// Converts an object to String.  Returns the default value if the conversion is invalid, null, or DBNull.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The converted object or the default.</returns>
        public static string AsString(this object value, string? defaultValue = default)
        {
            return SafeConvert.ToString(value, defaultValue);
        }

        /// <summary>
        /// Converts an object to DateTime.  Returns the default value if the conversion is invalid, null, or DBNull.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The converted object or the default.</returns>
        public static DateTime AsDateTime(this object value, DateTime defaultValue = default)
        {
            return SafeConvert.ToDateTime(value, defaultValue);
        }

        /// <summary>
        /// Converts an object to Boolean.  Returns the default value if the conversion is invalid, null, or DBNull.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The converted object or the default.</returns>
        public static bool AsBoolean(this object value, bool defaultValue = default)
        {
            return SafeConvert.ToBoolean(value, defaultValue);
        }

        /// <summary>
        /// Converts an object to Byte.  Returns the default value if the conversion is invalid, null, or DBNull.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The converted object or the default.</returns>
        public static byte AsByte(this object value, byte defaultValue = default)
        {
            return SafeConvert.ToByte(value, defaultValue);
        }

        /// <summary>
        /// Converts an object to Char.  Returns the default value if the conversion is invalid, null, or DBNull.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The converted object or the default.</returns>
        public static char AsChar(this object value, char defaultValue = default)
        {
            return SafeConvert.ToChar(value, defaultValue);
        }

        /// <summary>
        /// Converts an object to Decimal.  Returns the default value if the conversion is invalid, null, or DBNull.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The converted object or the default.</returns>
        public static decimal AsDecimal(this object value, decimal defaultValue = default)
        {
            return SafeConvert.ToDecimal(value, defaultValue);
        }

        /// <summary>
        /// Converts an object to SByte.  Returns the default value if the conversion is invalid, null, or DBNull.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The converted object or the default.</returns>
        public static sbyte AsSByte(this object value, sbyte defaultValue = default)
        {
            return SafeConvert.ToSByte(value, defaultValue);
        }

    }
}
