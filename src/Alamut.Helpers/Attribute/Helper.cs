using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Alamut.Helpers.Attribute
{
    public static class Helper
    {
        /// <summary>
        /// gets display properties by proprty name
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDisplayNameByProperites(IEnumerable<string> properties, Type type)
        {
            return type.GetMembers()
                .Where(q => q.GetCustomAttributes(typeof(DisplayNameAttribute), true).Any())
                .Join(properties, info => info.Name, s => s,
                    (info, s) => new
                    {
                        Property = s,
                        DisplayName = GetAttribute<DisplayNameAttribute>(info).DisplayName
                    })
                .ToDictionary(x => x.Property, x => x.DisplayName);

        }


        /// <summary>
        /// recommended for geting custom attribute in invocation
        /// </summary>
        /// <typeparam name="T">attribute</typeparam>
        /// <param name="memberInfo"></param>
        /// <returns>attribute information if exist, otherwise null.</returns>
        /// <see cref="http://stackoverflow.com/questions/2536675/access-custom-attribute-on-method-from-castle-windsor-interceptor"/>
        public static T GetAttribute<T>(MemberInfo memberInfo) where T : class
        {
            return memberInfo.GetCustomAttribute(typeof(T), true) as T;
            //return System.Attribute.GetCustomAttribute(memberInfo, typeof(T), true) as T;
        }

        public static T GetAttribute<T>(System.Enum enumValue) where T : System.Attribute
        {
            var memberInfo = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .FirstOrDefault();

            var attribute = (T) memberInfo?
                .GetCustomAttributes(typeof(T), false)
                .FirstOrDefault();

            return attribute;
        }

    }
}
