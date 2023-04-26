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

[assembly: Xamarin.Forms.Dependency(typeof(OSVersion))]
namespace Fijicare.Droid.Native
{
    public class OSVersion : IosVersion
    {
        public string GetOSVersion()
        {
            return Android.OS.Build.VERSION.Release;
        }
    }
}