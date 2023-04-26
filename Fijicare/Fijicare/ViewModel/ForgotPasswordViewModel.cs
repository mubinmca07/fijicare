using Fijicare.Commands;
using Fijicare.Helpers;
using Fijicare.Model;
using Fijicare.Model.Login;
using Fijicare.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Fijicare.ViewModel
{

    public class ForgotPasswordViewModel : ModelBase
    {
        protected INavigation NavigationService { get; private set; }

        public DelegateCommand BackButtonCommand { get; set; }
        public DelegateCommand LoginButtonCommand { get; set; }
        public DelegateCommand ForgotPasswordCommand { get; set; }
        public DelegateCommand RegistrationCommand { get; set; }
        public DelegateCommand ReSetPasswordCommand { get; set; }
        private string _username = Session.username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private string _password = "";
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }


        private bool _IsProcessing;
        public bool IsProcessing
        {
            get { return _IsProcessing; }
            set { SetProperty(ref _IsProcessing, value); }
        }
        private string _client;
        public string Client
        {
            get { return _client; }
            set { SetProperty(ref _client, value); }
        }

        public ForgotPasswordViewModel(INavigation navigationService)
        {
            this.NavigationService = navigationService;
            BackButtonCommand = new DelegateCommand(BackButtonCommandClick);
            //LoginButtonCommand = new DelegateCommand(LoginButtonCommandClick);
            ForgotPasswordCommand = new DelegateCommand(ForgotPasswordClick);
            RegistrationCommand = new DelegateCommand(RegistrationCommandClick);
            //ReSetPasswordCommand = new DelegateCommand(ReSetPasswordCommandClick);
            LocalStorageHelper.StoreInLocalSetting("IsLogin", "false");
        }
        private async void RegistrationCommandClick()
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.ResistrationPage());
        }
        /*private async void   ReSetPasswordCommandClick()
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.ReSetPasswordPage());
        }*/

        
        private async void ForgotPasswordClick()
        {

            if (!string.IsNullOrWhiteSpace(Username))
            {
                Dictionary<string, object> PostParam = new Dictionary<string, object>();
                PostParam.Add("User_Nm", Username);
                PostParam.Add("Entity_Cd", "");
                PostParam.Add("StrMode", "I");
                PostParam.Add("Type", "CLIENT");
                PostParam.Add("Crtd_Ip_Addr", "::1");
                ServiceRequestHelper _WebRequestHelper = new ServiceRequestHelper();
                string url = AppConstants.HostName + AppConstants.forgotPasword;
                ServiceRequestHelper.WebServiceResult result = await _WebRequestHelper.SendJsonDataPostRequest(url, PostParam);

                if (result != null && result.status == 200)
                {
                    RootObjectForget root = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObjectForget>(result.Result);

                    List<ErrorObjForget> errorcode = root.ErrorObj;

                    if (errorcode != null && errorcode.Count > 0)
                    {
                        var valChecksuccessorfail = errorcode[0];
                        if (valChecksuccessorfail.ErrorMessage == "Success" && valChecksuccessorfail.ErrorCode == "0")
                        {

                            XFToast.ShortMessage("A Mail will be sent to your Register Email id with a link to reset your password.");
                            IsProcessing = false;
                        }
                        else
                        {
                            IsProcessing = false;
                            XFToast.ShortMessage("Please Enter Valid Client Code");
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
                IsProcessing = false;
            }
            else
            {
                IsProcessing = false;
                XFToast.ShortMessage("Please Enter Client Code");
            }

        }

        public async void BackButtonCommandClick()
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.LoginPage());
        }

     /* private async void LoginButtonCommandClick()
        {

            if (string.IsNullOrWhiteSpace(Username))
            {
                XFToast.ShortMessage("please enter Client Code");
                return;
            }

            else if (string.IsNullOrWhiteSpace(Password))
            {
                XFToast.ShortMessage("please enter password");
                return;
            }
            IsProcessing = true;

            Dictionary<string, object> PostParam = new Dictionary<string, object>();
            PostParam.Add("User_Nm", Username);
            PostParam.Add("Password", Password);
            PostParam.Add("Type", "Client");

            ServiceRequestHelper _WebRequestHelper = new ServiceRequestHelper();


            string url = AppConstants.HostName + AppConstants.ValidateEntityCredential;
            ServiceRequestHelper.WebServiceResult result = await _WebRequestHelper.SendJsonDataPostRequest(url, PostParam);
            if (result != null && result.status == 200)
            {
                RootObject root = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(result.Result);

                List<ErrorObj> errorcode = root.ErrorObj;
                if (errorcode != null && errorcode.Count > 0)
                {
                    ErrorObj valChecksuccessorfail = errorcode[0];
                    if (valChecksuccessorfail.ErrorMessage == "Success" && valChecksuccessorfail.ErrorCode == "0")
                    {
                        if (LocalStorageHelper.RetriveFromLocalSetting("IspasswordReseted") == "true")
                        {
                            ResponseObj Responseresult = root.ResponseObj;
                            List<UserDetail> lsl = Responseresult.EntityCredentialObj;
                            Session.userDetail = lsl[0];
                            Session.lastlogin = LocalStorageHelper.RetriveFromLocalSetting("lastlogin");
                            LocalStorageHelper.StoreInLocalSetting("lastlogin", DateTime.Now.ToString());

                            var userdetail = Newtonsoft.Json.JsonConvert.SerializeObject(Session.userDetail);
                            LocalStorageHelper.StoreInLocalSetting("userDetail", userdetail);
                            LocalStorageHelper.StoreInLocalSetting("IsLogin", "true");
                            Session.username = _username;
                            await NavigationService.PushAsync(new ViewPage.Dashboardpage());
                            IsProcessing = false;
                        }
                        else
                        {
                            await NavigationService.PushAsync(new ViewPage.ReSetPasswordPage());
                            IsProcessing = false;
                        }
                    }
                    else
                    {
                        IsProcessing = false;
                        XFToast.ShortMessage("Please enter valid credential.");
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
            IsProcessing = false;
        }*/



    }


    //No need of these below classes
    public class ErrorObjForget1
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class EntityCredentialObjForget1
    {
        public double Id { get; set; }
        public string Email { get; set; }
    }

    public class ResponseObjForget1
    {
        public List<EntityCredentialObjForget> EntityCredentialObj { get; set; }
    }
    public class RootObjectForget1
    {
        public List<ErrorObjForget> ErrorObj { get; set; }
        public ResponseObjForget ResponseObj { get; set; }
    }

}
