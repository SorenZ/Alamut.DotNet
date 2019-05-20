using System;
using System.Collections.Generic;


namespace Alamut.Helpers.Dictionary
{
    /// <summary>
    /// https://stackoverflow.com/a/2601501/428061
    /// </summary>
    public static class DictionaryExtensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>
        (this IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultValue)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TValue> defaultValueProvider)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value)
                ? value
                : defaultValueProvider();
        }

        /// <summary>
        /// update the source dic or insert new fields from provided information by newOne
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="newOne"></param>
        public static void Upsert<TKey, TValue>(this Dictionary<TKey, TValue> source, Dictionary<TKey, TValue> newOne)
        {
            foreach (var item in newOne)
            {
                source[item.Key] = item.Value;
            }
        }
    }
}