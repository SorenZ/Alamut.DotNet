using System.Globalization;

namespace Alamut.Helpers.Localization
{
    /// <summary>
    /// provide a culture helper to simplify access to culture feature
    /// </summary>
    public static class Language
    {
        /// <summary>
        /// is current language system folow from Right to Left 
        /// </summary>
        public static bool IsRtl => CultureInfo.CurrentCulture.TextInfo.IsRightToLeft;

        /// <summary>
        /// provide current languge Two ISO letter 
        /// en, fa, ar
        /// </summary>
        public static string Current => CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
    }
}
