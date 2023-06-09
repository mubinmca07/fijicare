﻿using System;
using Xamarin.Forms;

namespace Fijicare.Utility.PDF
{
    public class PdfView : WebView
    {
        //public static readonly BindableProperty UriProperty =
        //    BindableProperty.Create(nameof(Uri), typeof(string), typeof(PdfView));

        //public string Uri
        //{
        //    get { return (string)GetValue(UriProperty); }
        //    set { SetValue(UriProperty, value); }
        //}


        public static readonly BindableProperty UriProperty = BindableProperty.Create(
        propertyName: "Uri",
        returnType: typeof(string),
        declaringType: typeof(PdfView),
        defaultValue: default(string));

        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }
    }
}
