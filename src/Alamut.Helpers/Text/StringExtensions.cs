namespace Alamut.Helpers.Text
{
    public static class StringExtensions
    {
        /// <summary>
        /// determine weather is string is null OR empty or not
        /// </summary>
        /// <param name="s"></param>
        /// <returns>true if string is null or empty</returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
    }
}
