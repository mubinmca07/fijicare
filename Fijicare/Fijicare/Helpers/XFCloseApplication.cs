using Fijicare.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Fijicare.Helpers
{
    public static class XFCloseApplication
    {
        public static void CloseApplication()
        {
            ICloseApplication closer = DependencyService.Get<ICloseApplication>();
            closer?.closeApplication();
        }
    }
}
