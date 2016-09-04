namespace Alamut.Helpers.Text
{
    public static class PersianTextHelper
    {
        /// <summary>
        /// Gets the Arabic format.
        /// </summary>
        /// <param name="txt"> The Text. </param>
        /// <returns> </returns>
        public static string ToArabicsFormat(this string txt)
        {
            return !string.IsNullOrEmpty(txt) ? txt.Replace("ی", "ي").Replace("ک", "ك") : txt;
        }

        /// <summary>
        /// Gets the Persian format.
        /// </summary>
        /// <param name="txt"> The Text. </param>
        /// <returns> </returns>
        public static string ToPersianFormat(this string txt)
        {
            return !string.IsNullOrEmpty(txt) ? txt.Replace("ي", "ی").Replace("ك", "ک") : txt;
        }

        /// <summary>
        /// Converts all digits in the given string to the Persian digits.
        /// </summary>
        /// <param name="source"> </param>
        /// <returns> </returns>
        public static string ConvertToPersianDigit(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return source;
            }

            var nums = new[] { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
            for (var i = 0; i <= 9; i++)
            {
                source = source.Replace(i.ToString(), nums[i]);
            }

            return source;
        }
    }
}
