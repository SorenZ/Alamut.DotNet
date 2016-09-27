using System;
using System.Collections.Generic;
using System.Globalization;

namespace Alamut.Utilities.Localization
{
    /// <summary>
    /// provide localization information throughout framework
    /// </summary>
    public interface ILocalizationService
    {
        /// <summary>
        /// get supported languages 
        /// en, english
        /// fa, فارسی
        /// </summary>
        /// <returns></returns>
        /// <remarks>is based on configuraion</remarks>
        Dictionary<string, string> GetSupportedLanguges();

        /// <summary>
        /// get supported culture info
        /// </summary>
        /// <returns></returns>
        /// <remarks>is based on configuraion</remarks>
        IList<CultureInfo> GetSupportedCulture();

        /// <summary>
        /// get current languase if multilanguage supported.
        /// two ISO letter
        /// </summary>
        //[Obsolete("use SetCurrentLanguage()")]
        string CurrentLanguage { get; }

        /// <summary>
        /// get default language title
        /// English, عربی, فارسی
        /// </summary>
        string CurrentLanguageTitle { get; }

        /// <summary>
        /// get default language 
        /// Two ISO letter
        /// </summary>
        /// <remarks>is based on configuraion</remarks>
        string DefaultLanguage { get; }
        

        /// <summary>
        /// determine wether system support multi-lingual feature or not.
        /// </summary>
        /// <remarks>is based on configuraion if provided otherwise return false</remarks>
        bool IsMulitLanguage { get; }
    }
}