using Fijicare.Commands;
using Fijicare.Helpers;
using Fijicare.Model;
using Fijicare.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;

namespace Fijicare.ViewModel
{
   

    public class HospitalDeatailViewModel : ModelBase
    {
        public DelegateCommand<string> BackCommand { get; set; }
        public DelegateCommand<string> DetailCommand { get; set; }
        protected INavigation NavigationService { get; set; }
        public DelegateCommand NotificationCommand { get; set; }

        private ObservableCollection<Fijicare.Model.Doctor.BulkCapitionProviderObj> _Hospital;
        public ObservableCollection<Fijicare.Model.Doctor.BulkCapitionProviderObj> Hospital
        { 
            get { return _Hospital; }
            set { SetProperty(ref _Hospital, value); }
        }


        private string _Heading;
        public string Heading
        {
            get { return _Heading; }
            set { SetProperty(ref _Heading, value); }
        }
        public DelegateCommand LogoutButtonCommand { get; set; }
        public DelegateCommand SearchButtonCommand { get; set; }
        public DelegateCommand<string> FooterCommand { get; set; }
        public string PageParameter { get; set; }
        public HospitalDeatailViewModel(INavigation _NavigationService)
        {
            this.NavigationService = _NavigationService;
            BackCommand = new DelegateCommand<string>(BackCommandClicked);
            LogoutButtonCommand = new DelegateCommand(LogoutButtonClicked);
            FooterCommand = new DelegateCommand<string>(Footer_Click);
            PageParameter = Session.PageParameter;
            Hospital = new ObservableCollection<Fijicare.Model.Doctor.BulkCapitionProviderObj>(Session.Doctor);
            txtName = "Doctor Name";
            Heading = "Doctor Detail";
            if (Session.DoctorType == "Pharmacy")
            {
                txtName = " Name";
                Heading = "Pharmacy Detail";
                Hospital[0].Business_Name = Hospital[0].Manager_Nm;
            }
            txtOperatingHours = "Operating Hours";

            NotificationCommand = new DelegateCommand(NotificationCommandClick);         
            

        }

        private string _txtOperatingHours ="Operating Hours";
        public string txtOperatingHours
        {
            get { return _txtOperatingHours; }
            set { SetProperty(ref _txtOperatingHours, value); }
        }

        private string _txtName ="Doctor Name";
        public string txtName
        {
            get { return _txtName; }
            set { SetProperty(ref _txtName, value); }
        }

        private async void   NotificationCommandClick()
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.NotificationPage());

        }







        private async void   LogoutButtonClicked()
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.LoginPage());

        }

        private async void   BackCommandClicked(string obj)
        {
             await  NavigationService.PopAsync();
        }
        private async void   Footer_Click(string obj)
        {
            Session.PageParameter = obj;


            if (obj == "PHARMACY")
            {
                await NavigationService.PushAsync(new Fijicare.ViewPage.PharmecyPage());
            }
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
                await NavigationService.PushAsync(new Fijicare.ViewPage.WelcomepPage());
            }
            if (obj == "EMERGENCY")
            {
                await NavigationService.PushAsync(new Fijicare.ViewPage.EmergencyContacts());
            }

        }




    }
}
