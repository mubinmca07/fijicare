using Fijicare.Commands;
using Fijicare.Helpers;
using Fijicare.Model;
using Fijicare.Utility;
using Fijicare.ViewPage;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Fijicare.ViewModel
{
    public class PdfViewModel : ModelBase
    {
        protected INavigation NavigationService { get; private set; }

        private string _PageName;
        public string PageName
        {
            get { return _PageName; }
            set { SetProperty(ref _PageName, value); }
        }

        public DelegateCommand<string> BackCommand { get; set; }

        public DelegateCommand LogoutButtonCommand { get; set; }
        public DelegateCommand<string> MemberDetailCommand { get; set; }

        public PdfViewModel(INavigation _NavigationService)
        {
            this.NavigationService = _NavigationService; ;
            NotificationCommand = new DelegateCommand(NotificationCommandClick);

            BackCommand = new DelegateCommand<string>(BackCommandClick);
            LogoutButtonCommand = new DelegateCommand(LogoutButtonClicked);

            // getPolicy();

            FooterCommand = new DelegateCommand<string>(Footer_Click);
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
                return;
                // await NavigationService.PushAsync(new Fijicare.ViewPage.PharmecyPage());
            }
            if (obj == "EMERGENCY")
            {
                await NavigationService.PushAsync(new Fijicare.ViewPage.EmergencyContacts());
            }

        }

        private async void NotificationCommandClick()
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.NotificationPage());
        }

        public DelegateCommand NotificationCommand { get; set; }


        private async void LogoutButtonClicked()
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.LoginPage());
        }

        public async void BackCommandClick(string obj)
        {
             await  NavigationService.PopAsync();
        }
    }
}
