using ACL.Data;
using System.Globalization;

namespace ACL.Globalization
{
    /// <summary>
    /// Represents an base class for language data source readers.
    /// </summary>
    public abstract class LanguageReader : DataSourceReader, ILanguageReader
    {
        /// <summary>
        /// The identifier used to identify the data source reader.
        /// </summary>
        public override string Id => string.Empty;

        /// <summary>
        /// Translates a phrase by looking up the translation in a data source.
        /// </summary>
        /// <param name="identifier">The phrase identifier used to retrieve the translation.</param>
        /// <param name="cultureInfo">The culture information used for the translation.</param>
        /// <returns>The translation of the phrase.</returns>
        public abstract string GetTranslationByIdentifier(string identifier, CultureInfo cultureInfo);
    }
}
