using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Fijicare.Utility.PDF
{
    
    public class CustomWebViewForPdf : WebView
    {
        public static readonly BindableProperty UriProperty = BindableProperty.Create(propertyName: "Uri",
                returnType: typeof(string),
                declaringType: typeof(CustomWebViewForPdf),
                defaultValue: default(string));

        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        public static readonly BindableProperty IsPdfProperty = BindableProperty.Create(propertyName: "IsPdf",
        returnType: typeof(bool),
        declaringType: typeof(CustomWebViewForPdf),
        defaultValue: default(bool));

        public bool IsPdf
        {
            get { return (bool)GetValue(IsPdfProperty); }
            set { SetValue(IsPdfProperty, value); }
        }
    }
}
