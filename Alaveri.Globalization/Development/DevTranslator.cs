namespace Alaveri.Globalization.Development
{
    /// <summary>
    /// The translator used for development strings, for example exception messages.
    /// </summary>
    public class DevTranslator : LanguageTranslator
    {
        /// <summary>
        /// Current instance of the development translator.
        /// </summary>
        public static DevTranslator Current { get; } = new DevTranslator(new DevLanguageDataSource());

        /// <summary>
        /// Translates a phrase given the specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier of the translated string.</param>
        /// <returns>the translated phrase from the specified identifier.</returns>
        public override string Translate(string identifier)
        {
            return DataSource.Reader.GetTranslationByIdentifier(identifier, Culture);
        }

        /// <summary>
        /// Initializes a new instance of the DevTranslator class using the specified data source.
        /// </summary>
        /// <param name="dataSource">The source of the language data.</param>
        public DevTranslator(LanguageDataSource dataSource) : base(dataSource)
        {
        }
    }
}
