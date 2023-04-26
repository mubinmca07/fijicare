﻿using Fijicare.Commands;
using Fijicare.Helpers;
using Fijicare.Model;
using Fijicare.Utility;
using Fijicare.ViewPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Fijicare.ViewModel
{


    public class BenifitViewModal : ModelBase
    {
        protected INavigation NavigationService { get; private set; }

        private string _PageName;
        public string PageName
        {
            get { return _PageName; }
            set { SetProperty(ref _PageName, value); }
        }

        private ObservableCollection<Fijicare.Model.PolicyBenefitUtilizationObj> _HealthPoliciesBenefit;
        public ObservableCollection<Fijicare.Model.PolicyBenefitUtilizationObj> HealthPoliciesBenefit
        {
            get { return _HealthPoliciesBenefit; }
            set { SetProperty(ref _HealthPoliciesBenefit, value); }
        }

        private bool _IsProcessing;
        public bool IsProcessing
        {
            get { return _IsProcessing; }
            set { SetProperty(ref _IsProcessing, value); }
        }



        public DelegateCommand<string> BackCommand { get; set; }

        public DelegateCommand LogoutButtonCommand { get; set; }
        public DelegateCommand<string> MemberDetailCommand { get; set; }

        public BenifitViewModal(INavigation _NavigationService)
        {
            this.NavigationService = _NavigationService; ;
            NotificationCommand = new DelegateCommand(NotificationCommandClick);

            BackCommand = new DelegateCommand<string>(BackCommandClick);
            LogoutButtonCommand = new DelegateCommand(LogoutButtonClicked);
            HealthPoliciesBenefit = new ObservableCollection<Model.PolicyBenefitUtilizationObj>(Session.HealthPoliciesBenefit);
            // getPolicy();

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

        private async void   NotificationCommandClick()
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.NotificationPage());

        }


        public DelegateCommand NotificationCommand { get; set; }


        private async void   LogoutButtonClicked()
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.LoginPage());

        }

        private async void   BackCommandClick(string obj)
        {
             await  NavigationService.PopAsync();
        }

       
    }
}
