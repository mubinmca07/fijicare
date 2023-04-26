
using Fijicare.Commands;
using Fijicare.Helpers;
using Fijicare.Model;
using Fijicare.Utility;
using Fijicare.ViewPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Fijicare.ViewModel
{
    public class DoctorPageViewModel : ModelBase
    {
        protected INavigation NavigationService { get; private set; }

        public DelegateCommand BackButtonCommand { get; set; }
        public DelegateCommand LogoutButtonCommand { get; set; }
        public DelegateCommand NotificationCommand { get; set; }

        public DelegateCommand<string> DetailCommand { get; set; }

        private string _PageName;
        public string PageName
        {
            get { return _PageName; }
            set { SetProperty(ref _PageName, value); }
        }

        private bool _IsProcessing = false;
        public bool IsProcessing
        {
            get { return _IsProcessing; }
            set { SetProperty(ref _IsProcessing, value); }
        }
        public DoctorPageViewModel(INavigation navigationService)
        {
            Category = Session.DoctorType;
            this.NavigationService = navigationService;
            PageName = Fijicare.Utility.Session.PageParameter;
            NotificationCommand = new DelegateCommand(NotificationCommandClick);
            BackButtonCommand = new DelegateCommand(BackCommandClick);
            DetailCommand = new DelegateCommand<string>(DetailCommandClick);
            LogoutButtonCommand = new DelegateCommand(LogoutButtonClicked);
            
            // getPolicy();
            FooterCommand = new DelegateCommand<string>(Footer_Click);

            string val = LocalStorageHelper.RetriveFromLocalSetting("IsLogin");
            IsLogin = false;
            if (!string.IsNullOrWhiteSpace(val) && val == "true")
            {
                IsLogin = true;
            }

        }
        private bool _IsLogin;
        public bool IsLogin
        {
            get { return _IsLogin; }
            set
            {
                IsLogOff = !value;
                SetProperty(ref _IsLogin, value);
            }
        }

        private bool _IsLogOff;
        public bool IsLogOff
        {
            get { return _IsLogOff; }
            set { SetProperty(ref _IsLogOff, value); }
        }

        private async void DetailCommandClick(string obj)
        {
            if (string.IsNullOrEmpty(obj))
                return;
            // int Id = Convert.ToInt32(obj);
            // HospitalDetail = new ObservableCollection<Model.MedicalProviders.MedicalProvidersObj>();
           // Session.Doctor = (from p in Hospital where p.id.ToString() == obj select p).ToList();
            // HospitalDetail.Add(ob);
            await NavigationService.PushAsync(new ViewPage.HospitalsPage());
        }

        private string _Provider = "";
        public string Provider
        {
            get { return _Provider; }
            set { SetProperty(ref _Provider, value); }
        }

        private string _Category = "";
        public string Category
        {
            get { return _Category; }
            set { SetProperty(ref _Category, value); }
        }   
        
         

        private async void   NotificationCommandClick()
        {
            //await NavigationService.PushAsync(new Fijicare.ViewPage.NotificationPage());

            if (!string.IsNullOrWhiteSpace(LocalStorageHelper.RetriveFromLocalSetting("IsLogin")))
            {
                if (LocalStorageHelper.RetriveFromLocalSetting("IsLogin") == "true")
                {
                    await NavigationService.PushAsync(new Fijicare.ViewPage.NotificationPage());
                }
                else
                    XFToast.ShortMessage(" Opps! Please login to view notification ");

            }
            else
                XFToast.ShortMessage(" Opps! Please login to view notification ");

        }
        private async void   BackCommandClick()
        {
             await  NavigationService.PopAsync();
        }
        private async void   LogoutButtonClicked()
        {
            await NavigationService.PushAsync(new LoginPage());

        }
        public DelegateCommand<string> FooterCommand { get; set; }
        private async void   Footer_Click(string obj)
        {
            Session.PageParameter = obj;

            /*if (obj == "DOCTORS")
            {
                await NavigationService.PushAsync(new Fijicare.ViewPage.DoctorPage());
            }*/
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
    }
}

