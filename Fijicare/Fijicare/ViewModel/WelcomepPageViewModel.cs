using System;
using System.Collections.Generic;
using System.Text;
using Fijicare.Commands;
using Fijicare.Helpers;
using Fijicare.Model;
using Fijicare.Utility;
using Fijicare.ViewPage;
using Xamarin.Forms;

namespace Fijicare.ViewModel
{
    public class WelcomepPageViewModal:ModelBase
    {
        INavigation NavigationService;
        public WelcomepPageViewModal(INavigation navigation)
        {
            NavigationService = navigation;
            MainCommand = new DelegateCommand<string>(Main_Click);

            FooterCommand = new DelegateCommand<string>(Footer_Click);
            if (!string.IsNullOrWhiteSpace(LocalStorageHelper.RetriveFromLocalSetting("IsLogin")))
                if (LocalStorageHelper.RetriveFromLocalSetting("IsLogin") == "true")
                {
                    LoginText = "DASHBOARD";
                }
        }

        private bool _IsLoginButton;
        public bool IsLoginButton
        {
            get { return _IsLoginButton; }
            set { SetProperty(ref _IsLoginButton, value); }
        }

        private string _LoginText  = "LOGIN";
        public string LoginText
        {
            get { return _LoginText; }
            set { SetProperty(ref _LoginText, value); }
        }
        
        public DelegateCommand<string> FooterCommand { get; set; }
        private async void Footer_Click(string obj)
        {
            Session.PageParameter = obj;

            if (obj == "DOCTORS")
            {
                await NavigationService.PushAsync(new Fijicare.ViewPage.DoctorPage());
            }
            if (obj == "Garage")
            {
                await NavigationService.PushAsync(new Fijicare.ViewPage.GarageSearch());
            }
            if (obj == "Hospitals")
            {
                await NavigationService.PushAsync(new Fijicare.ViewPage.SearchHospitalpage());
            }
            if (obj == "Home")
            {
                await NavigationService.PushAsync(new WelcomepPage());
            }

            if (obj == "PHARMACY")
            {
                await NavigationService.PushAsync(new Fijicare.ViewPage.PharmecyPage());
            }
            if (obj == "EMERGENCY")
            {
                await NavigationService.PushAsync(new Fijicare.ViewPage.EmergencyContacts());
            }


        }

        public DelegateCommand<string> MainCommand { get; set; }
        private async void   Main_Click(string obj)
        {
            Session.PageParameter = obj;

            if (obj == "PRODUCT")
            {
                string url = $"http://www.Fijicare.com.fj";
                Device.OpenUri(new Uri(url));

                //await NavigationService.PushAsync(new Fijicare.ViewPage.DoctorPage());
            }
            if (obj == "NEWS_MEDIA")
            {
                string url = $"http://www.Fijicare.com.fj/Pages/Media/NewsAndMedia";
                Device.OpenUri(new Uri(url));
                
            }
            if (obj == "About")
            {
                string url = $"http://www.Fijicare.com.fj/Pages/Aboutus";

                Device.OpenUri(new Uri(url));
                //  await NavigationService.PushAsync(new Fijicare.ViewPage.SearchHospitalpage());
            }
            if (obj == "Contact")
            {
                string url = $"http://www.Fijicare.com.fj/Pages/Contact";
                Device.OpenUri(new Uri(url));
                // await NavigationService.PushAsync(new ViewPage.WelcomepPage());
            }

        }
    }
}
