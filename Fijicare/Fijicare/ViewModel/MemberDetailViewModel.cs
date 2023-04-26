using Fijicare.Commands;
using Fijicare.Helpers;
using Fijicare.Model;
using Fijicare.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using Fijicare.ViewPage;

namespace Fijicare.ViewModel
{
    public class MemberDetailViewModel:ModelBase
    {
        INavigation NavigationService;

        private string _HeadingTitle;
        public string HeadingTitle
        {
            get { return _HeadingTitle; }
            set { SetProperty(ref _HeadingTitle, value); }
        }
        public MemberDetailViewModel(INavigation _NavigationService)
        {
            this.NavigationService = _NavigationService;
            BackCommand = new DelegateCommand<string>(BackCommandClick);
            NotificationCommand = new DelegateCommand(NotificationCommandClick);
            LogoutButtonCommand = new DelegateCommand(LogoutButtonClicked);
            if (!Session.IsComingFromInsured)
            {
                List<Model.MemberDetails.HealthRiskDtl> lst = (from p in Session.HealthRiskDtl where p.Policy_id == Session.Policy_Id select p).ToList();

                HealthRiskDtl = new ObservableCollection<Model.MemberDetails.HealthRiskDtl>(lst);
                HeadingTitle = "Member Details";
            }
            else
            {
                Health(Session.Policy_Id);
                HeadingTitle = "Insured Details";
            }
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

        private async void   LogoutButtonClicked()
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.LoginPage());

        }
        public DelegateCommand NotificationCommand { get; set; }

      //  NotificationCommand = new DelegateCommand(NotificationCommandClick);

        private async void Health(string obj)
        {
            Dictionary<string, object> PostParam = new Dictionary<string, object>();
            PostParam.Add("Policy_Id", obj);


            ServiceRequestHelper _WebRequestHelper = new ServiceRequestHelper();


            string url = AppConstants.HostName + AppConstants.GetHealthRiskDtls;
            ServiceRequestHelper.WebServiceResult result = await _WebRequestHelper.SendJsonDataPostRequest(url, PostParam);
            if (result != null && result.status == 200)
            {
                Model.MemberDetails.RootObject root = Newtonsoft.Json.JsonConvert.DeserializeObject<Fijicare.Model.MemberDetails.RootObject>(result.Result);

                List<Model.MemberDetails.ErrorObj> errorcode = root.ErrorObj;
                if (errorcode != null && errorcode.Count > 0)
                {
                    Model.MemberDetails.ErrorObj valChecksuccessorfail = errorcode[0];
                    if (valChecksuccessorfail.ErrorMessage == "Success" && valChecksuccessorfail.ErrorCode == "0")
                    {
                        Model.MemberDetails.ResponseObj Responseresult = root.ResponseObj;
                        HealthRiskDtl = new ObservableCollection<Fijicare.Model.MemberDetails.HealthRiskDtl>(Responseresult.HealthRiskDtls);

                    }
                    else
                    {
                        // XFToast.ShortMessage("Please enter valid credential.");
                        XFToast.ShortMessage("there is no record available.");
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
        }

        private async void   BackCommandClick(string obj)
        {
             await  NavigationService.PopAsync();
        }

        public DelegateCommand<string> BackCommand { get; set; }
       // public DelegateCommand<string> LogoutButtonCommand { get; set; }
        public DelegateCommand LogoutButtonCommand { get; set; }
        private ObservableCollection<Fijicare.Model.MemberDetails.HealthRiskDtl> _HealthRiskDtl;
        public ObservableCollection<Fijicare.Model.MemberDetails.HealthRiskDtl> HealthRiskDtl
        {
            get { return _HealthRiskDtl; }
            set { SetProperty(ref _HealthRiskDtl, value); }
        }
    }
}
