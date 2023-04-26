using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Fijicare.Utility.Controls
{
   public class MaxLinesLabel : Label
    {
        public MaxLinesLabel() { }
        public static BindableProperty MaxLinesProperty = BindableProperty.Create("MaxLines", typeof(int), typeof(MaxLinesLabel), int.MaxValue, BindingMode.Default);
        public int MaxLines
        {
            get => (int)GetValue(MaxLinesProperty);
            set => SetValue(MaxLinesProperty, value);
        }
    }
}
