using System;
using System.Linq;
using Alamut.Data.Paging;

namespace Alamut.Data.Linq
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Gets the paginated data.
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="query"> The query. </param>
        /// <param name="startIndex"> Start index of the row. </param>
        /// <param name="itemCount"> Size of the page. </param>
        /// <returns> </returns>
        public static IQueryable<T> ToPage<T>(this IQueryable<T> query, int startIndex, int itemCount)
        {
            if (query == null)
                throw new ArgumentNullException("query");

            if (startIndex < 0)
                startIndex = 0;

            return query.Skip(startIndex).Take(itemCount);
        } 

        /// <summary>
        /// Creates an <see cref="IPaginated{T}" /> instance from the specified query.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">The query.</param>
        /// <param name="paginatedCriteria">The paginated criteria.</param>
        /// <returns></returns>
        public static IPaginated<T> ToPaginated<T>(this IQueryable<T> query, IPaginatedCriteria paginatedCriteria)
        {
            return new Paginated<T>(
                query.ToPage(paginatedCriteria.StartIndex, paginatedCriteria.PageSize),
                query.Count(),
                paginatedCriteria.CurrentPage,
                paginatedCriteria.PageSize);
        }

    }
}
