using System;
using System.Linq.Expressions;

namespace Alamut.Helpers.Linq
{
    public static class LambdaExpressions
    {
        /// <summary>
        /// Gets property name of an expression
        /// </summary>
        /// <typeparam name="TSource">the source type to extract property name</typeparam>
        /// <typeparam name="TField">the field type of the expected property</typeparam>
        /// <param name="field">the expression to extract property name</param>
        /// <returns>indicated property name</returns>
        /// <remarks>based on : </remarks>
        public static string GetName<TSource, TField>(Expression<Func<TSource, TField>> field)
        {
            return (field.Body as MemberExpression ??
                 ((UnaryExpression)field.Body).Operand as MemberExpression).Member.Name;
        }

        public static string GetName<TSource>(Expression<Func<TSource, object>> field)
        {
            return (field.Body as MemberExpression ??
                 ((UnaryExpression)field.Body).Operand as MemberExpression).Member.Name;
        }
    }
}
