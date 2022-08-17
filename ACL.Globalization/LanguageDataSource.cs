using ACL.Data;
using System.Globalization;

namespace ACL.Globalization
{
    /// <summary>
    /// Represents a base class for language data sources.
    /// </summary>
    public abstract class LanguageDataSource : DataSource<ILanguageReader, IDataSourceWriter>, ILanguageDataSource
    {
        /// <summary>
        /// The culture information used during translations.
        /// </summary>
        public CultureInfo Culture { get; set; }

        /// <summary>
        /// Translates a phrase by looking up the translation by identifier.
        /// </summary>
        /// <param name="identifier">The phrase identifier used to retrieve the translation.</param>
        /// <returns>The translation of the phrase.</returns>
        public abstract string GetTranslation(string identifier);

        /// <summary>
        /// Initializes a new instance of the LanguageDataSource class using the specified reader using the
        /// specified language reader.
        /// </summary>
        /// The culture information used during translations.
        /// <param name="culture">The culture used by the data source.</param>
        /// <param name="reader">The reader used to lookup translations.</param>
        public LanguageDataSource(ILanguageReader reader, CultureInfo culture) : base(reader, NullDataSourceWriter.Instance)
        {
            Culture = culture;
        }
    }
}
