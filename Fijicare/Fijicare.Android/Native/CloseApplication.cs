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
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(CloseApplication))]
namespace Fijicare.Droid.Native
{
    public class CloseApplication : ICloseApplication
    {
        public void closeApplication()
        {
            Activity activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
    }
}
