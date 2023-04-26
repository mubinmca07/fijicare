using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;

using Fijicare.Droid.Native;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

//[assembly: ExportRenderer(typeof(ListView), typeof(CustomListViewRenderer))]
namespace Fijicare.Droid.Native
{
    public class CustomListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                //  Control.Enabled = false;
                Control.SetDrawSelectorOnTop(false);
                // Control.ScrollEnabled = false;
            }
        }
    }
}

