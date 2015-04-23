using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace CommerceApp
{
    public enum ScheduleType
    {
        [Description("#40FF00:MGR")]  // color: green, job type: manager    0
        MGR = 0,
        [Description("#088A85:OLB Primary")] // dark blue                   1
        olbPrimary, 
        [Description("#00FFFF:OLB Secondary")] // light blue                2
        olbSecondary, 
        [Description("#DF0101:HSF Primary")] // dark red                    3
        hsfPrimary, 
        [Description("#FE2E2E:HSF Secondary")] // light red                 4
        hsfSecondary

    }

    public static class Enums
    {
        /// Get all values
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        /// Get all the names
        public static IEnumerable<T> GetNames<T>()
        {
            return Enum.GetNames(typeof(T)).Cast<T>();
        }

        /// Get the name for the enum value
        public static string GetName<T>(T enumValue)
        {
            return Enum.GetName(typeof(T), enumValue);
        }

        /// Get the underlying value for the Enum string
        public static int GetValue<T>(string enumString)
        {
            return (int)Enum.Parse(typeof(T), enumString.Trim());
        }

        public static string GetEnumDescription<T>(string value)
        {
            Type type = typeof(T);
            var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }
            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }
    }


}