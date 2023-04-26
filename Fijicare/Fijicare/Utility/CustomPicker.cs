using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Fijicare.Utility
{
    public class CustomPicker : Picker
    {
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(CustomPicker), string.Empty);

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }



        public static readonly BindableProperty IsBorderProperty =
       BindableProperty.Create(nameof(IsBorder),
      typeof(bool), typeof(CustomPicker), true);
        // Gets or sets BorderColor value
        public bool IsBorder
        {
            get => (bool)GetValue(IsBorderProperty);
            set => SetValue(IsBorderProperty, value);
        }
    }
}
