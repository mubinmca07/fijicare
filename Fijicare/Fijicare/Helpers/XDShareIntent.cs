using Fijicare.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Fijicare.Helpers
{
  public static  class XDShareIntent
    {
        public static void ShareIntent(string value)
        {
            IShareable sharer = DependencyService.Get<IShareable>();
            sharer.OpenShareIntent(value);
        }
    }
}
