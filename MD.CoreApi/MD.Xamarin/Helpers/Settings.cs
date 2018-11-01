using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace MD.Xamarin.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private const string CurrentUserIdKey = "userId";
        private const string UserAccessTokenKey = "access_token";

        private static readonly string SettingsDefault = string.Empty;

        #endregion

        public static string CurrentUserId
        {
            get => AppSettings.GetValueOrDefault(CurrentUserIdKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(CurrentUserIdKey, value);
        }

        public static string UserAccessToken
        {
            get => AppSettings.GetValueOrDefault(UserAccessTokenKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(UserAccessTokenKey, value);
        }

        public static string GeneralSettings
        {
            get => AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            set => AppSettings.AddOrUpdateValue(SettingsKey, value);
        }

        public static void Clear()
        {
            AppSettings.Clear();
        }
    }
}