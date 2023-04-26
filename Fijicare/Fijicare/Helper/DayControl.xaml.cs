using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Fijicare.Helper
{
   


    public partial class DayControl : ContentView
    {
        public static readonly BindableProperty IsCurrentDateProperty = BindableProperty.Create(nameof(IsCurrentDate), typeof(bool), typeof(DayControl), default(bool), Xamarin.Forms.BindingMode.TwoWay);
        public bool IsCurrentDate
        {
            get
            {
                return (bool)GetValue(IsCurrentDateProperty);
            }

            set
            {
                SetValue(IsCurrentDateProperty, value);
            }
        }

        public static readonly BindableProperty LabelTextProperty
            = BindableProperty.Create(nameof(LabelText), typeof(string), typeof(DayControl), default(string), BindingMode.TwoWay);
        public string LabelText
        {
            get
            {
                return (string)GetValue(LabelTextProperty);
            }

            set
            {
                SetValue(LabelTextProperty, value);
            }
        }
        public DayControl()
        {
            InitializeComponent();
            label.Text = LabelText;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == LabelTextProperty.PropertyName)
            {
                label.Text = LabelText;
            }
            else if (propertyName == IsCurrentDateProperty.PropertyName)
            {
                frame.BackgroundColor = Color.Transparent;
                label.TextColor = Color.Black;
                if (IsCurrentDate)
                {
                    frame.BackgroundColor = Color.Black;
                    label.TextColor = Color.White;
                }
                
                //label.Text = LabelText;
            }
        }
    }
}
