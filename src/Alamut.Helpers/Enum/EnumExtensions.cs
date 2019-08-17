using System.ComponentModel;

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

        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        /// <example DescriptionAttribute="().Description;">string desc = myEnumVariable.GetAttributeOfType</example>
        public static T GetAttribute<T>(this System.Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            
            var memInfo = type.GetMember(enumVal.ToString());
            
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            
            return (attributes.Length > 0)
                ? (T)attributes[0]
                : null;
        }
    }
}
