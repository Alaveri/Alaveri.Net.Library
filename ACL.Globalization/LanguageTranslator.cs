using System.Globalization;

namespace ACL.Globalization
{
    /// <summary>
    /// Represents a base class for language translators.
    /// </summary>
    public abstract class LanguageTranslator: ILanguageTranslator
    {
        /// <summary>
        /// The culture information used during translations.
        /// </summary>
        public CultureInfo Culture => DataSource.Culture;

        /// <summary>
        /// The data source used to look up translations.
        /// </summary>
        public LanguageDataSource DataSource { get; }

        /// <summary>
        /// Translates a phrase given the specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier of the translated string.</param>
        /// <returns>the translated phrase from the specified identifier.</returns>
        public abstract string Translate(string identifier);

        /// <summary>
        /// Translates a phrase given the specified
        /// </summary>
        /// <param name="identifier">The identifier of the translated string.</param>
        /// <param name="args">Formatting arguments.</param>
        public virtual string TranslateFormat(string identifier, params object[] args)
        {
            return string.Format(DataSource.Culture, Translate(identifier), args);
        }

        /// <summary>
        /// Initializes a new instance of the LanguageTranslator class using the specified data source.
        /// </summary>
        /// <param name="dataSource">The source of the language data.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public LanguageTranslator(LanguageDataSource dataSource)
        {
            DataSource = dataSource;
        }
    }
}
