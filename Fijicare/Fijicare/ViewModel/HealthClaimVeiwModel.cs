using Fijicare.Commands;
using Fijicare.Helpers;
using Fijicare.Model;
using Fijicare.Utility;
using Fijicare.ViewPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fijicare.ViewModel
{
   public class HealthClaimVeiwModel : ModelBase
    {
        protected INavigation NavigationService { get; private set; }

        private string _PageName;
        public string PageName
        {
            get { return _PageName; }
            set { SetProperty(ref _PageName, value); }
        }

        private ObservableCollection<Fijicare.Model.HealthClaims.HealthClaimsObj> _HealthClaims;
        public ObservableCollection<Fijicare.Model.HealthClaims.HealthClaimsObj> HealthClaims
        {
            get { return _HealthClaims; }
            set { SetProperty(ref _HealthClaims, value); }
        }

        private bool _IsProcessing;
        public bool IsProcessing
        {
            get { return _IsProcessing; }
            set { SetProperty(ref _IsProcessing, value); }
        }

        private ObservableCollection<Fijicare.Model.MotorClaims.MotorClaimsObj> _MotorClaims;
        public ObservableCollection<Fijicare.Model.MotorClaims.MotorClaimsObj> MotorClaims
        {
            get { return _MotorClaims; }
            set { SetProperty(ref _MotorClaims, value); }
        }
        public DelegateCommand<string> BackCommand { get; set; }
        public DelegateCommand LogoutButtonCommand { get; set; }
        private bool _IsMotor = false;
        public bool IsMotor
        {
            get { return _IsMotor; }
            set { SetProperty(ref _IsMotor, value); }
        }
        private bool _IsHealth = false;
        public bool IsHealth
        {
            get { return _IsHealth; }
            set { SetProperty(ref _IsHealth, value); }
        }

        private bool _IsNoClaimFound = false;
        public bool IsNoClaimFound
        {
            get { return _IsNoClaimFound; }
            set { SetProperty(ref _IsNoClaimFound, value); }
        }
        private async void   NotificationCommandClick()
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.NotificationPage());

        }


        public DelegateCommand NotificationCommand { get; set; }


        public HealthClaimVeiwModel(INavigation navigation)
        {
            this.NavigationService = navigation;
            BackCommand = new DelegateCommand<string>(BackCommandClick);
            LogoutButtonCommand = new DelegateCommand(LogoutButtonClicked);

            NotificationCommand = new DelegateCommand(NotificationCommandClick);

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
        public async Task HealthClaim()
        {
            IsHealth = false;
            IsMotor = false;
            IsNoClaimFound = false;
            IsProcessing = true;
            Dictionary<string, object> PostParam = new Dictionary<string, object>();
            PostParam.Add("Policy_No", Session.Policy_No);


            ServiceRequestHelper _WebRequestHelper = new ServiceRequestHelper();


            string url = AppConstants.HostName + AppConstants.GetHealthClaims;
            ServiceRequestHelper.WebServiceResult result = await _WebRequestHelper.SendJsonDataPostRequest(url, PostParam);
            if (result != null && result.status == 200)
            {
                Model.HealthClaims.RootObject root = Newtonsoft.Json.JsonConvert.DeserializeObject<Fijicare.Model.HealthClaims.RootObject>(result.Result);

                List<Model.HealthClaims.ErrorObj> errorcode = root.ErrorObj;
                if (errorcode != null && errorcode.Count > 0)
                {
                    Model.HealthClaims.ErrorObj valChecksuccessorfail = errorcode[0];
                    if (valChecksuccessorfail.ErrorMessage == "Success" && valChecksuccessorfail.ErrorCode == "0")
                    {
                        Model.HealthClaims.ResponseObj Responseresult = root.ResponseObj;
                        HealthClaims = new ObservableCollection<Fijicare.Model.HealthClaims.HealthClaimsObj>(Responseresult.HealthClaimsObj);
                        IsHealth = true;
                        IsProcessing = false;
                    }
                    else
                    {
                        IsNoClaimFound = true;
                        IsProcessing = false;
                        // XFToast.ShortMessage("Please enter valid credential.");
                        XFToast.ShortMessage("There is no claim fond");
                    }

                }
                else
                {
                    IsProcessing = false;
                    XFToast.ShortMessage("Something went wrong...");
                }

            }
            else
            {
                IsProcessing = false;
                XFToast.ShortMessage("Please check your internet connection.");
            }
        }
        public async Task MotorClaim()
        {
            IsHealth = false;
            IsMotor = false;
            IsNoClaimFound = false;

            Dictionary<string, object> PostParam = new Dictionary<string, object>();
            PostParam.Add("Policy_No", Session.Policy_No);

            IsProcessing = true;
            ServiceRequestHelper _WebRequestHelper = new ServiceRequestHelper();


            string url = AppConstants.HostName + AppConstants.GetMotorClaims;
            ServiceRequestHelper.WebServiceResult result = await _WebRequestHelper.SendJsonDataPostRequest(url, PostParam);
            if (result != null && result.status == 200)
            {
                Model.MotorClaims.RootObject root = Newtonsoft.Json.JsonConvert.DeserializeObject<Fijicare.Model.MotorClaims.RootObject>(result.Result);

                List<Model.MotorClaims.ErrorObj> errorcode = root.ErrorObj;
                if (errorcode != null && errorcode.Count > 0)
                {
                    Model.MotorClaims.ErrorObj valChecksuccessorfail = errorcode[0];
                    if (valChecksuccessorfail.ErrorMessage == "Success" && valChecksuccessorfail.ErrorCode == "0")
                    {
                        Model.MotorClaims.ResponseObj Responseresult = root.ResponseObj;
                        MotorClaims = new ObservableCollection<Fijicare.Model.MotorClaims.MotorClaimsObj>(Responseresult.MotorClaimsObj);
                        IsMotor = true;
                        IsProcessing = false;
                    }
                    else
                    {
                        IsNoClaimFound = true;
                        // XFToast.ShortMessage("Please enter valid credential.");
                        XFToast.ShortMessage("There is no claim fond");
                        IsProcessing = false;
                    }

                }
                else
                {
                    XFToast.ShortMessage("Something went wrong...");
                    IsProcessing = false;
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
        private async void   LogoutButtonClicked()
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.LoginPage());

        }
    }
}
