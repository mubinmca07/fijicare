using System;
using System.Diagnostics;
using Xamarin.Forms;
using Fijicare.Interfaces;

namespace FijiCareApiCall
{
    public static class CacheService
    {
        static ICache Cacher = DependencyService.Get<ICache>();

        public static void Save<TModel>(TModel model, string key)
        {
            try
            {
                Cacher.Save(model, NormalizeKey(key));
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }

        public static bool TryLoad<TModel>(string key, out TModel result)
        {
            return Cacher.TryLoad(NormalizeKey(key), out result);
        }

        public static void Remove(string key)
        {
            Cacher.Remove(NormalizeKey(key));
        }

        public static void RemoveAll()
        {
            Cacher.RemoveAll();
        }

        static string NormalizeKey(string key)
        {
            return key.Replace("/", "");
        }
    }
}
