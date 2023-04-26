using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Fijicare.Droid.Native;
using Fijicare.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(APPVersion))]
namespace Fijicare.Droid.Native
{
  public  class APPVersion: IAppVersion
    {

       public string GetAppVersion()
        {
            Context context = global::Android.App.Application.Context;

            PackageManager manager = context.PackageManager;
            PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);

            return info.VersionName;
        }
    }
}
