﻿using System.Globalization;

namespace ACL.Globalization
{
    /// <summary>
    /// Represents a base class for language data sources.
    /// </summary>
    public interface ILanguageDataSource
    {
        /// <summary>
        /// The culture information used during translations.
        /// </summary>
        CultureInfo Culture { get; set; }

        /// <summary>
        /// Translates a phrase by looking up the translation by identifier.
        /// </summary>
        /// <param name="identifier">The phrase identifier used to retrieve the translation.</param>
        /// <returns>The translation of the phrase.</returns>
        string GetTranslation(string identifier);
    }
}