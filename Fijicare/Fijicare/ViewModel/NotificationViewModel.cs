using Fijicare.Commands;
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
    public class NotificationViewModel : ModelBase
    {

        protected INavigation NavigationService { get; private set; }
        public DelegateCommand<string> BackCommand { get; set; }
        public DelegateCommand LogoutButtonCommand { get; set; }
        public NotificationViewModel(INavigation navigationService)
        {
            this.NavigationService = navigationService;
            BackCommand = new DelegateCommand<string>(BackCommandClick);
            LogoutButtonCommand = new DelegateCommand(LogoutButtonClicked);


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



        private bool isProcessing;
        public bool IsProcessing
        {
            get { return isProcessing; }
            set { SetProperty(ref isProcessing, value); }
        }
        private ObservableCollection<Fijicare.Model.Notification.NotificationsObj> notifications;
        public ObservableCollection<Fijicare.Model.Notification.NotificationsObj> Notifications
        {
            get { return notifications; }
            set { SetProperty(ref notifications, value); }
        }
        public async void GetNotification()
        {  IsProcessing = true;

            Dictionary<string, object> PostParam = new Dictionary<string, object>();
            PostParam.Add("Entity_Cd", Session.userDetail.Entity_Cd);


            ServiceRequestHelper _WebRequestHelper = new ServiceRequestHelper();


            string url = AppConstants.HostName + AppConstants.NOTIFIFATION;
            ServiceRequestHelper.WebServiceResult result = await _WebRequestHelper.SendJsonDataPostRequest(url, PostParam);
            if (result != null && result.status == 200)
            {
                Model.Notification.RootObject root = Newtonsoft.Json.JsonConvert.DeserializeObject< Fijicare.Model.Notification.RootObject>(result.Result);

                List<Model.Notification.ErrorObj> errorcode = root.ErrorObj;
                if (errorcode != null && errorcode.Count > 0)
                {
                    Model.Notification.ErrorObj valChecksuccessorfail = errorcode[0];
                    if (valChecksuccessorfail.ErrorMessage == "Success" && valChecksuccessorfail.ErrorCode == "0")
                    {
                        Model.Notification.ResponseObj Responseresult = root.ResponseObj;
                        Notifications = new ObservableCollection<Fijicare.Model.Notification.NotificationsObj>(Responseresult.NotificationsObj);

                    }
                    else
                    {
                        // XFToast.ShortMessage("Please enter valid credential.");
                        XFToast.ShortMessage("there is no notification available.");
                    }

                }
                else
                {
                    XFToast.ShortMessage("Something went wrong...");
                }

            }
            else
            {
                XFToast.ShortMessage("Please check your internet connection.");
            }
            IsProcessing = false;
        }
        private async void   BackCommandClick(string obj)
        {
             await  NavigationService.PopAsync();
        }
        private async void   LogoutButtonClicked()
        {
            await NavigationService.PushAsync(new LoginPage());

        }
    }
}
