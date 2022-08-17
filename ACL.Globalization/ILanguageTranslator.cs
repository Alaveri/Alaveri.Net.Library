using System.Globalization;

namespace ACL.Globalization
{
    /// <summary>
    /// Interface to an object that provides translations by reading from a data source.
    /// </summary>
    public interface ILanguageTranslator
    {
        /// <summary>
        /// The culture information used during translations.
        /// </summary>
        CultureInfo Culture { get; }

        /// <summary>
        /// The data source used to look up translations.
        /// </summary>
        LanguageDataSource DataSource { get; }

        /// <summary>
        /// Translates a phrase given the specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier of the translated string.</param>
        /// <returns>the translated phrase from the specified identifier.</returns>
        string Translate(string identifier);
    }
}