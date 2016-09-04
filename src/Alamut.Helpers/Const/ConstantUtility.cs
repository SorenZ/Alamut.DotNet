using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Alamut.Helpers.Const
{
    public static class ConstantUtility
    {
        /// <summary>
        /// provides a constants of on object
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <remarks>based on this article : http://stackoverflow.com/a/10261848/428061 </remarks>
        public static IEnumerable<FieldInfo> GetConstants(this IReflect type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly);
        }

        /// <summary>
        /// get value of const property
        /// </summary>
        /// <typeparam name="T">type of value</typeparam>
        /// <param name="source">source object</param>
        /// <param name="constantName">the name of const property</param>
        /// <returns>constant value if available otherwise default value of T </returns>
        public static T GetConstValue<T>(this object source, string constantName) 
        {
            var constantInfo = source.GetType().GetConstants()
                .FirstOrDefault(q => q.Name == constantName);

            return (constantInfo == null) ? default(T) : (T) constantInfo.GetValue(source);
        }
    }
}
