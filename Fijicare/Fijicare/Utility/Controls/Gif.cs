using Fijicare.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Fijicare.Utility.Controls
{
    public class Gif : WebView
    {

        //public string GifSource
        //{
        //    get {

        //        string imgSource = DependencyService.Get<IGif>().Get();
        //        var html = new HtmlWebViewSource();
        //        html.Html = String.Format(@"<html><body><img src='{0}'  style='width: 100px; height: 100px;' /></body></html>", imgSource);
        //       return (string)GetValue(SourceProperty);

        //    }
        //    set
        //    {
        //        string imgSource = DependencyService.Get<IGif>().Get() + value;
        //        var html = new HtmlWebViewSource();
        //        html.Html = String.Format(@"<html><body><img src='{0}'  style='width: 100px; height: 100px;' /></body></html>", imgSource);
        //        SetValue(SourceProperty, html);
        //    }
        //}


        public static readonly BindableProperty GifSourceProperty = BindableProperty.Create(nameof(GifSource), typeof(string), typeof(Gif), null);
        public string GifSource
        {
            get { return (string)GetValue(GifSourceProperty); }
            set { SetValue(GifSourceProperty, value);

                string imgSource = DependencyService.Get<IGif>().Get() + value;
                HtmlWebViewSource html = new HtmlWebViewSource();
                html.Html = String.Format(@"<html><body><img src='{0}'  style='width: 100px; height: 100px;' /></body></html>", imgSource);
                SetValue(GifSourceProperty, html);
            }
        }

        Action<string> action;
        public void RegisterAction(Action<string> callback)
        {
            action = callback;
        }

        public void Cleanup()
        {
            action = null;
        }

        public void InvokeAction(string data)
        {
            if (action == null || data == null)
            {
                return;
            }
            action.Invoke(data);
        }
    }
}
