using ACL.Globalization.Development;

namespace ACL.Core
{
    /// <summary>
    /// Provides fexception creation functions.
    /// </summary>
    public static class ExceptionFactory
    {
        /// <summary>
        /// Creates an ArgumentNullException with a translated message.
        /// </summary>
        /// <param name="obj">The null argument used to generate the exception message.</param>
        public static ArgumentNullException CreateArgumentNullException(object? obj)
        {
            return new ArgumentNullException(DevTranslator.Current.TranslateFormat("ArgumentNullMessage", obj?.GetType().Name ?? string.Empty));
        }

        /// <summary>
        /// Creates an InvalidOperationException with a translated message.
        /// </summary>
        /// <param name="obj">The null argument used to generate the exception message.</param>
        public static InvalidOperationException CreateInvalidOperationException(object? obj)
        {
            return new InvalidOperationException(DevTranslator.Current.TranslateFormat("InvalidOperationException", obj?.GetType()?.Name ?? string.Empty));
        }
    }
}
