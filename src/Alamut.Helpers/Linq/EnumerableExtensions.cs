using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Alamut.Helpers.Linq
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// determine whether the collection is not null AND has values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        /// <remarks>
        /// based on : http://stackoverflow.com/a/5047373/428061
        /// </remarks>
        public static bool IsAny<T>(this IEnumerable<T> list)
        {
            return list != null && list.Any();
        }

        /// <summary>
        /// sort an enumerator by list of items(ids)
        /// </summary>
        /// <typeparam name="TSource">type of object that needs to be sorted</typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="sources">an enumerator that be sorted</param>
        /// <param name="ids">list of item to show how sort enumerator</param>
        /// <param name="idSelector">select the key of item for comparing with list of ids</param>
        /// <returns>sorted source by ids</returns>
        public static IEnumerable<TSource> SortBy<TSource, TKey>(this IEnumerable<TSource> sources
            , IEnumerable<TKey> ids, Func<TSource, TKey> idSelector)
        {
            return from id in ids
                   join p in sources
                       on id equals idSelector.Invoke(p)
                   select p;
        }

        /// <summary>
        /// remove element at collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="predicate"></param>
        /// <remarks>
        /// based on : http://stackoverflow.com/a/653602/428061
        /// </remarks>
        public static void RemoveAll<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            for (var i = 0; i < collection.Count; i++)
            {
                var element = collection.ElementAt(i);
                if (predicate(element))
                {
                    collection.Remove(element);
                    i--;
                }
            }
        }

        /// <summary>
        /// Split a collection into n parts
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="size"></param>
        /// <remarks>http://stackoverflow.com/a/438208/428061</remarks>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Partition<T>
            (this IEnumerable<T> source, int size)
        {
            T[] array = null;
            int count = 0;
            foreach (T item in source)
            {
                if (array == null)
                {
                    array = new T[size];
                }
                array[count] = item;
                count++;
                if (count == size)
                {
                    yield return new ReadOnlyCollection<T>(array);
                    array = null;
                    count = 0;
                }
            }
            if (array != null)
            {
                Array.Resize(ref array, count);
                yield return new ReadOnlyCollection<T>(array);
            }
        }

        /// <summary>
        /// provide dynamic projection by list of fields
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static IEnumerable<object> DynamicSelect<TSource>(this IEnumerable<TSource> source, 
            IEnumerable<string> fields)
        {
            return source.Select(s => DynamicProjection(s, fields));
        }

        /// <summary>
        /// get properties list object(s) from the input 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="properties"></param>
        /// <returns></returns>
        static object DynamicProjection(object input, IEnumerable<string> properties)
        {
            var type = input.GetType();
            dynamic dObject = new ExpandoObject();
            var dDict = dObject as IDictionary<string, object>;

            foreach (var p in properties)
            {
                var field = type.GetField(p);
                if (field != null)
                    dDict[p] = field.GetValue(input);

                var prop = type.GetProperty(p);
                if (prop != null && prop.GetIndexParameters().Length == 0)
                    dDict[p] = prop.GetValue(input, null);
            }

            return dObject;
        }
    }
}
