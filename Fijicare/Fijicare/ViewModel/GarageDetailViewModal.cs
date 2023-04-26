using Fijicare.Commands;
using Fijicare.Helpers;
using Fijicare.Model;
using Fijicare.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;
using Fijicare.Model;

namespace Fijicare.ViewModel
{


    public class GarageDetailViewModal : ModelBase
    {
        public DelegateCommand<string> BackCommand { get; set; }
        public DelegateCommand<string> DetailCommand { get; set; }
        protected INavigation NavigationService { get; set; }

        private ObservableCollection<Fijicare.Model.MotorGarage.MotorGarageObj> _Garage;
        public ObservableCollection<Fijicare.Model.MotorGarage.MotorGarageObj> Garage
        {
            get { return _Garage; }
            set { SetProperty(ref _Garage, value); }
        }



        public DelegateCommand LogoutButtonCommand { get; set; }
        public DelegateCommand SearchButtonCommand { get; set; }
        public string PageParameter { get; set; }
        public GarageDetailViewModal(INavigation _NavigationService)
        {
            this.NavigationService = _NavigationService;
            BackCommand = new DelegateCommand<string>(BackCommandClicked);
            LogoutButtonCommand = new DelegateCommand(LogoutButtonClicked);
            PageParameter = Session.PageParameter;
            NotificationCommand = new DelegateCommand(NotificationCommandClick);
            Garage = new ObservableCollection<Fijicare.Model.MotorGarage.MotorGarageObj>(Session.Garage);
            //   IsSearch = true
            FooterCommand = new DelegateCommand<string>(Footer_Click);
        }
        public DelegateCommand<string> FooterCommand { get; set; }
        private async void   Footer_Click(string obj)
        {
            Session.PageParameter = obj;

            if (obj == "DOCTORS")
            {
                await NavigationService.PushAsync(new Fijicare.ViewPage.DoctorPage());
            }
            /*if (obj == "Garage")
            {
                await NavigationService.PushAsync(new Fijicare.ViewPage.GarageSearch());
            }*/
            if (obj == "Hospitals")
            {
                await NavigationService.PushAsync(new Fijicare.ViewPage.SearchHospitalpage());
            }
            if (obj == "Home")
            {
                await NavigationService.PushAsync(new Fijicare.ViewPage.WelcomepPage());
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
        private async void   NotificationCommandClick()
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.NotificationPage());

        }


        public DelegateCommand NotificationCommand { get; set; }

        



        private async void   LogoutButtonClicked()
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.LoginPage());

        }

        private async void   BackCommandClicked(string obj)
        {
             await  NavigationService.PopAsync();
        }





    }
}

