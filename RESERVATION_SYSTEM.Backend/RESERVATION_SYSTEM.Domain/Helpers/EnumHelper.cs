using System.ComponentModel;
using System.Reflection;

namespace RESERVATION_SYSTEM.Domain.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value
                .GetType()
                .GetField(value.ToString())!;

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo
                .GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes != null && attributes.Length > 0
                ? attributes[0].Description
                : value.ToString();
        }
    }
}
