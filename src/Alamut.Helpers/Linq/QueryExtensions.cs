using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Alamut.Helpers.Linq
{
    /// <summary>
    /// IQueryable Helpers
    /// </summary>
    public static class QueryExtensions
    {
        /// <summary>
        /// 'Select' by condition before query executed in provider.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="condition">the condition</param>
        /// <param name="trueExpression">The expression that will apply if <see cref="condition"/> is true.</param>
        /// <param name="falseExpression">The expression that will apply if <see cref="condition"/> is false.</param>
        /// <returns>
        /// filtered query
        /// </returns>
        public static IQueryable<TResult> SelectIf<TSource, TResult>(
            this IQueryable<TSource> source,
            bool condition,
            Expression<Func<TSource, TResult>> trueExpression,
            Expression<Func<TSource, TResult>> falseExpression)
        {
            return condition ? source.Select(trueExpression) : source.Select(falseExpression);
        }

        /// <summary>
        /// Conditional 'Where', decide about predicate before query executed in provider.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="condition">the condition</param>
        /// <param name="trueExpression">The expression that will apply if <see cref="condition"/> is true.</param>
        /// <param name="falseExpression">The expression that will apply if <see cref="condition"/> is false.</param>
        /// <returns>
        /// filtered query
        /// </returns>
        public static IQueryable<T> WhereIf<T>(
            this IQueryable<T> source,
            bool condition,
            Expression<Func<T, bool>> trueExpression,
            Expression<Func<T, bool>> falseExpression)
        {
            return condition ? source.Where(trueExpression) : source.Where(falseExpression);
        }

        /// <summary>
        /// conditional 'where', execute expressioin when condition is true
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <param name="trueExpression"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source,
            bool condition, Expression<Func<T, bool>> trueExpression)
        {
            return condition ? source.Where(trueExpression) : source;
        }

        
    }
}
