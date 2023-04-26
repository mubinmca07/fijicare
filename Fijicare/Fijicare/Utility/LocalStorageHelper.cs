using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fijicare.Utility
{
    public static class LocalStorageHelper
    {
        public static void StoreInLocalSetting(string key, string val)
        {
            try
            {
                CrossSettings.Current.AddOrUpdateValue(key,val);  
            }
            catch (Exception Ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in storing in local settings", Ex.Message);
            }
        }
        public static string RetriveFromLocalSetting(string key)
        {
            string retVal = null;
            try
            {
                string serializedUser = CrossSettings.Current.GetValueOrDefault(key,string.Empty,null);
                if (serializedUser != null)
                {
                    retVal = serializedUser;
                }             
                return retVal;
            }
            catch (Exception Ex)
            {
                System.Diagnostics.Debug.WriteLine("Error in retrieving from local settings", Ex.Message);
                return retVal;
            }
        }

      
    }
}
