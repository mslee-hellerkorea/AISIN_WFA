using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace AISIN_WFA.Utility
{
    public class UseConfigFile
    {
        public static string GetStringConfigurationSetting(string configurationName, string defaultValue)
        {
            string configValue = null;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                configValue = appSettings[configurationName] ?? defaultValue;
                if (configValue != null)
                    return configValue;
            }
            catch
            {
            }
            return defaultValue;
        }
        public static int GetIntConfigurationSetting(string configurationName, int defaultValue)
        {
            string configValue = null;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                configValue = appSettings[configurationName] ?? defaultValue.ToString();
                if (configValue != null)
                    return Convert.ToInt32(configValue);
            }
            catch
            {
            }
            return defaultValue;
        }
        public static bool GetBoolConfigurationSetting(string configurationName, bool defaultValue)
        {
            string configValue = null;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                configValue = appSettings[configurationName] ?? defaultValue.ToString();

                if (configValue != null)
                    return Convert.ToBoolean(configValue);
            }
            catch
            {
            }
            return defaultValue;
        }
        public static void SetBoolConfigurationSetting(string configurationName, bool value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[configurationName] == null)
                {
                    settings.Add(configurationName, value.ToString());
                }
                else
                {
                    settings[configurationName].Value = value.ToString();
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch
            {
            }
        }
        public static void SetStringConfigurationSetting(string configurationName, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[configurationName] == null)
                {
                    settings.Add(configurationName, value);
                }
                else
                {
                    settings[configurationName].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch
            {
            }
        }
        public static void SetIntConfigurationSetting(string configurationName, int value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[configurationName] == null)
                {
                    settings.Add(configurationName, value.ToString());
                }
                else
                {
                    settings[configurationName].Value = value.ToString();
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch
            {
            }
        }
    }
}
