using System.Globalization;

namespace ACL.Globalization.Development
{
    /// <summary>
    /// Represents an object that reads from a resource file.
    /// </summary>
    public class DevLanguageReader : LanguageReader
    {
        /// <summary>
        /// The identifier used to identify the data source reader.
        /// </summary>
        public override string Id => nameof(DevLanguageReader);

        /// <summary>
        /// Translates a phrase by looking up the translation in the resource file.
        /// </summary>
        /// <param name="identifier">The phrase identifier used to retrieve the translation.</param>
        /// <param name="culture">The culture information used for the translation.</param>
        /// <returns>The translation of the phrase.</returns>
        public override string GetTranslationByIdentifier(string identifier, CultureInfo culture)
        {
            return Properties.AmericanEnglish.ResourceManager?.GetString(identifier, culture) ?? string.Empty;
        }
    }
}
