using System.ComponentModel;
using System.Reflection;

namespace Alamut.Helpers.Enum
{
    public static class EnumExtensions
    {
        /// <summary>
        /// provide the value of Description of an enum type
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(this System.Enum enumValue)
        {
            var fi = enumValue.GetType().GetField(enumValue.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : enumValue.ToString();
        }

        /// <summary>
        /// parse enum type by string name
        /// </summary>
        /// <typeparam name="T">the enum type</typeparam>
        /// <param name="enumString">the enum name</param>
        /// <returns>enum item</returns>
        public static T ToEnum<T>(this string enumString)
        {
            return (T)System.Enum.Parse(typeof(T), enumString);
        }
    }
}
