using System.Globalization;

namespace Alaveri.Globalization.Development
{
    /// <summary>
    /// The data source used to read the development language.
    /// </summary>
    public class DevLanguageDataSource: LanguageDataSource
    {
        /// <summary>
        /// Translates a phrase by looking up the translation by identifier.
        /// </summary>
        /// <param name="identifier">The phrase identifier used to retrieve the translation.</param>
        /// <returns>The translation of the phrase.</returns>
        public override string GetTranslation(string identifier)
        {
            return Reader.GetTranslationByIdentifier(identifier, Culture);
        }

        /// <summary>
        /// Initializes a new object of the DevelopmentDataSource class.
        /// </summary>
        public DevLanguageDataSource() : base(new DevLanguageReader(), CultureInfo.GetCultureInfo("en-US"))
        {
        }
    }
}
