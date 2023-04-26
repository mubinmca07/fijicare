using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Xamarin.Essentials;

namespace Fijicare.Helper
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        #endregion

        private static string _Authorization;
        public static string Authorization
        {
            get
            {
                var myValue = Preferences.Get("Authorization", string.Empty);
                return myValue;
            }
            set
            {
                bool hasKey = Preferences.ContainsKey("Authorization");
                Preferences.Set("Authorization", value);

            }
        }

        public static long Expired_in
        {
            get
            {
                var keyValue = AppSettings.GetValueOrDefault(nameof(Expired_in), string.Empty);
                string val = string.IsNullOrEmpty(keyValue) ? "0" : keyValue;
                return Convert.ToInt64(val);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(Expired_in), value.ToString());
            }
        }

        public static string UserServiceAPI
        {
            get
            {
                var keyValue = AppSettings.GetValueOrDefault(nameof(UserServiceAPI), string.Empty);
                return keyValue.IsEmpty() ? null : keyValue;
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(UserServiceAPI), value);
            }
        }

        public static string AuthAPI
        {
            get
            {
                var keyValue = AppSettings.GetValueOrDefault(nameof(AuthAPI), string.Empty);
                return keyValue.IsEmpty() ? null : keyValue;
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(AuthAPI), value);
            }
        }

        public static string latttude
        {
            get
            {
                var keyValue = AppSettings.GetValueOrDefault(nameof(latttude), string.Empty);
                return keyValue.IsEmpty() ? null : keyValue;
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(latttude), value);
            }
        }
        public static string longitude
        {
            get
            {
                var keyValue = AppSettings.GetValueOrDefault(nameof(longitude), string.Empty);
                return keyValue.IsEmpty() ? null : keyValue;
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(longitude), value);
            }
        }
        public static string placeName
        {
            get
            {
                var keyValue = AppSettings.GetValueOrDefault(nameof(placeName), string.Empty);
                return keyValue.IsEmpty() ? null : keyValue;
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(placeName), value);
            }
        }

        public static string HomeWorthPlaceName
        {
            get
            {
                var keyValue = AppSettings.GetValueOrDefault(nameof(HomeWorthPlaceName), string.Empty);
                return keyValue.IsEmpty() ? null : keyValue;
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(HomeWorthPlaceName), value);
            }
        }



        public static string APISecure
        {
            get
            {
                var statusValue = AppSettings.GetValueOrDefault(nameof(APISecure), "0");
                return statusValue.IsEmpty() ? null : statusValue;
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(APISecure), value);
            }
        }

        public static string LoginStatus
        {
            get
            {
                var statusValue = AppSettings.GetValueOrDefault(nameof(LoginStatus), "0");
                return statusValue.IsEmpty() ? null : statusValue;
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(LoginStatus), value);
            }
        }
        public static string LoginUserId
        {
            get
            {
                var statusValue = AppSettings.GetValueOrDefault(nameof(LoginUserId), "0");
                return statusValue.IsEmpty() ? null : statusValue;
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(LoginUserId), value);
            }
        }


        public static string SearchKeyword
        {
            get
            {
                var statusValue = AppSettings.GetValueOrDefault(nameof(SearchKeyword), string.Empty);
                return statusValue;
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(SearchKeyword), value);
            }
        }


        public static string FullName
        {
            get
            {
                var keyValue = AppSettings.GetValueOrDefault(nameof(FullName), string.Empty);
                return keyValue.IsEmpty() ? null : keyValue;
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(FullName), value);
            }
        }

        public static string MobileNumber
        {
            get
            {
                var keyValue = AppSettings.GetValueOrDefault(nameof(MobileNumber), string.Empty);
                return keyValue.IsEmpty() ? null : keyValue;
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(MobileNumber), value);
            }
        }

        public static string EmailId
        {
            get
            {
                var keyValue = AppSettings.GetValueOrDefault(nameof(EmailId), string.Empty);
                return keyValue.IsEmpty() ? null : keyValue;
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(EmailId), value);
            }
        }

        public static string Radius
        {
            get
            {
                var statusValue = AppSettings.GetValueOrDefault(nameof(Radius), "0");
                return statusValue.IsEmpty() ? null : statusValue;
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(Radius), value);
            }
        }

        public static string token_time
        {
            get
            {
                var statusValue = AppSettings.GetValueOrDefault(nameof(token_time), "0");
                return statusValue.IsEmpty() ? null : statusValue;
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(token_time), value);
            }
        }

        public static string SensorSteps_time
        {
            get
            {
                var statusValue = AppSettings.GetValueOrDefault(nameof(SensorSteps_time), "0");
                return statusValue.IsEmpty() ? null : statusValue;
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(SensorSteps_time), value);
            }
        }

        public static bool PropertyImagePage;

        public static long StepSinceReboot
        {
            get
            {
                var keyValue = AppSettings.GetValueOrDefault(nameof(StepSinceReboot), string.Empty);
                string val = string.IsNullOrEmpty(keyValue) ? "0" : keyValue;
                return Convert.ToInt64(val);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(StepSinceReboot), value.ToString());
            }
        }

        public static long LastStep
        {
            get
            {
                var keyValue = AppSettings.GetValueOrDefault(nameof(LastStep), string.Empty);
                string val = string.IsNullOrEmpty(keyValue) ? "0" : keyValue;
                return Convert.ToInt64(val);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(LastStep), value.ToString());
            }
        }
        public static long NotSavedSteps
        {
            get
            {
                var keyValue = AppSettings.GetValueOrDefault(nameof(NotSavedSteps), string.Empty);
                string val = string.IsNullOrEmpty(keyValue) ? "0" : keyValue;
                return Convert.ToInt64(val);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(NotSavedSteps), value.ToString());
            }
        }

        public static long SensorSteps
        {
            get
            {
                var keyValue = AppSettings.GetValueOrDefault(nameof(SensorSteps), string.Empty);
                string val = string.IsNullOrEmpty(keyValue) ? "0" : keyValue;
                return Convert.ToInt64(val);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(SensorSteps), value.ToString());
            }
        }

        public static long todaySteps
        {
            get
            {
                var keyValue = AppSettings.GetValueOrDefault(nameof(todaySteps), string.Empty);
                string val = string.IsNullOrEmpty(keyValue) ? "0" : keyValue;
                return Convert.ToInt64(val);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(todaySteps), value.ToString());
            }
        }

        public static long IntervalStoptStepCounts
        {
            get
            {
                var keyValue = AppSettings.GetValueOrDefault(nameof(IntervalStoptStepCounts), string.Empty);
                string val = string.IsNullOrEmpty(keyValue) ? "0" : keyValue;
                return Convert.ToInt64(val);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(IntervalStoptStepCounts), value.ToString());
            }
        }

        public static long CurrentStep
        {
            get
            {
                var keyValue = AppSettings.GetValueOrDefault(nameof(CurrentStep), string.Empty);
                string val = string.IsNullOrEmpty(keyValue) ? "0" : keyValue;
                return Convert.ToInt64(val);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(CurrentStep), value.ToString());
            }
        }
    }
}
