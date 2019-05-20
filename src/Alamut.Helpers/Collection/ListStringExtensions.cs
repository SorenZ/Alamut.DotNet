using System.Collections.Generic;

namespace Alamut.Helpers.Collection
{
    /// <summary>
    /// convert list of collection to comma seperated string
    /// </summary>
    public static class ListStringExtensions
    {
        public static string ToString<T>(this IEnumerable<T> list, string seperator = ", ")
        {
            return string.Join(seperator, list);
        }

        public static string ToString<T>(this T[] array, string seperator = ", ")
        {
            return string.Join(seperator, array);
        }
    }
}