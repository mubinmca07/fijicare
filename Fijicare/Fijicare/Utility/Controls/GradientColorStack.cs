using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Fijicare.Utility.Controls
{
    public class GradientColorStack : StackLayout
    {
      

        //public  BindableProperty BorderColorProperty = BindableProperty.Create(nameof(StartColor),  typeof(Color), typeof(GradientColorStack), Color.Gray);
        //// Gets or sets BorderColor value
        //public Color StartColor
        //{
        //    get => (Color)GetValue(BorderColorProperty);
        //    set => SetValue(BorderColorProperty, value);
        //}

        public static readonly BindableProperty StartColorProperty = BindableProperty.Create(
                                                          "StartColor", //Public name to use
                                                          typeof(Color), //this type
                                                          typeof(GradientColorStack), //parent type (tihs control)
                                                          Color.White); //default value
        public Color StartColor
        {
            get { return (Color)GetValue(StartColorProperty); }
            set { SetValue(StartColorProperty, value); }
        }

        public static readonly BindableProperty EndColorProperty = BindableProperty.Create("EndColor", typeof(Color), typeof(GradientColorStack), Color.Gray);
        // Gets or sets BorderColor value
        public Color EndColor
        {
            get => (Color)GetValue(EndColorProperty);
            set => SetValue(EndColorProperty, value);
        }
       
    }
}
