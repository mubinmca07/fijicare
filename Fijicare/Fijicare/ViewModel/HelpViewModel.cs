
using Fijicare.Commands;
using Fijicare.Helpers;
using Fijicare.Model;
using Fijicare.Utility;
using Xamarin.Forms;
using Fijicare.ViewPage;
namespace Fijicare.ViewModel
{
    public class HelpViewModel: ModelBase
    {
        protected INavigation NavigationService { get; set; }
        public DelegateCommand<string> BackCommand { get; set; }
        public DelegateCommand<string> DetailCommand { get; set; }
        public DelegateCommand LogoutButtonCommand { get; set; }      
        public DelegateCommand<string> FooterCommand { get; set; }

        public DelegateCommand<string> CallingCommand { get; set; }
        public DelegateCommand<string> EmaillingCommand { get; set; }

        public HelpViewModel(INavigation _NavigationService)
        {
            this.NavigationService = _NavigationService;
            BackCommand = new DelegateCommand<string>(BackCommandClicked);
            LogoutButtonCommand = new DelegateCommand(LogoutButtonClicked);
            NotificationCommand = new DelegateCommand(NotificationCommandClick);
            FooterCommand = new DelegateCommand<string>(Footer_Click);
            CallingCommand = new DelegateCommand<string>(CallingCommand_Click);
            EmaillingCommand = new DelegateCommand<string>(EmaillingCommand_Click);

        }

        private async void   EmaillingCommand_Click(string obj)
        {
            Device.OpenUri(new System.Uri("mailto:" + obj));
        }
        private async void   CallingCommand_Click(string obj)
        {
            Device.OpenUri(new System.Uri("tel:" + obj));
        }
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
            //await NavigationService.PushAsync(new Fijicare.ViewPage.NotificationPage());

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
