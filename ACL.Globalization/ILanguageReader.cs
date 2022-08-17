using ACL.Data;
using System.Globalization;

namespace ACL.Globalization
{
    /// <summary>
    /// Interface to an object that reads language data from a data source.
    /// </summary>
    public interface ILanguageReader: IDataSourceReader
    {
        /// <summary>
        /// Translates a phrase by looking up the translation.
        /// </summary>
        /// <param name="identifier">The phrase identifier used to retrieve the translation.</param>
        /// <param name="culture">The culture information to use for the translation.</param>
        /// <returns>The translation of the phrase.</returns>
        string GetTranslationByIdentifier(string identifier, CultureInfo culture);
    }
}