using System;
using System.Collections.Generic;
using Fijicare.ViewModel;
using Xamarin.Forms;

namespace Fijicare.ViewPage
{
    public partial class ReSetPasswordPage : ContentPage
    {
       
        private ReSetPasswordViewModel resetpasswordViewModel;
        public ReSetPasswordPage()
        {
            InitializeComponent();
            resetpasswordViewModel = new ReSetPasswordViewModel(Navigation);
            this.BindingContext = resetpasswordViewModel;
        }
    }
}
