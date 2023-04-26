using System;
using System.Collections.Generic;
using Fijicare.ViewModel;
using Xamarin.Forms;

namespace Fijicare.ViewPage
{
    public partial class ResistrationPage : ContentPage
    {
       
        private ResistrationViewModel resistrationViewModel;
        public ResistrationPage()
        {
            InitializeComponent();
            resistrationViewModel = new ResistrationViewModel(Navigation);
            this.BindingContext = resistrationViewModel;
        }
    }
}
