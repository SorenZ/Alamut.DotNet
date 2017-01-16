
using System.Collections.Generic;

namespace Alamut.Helpers.Collection
{
    public static class DictionaryExtensions
    {
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