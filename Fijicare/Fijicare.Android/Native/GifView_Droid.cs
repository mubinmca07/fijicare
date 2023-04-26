using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Fijicare.Droid.Native;
using Fijicare.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(GifView_Droid))]
namespace Fijicare.Droid.Native
{
    public class GifView_Droid : IGif
    {
        public string Get()
        {
            return "file:///android_asset/";
        }
    }
}