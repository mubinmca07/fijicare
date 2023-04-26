using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Fijicare.Utility.CustomEntry
{
    public class CustumScrollView : ScrollView
    {
        public static readonly BindableProperty IsHorizontalScrollProperty =
        BindableProperty.Create(nameof(IsHorizontalScrollProperty),
            typeof(bool), typeof(CustumScrollView), true);
        // Gets or sets IsCurvedCornersEnabled value
        public bool IsHorizontalScroll
        {
            get => (bool)GetValue(IsHorizontalScrollProperty);
            set => SetValue(IsHorizontalScrollProperty, value);
        }
    }
}
