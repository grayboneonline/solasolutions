using System;
using System.Configuration;
using System.Reflection;

namespace SOLA.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigAttribute : Attribute
    {
        public ConfigType Type { get; set; }
        public string Key { get; set; }

        public ConfigAttribute(string key, ConfigType type = ConfigType.AppSetting)
        {
            Key = key;
            Type = type;
        }

        public static void ReadConfigForType(Type t)
        {
            var staticProps = t.GetProperties();
            foreach (PropertyInfo prop in staticProps)
            {
                var attribute = prop.GetCustomAttribute<ConfigAttribute>();
                if (attribute != null)
                {
                    prop.SetValue(null,
                        attribute.Type == ConfigType.ConnectionString
                            ? ConfigurationManager.ConnectionStrings[attribute.Key].ConnectionString
                            : Convert.ChangeType(ConfigurationManager.AppSettings[attribute.Key], prop.PropertyType));
                }
            }
        }
    }

    public enum ConfigType
    {
        AppSetting = 0,
        ConnectionString = 1
    }
}
