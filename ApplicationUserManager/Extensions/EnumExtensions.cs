using System;
using System.Linq;
using System.Runtime.Serialization;

namespace UserAppService.Utility.Extensions
{
    public static class EnumExtensions
    {
        public static bool IsValidValue(this Enum e)
        {
            decimal d;
            return !decimal.TryParse(e.ToString(), out d);
        }

        /// <summary>
        /// Gets the type of the attribute of.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumVal">The enum value.</param>
        /// <returns></returns>
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        /// <summary>
        /// Returns the EnumMember value from the specified System.Runtime.Serialization Enum Types.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToName(this Enum value)
        {
            EnumMemberAttribute attribute = value.GetType()
                    .GetField(value.ToString())
                    .GetCustomAttributes(typeof(EnumMemberAttribute), false)
                    .SingleOrDefault() as EnumMemberAttribute;

            return attribute == null ? value.ToString() : attribute.Value;
        }
    }
}
