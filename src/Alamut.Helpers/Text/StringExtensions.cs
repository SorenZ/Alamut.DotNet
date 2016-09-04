using System;

namespace Alamut.Helpers.Text
{
    public static class StringExtensions
    {
        /// <summary>
        /// determine weather is string is null OR empty or not
        /// </summary>
        /// <param name="s"></param>
        /// <returns>true if string is null or empty</returns>
        public static bool IsNullOrEmpty(this String s)
        {
            return String.IsNullOrEmpty(s);
        }
    }
}
