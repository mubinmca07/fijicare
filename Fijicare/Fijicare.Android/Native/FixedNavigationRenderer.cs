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

using Xamarin.Forms.Platform.Android.AppCompat;


namespace Fijicare.Droid.Native
{
  public  class FixedNavigationRenderer : Xamarin.Forms.Platform.Android.ListViewRenderer
    {
        public FixedNavigationRenderer()
            : base() {

        }

        public FixedNavigationRenderer(IntPtr javaReference, JniHandleOwnership transfer)
            : base() { }

        protected override void OnDetachedFromWindow()
        {
            if (Element == null)
                return;

            base.OnDetachedFromWindow();
        }
    }
}