using System;
using System.Collections.Generic;
using Fijicare.Helpers;
using Fijicare.Utility;
using Fijicare.ViewPage;
using Fijicare.ViewModel;
using Xamarin.Forms;

namespace Fijicare.ViewPage
{
    public partial class WelcomepPage : ContentPage
    {
        WelcomepPageViewModal welcomepPageViewModal;
        public WelcomepPage()
        {
            InitializeComponent();
            // news.Text = @"NEWS & MEDIA";
            signup.Text = "SIGNUP";
            
            welcomepPageViewModal = new WelcomepPageViewModal(Navigation);
            this.BindingContext = welcomepPageViewModal;
            string val = LocalStorageHelper.RetriveFromLocalSetting("IsLogin");
            welcomepPageViewModal.IsLoginButton = false;
            if (!string.IsNullOrWhiteSpace(val) && val=="true")
            {
                welcomepPageViewModal.IsLoginButton = true;
                signup.Text = "LOGOUT";
                
            }

        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
           if (welcomepPageViewModal.IsLoginButton)
            {
                await Navigation.PushAsync(new Dashboardpage());
            }
            else
            {
                await Navigation.PushAsync(new LoginPage());
           }
            
        }
        private async void Signup_Clicked(object sender, EventArgs e)
        {


            if (signup.Text == "SIGNUP")
            {
                await Navigation.PushAsync(new Fijicare.ViewPage.ResistrationPage());
            }
            else if(signup.Text == "LOGOUT")
            {
                await Navigation.PushAsync(new Fijicare.ViewPage.LoginPage());
            }
            

        }

    }
}
