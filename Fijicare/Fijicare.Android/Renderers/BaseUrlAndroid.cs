using System;
using Fijicare.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(Fijicare.Droid.Renderers.BaseUrlAndroid))]
namespace Fijicare.Droid.Renderers
{
    public class BaseUrlAndroid : IBaseUrl
    {
        public string Get()
        {
            return "file:///android_asset/";
        }
    }
}
