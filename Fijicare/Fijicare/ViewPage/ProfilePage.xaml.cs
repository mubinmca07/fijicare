using System;
using System.Collections.Generic;
using Fijicare.ViewModel;
using Xamarin.Forms;

namespace Fijicare.ViewPage
{
    public partial class ProfilePage : ContentPage
    {
       
        private ProfilePageViewModel profilePageViewModel;
        public ProfilePage()
        {
            InitializeComponent();
            profilePageViewModel = new ProfilePageViewModel(Navigation);
            this.BindingContext = profilePageViewModel;
        }
    }
}
