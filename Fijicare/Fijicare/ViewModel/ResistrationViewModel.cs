using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Fijicare.Commands;
using Fijicare.Helpers;

using Fijicare.Utility;
using Fijicare.Model.Account;
using Xamarin.Forms;

namespace Fijicare.ViewModel
{
    public class ResistrationViewModel : Fijicare.Model.ModelBase
    {
        protected INavigation NavigationService { get; private set; }

        public DelegateCommand RegisterButtonCommand { get; set; }
        public DelegateCommand LoginButtonCommand { get; set; }


        private bool _IsProcessing = false;
        public bool IsProcessing
        {
            get { return _IsProcessing; }
            set { SetProperty(ref _IsProcessing, value); }
        }

        private string _User_Nm;
        public string User_Nm
        {
            get { return _User_Nm; }
            set { SetProperty(ref _User_Nm, value); }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set { SetProperty(ref _Password, value); }
        }

        private string _Confirm_Password;
        public string Confirm_Password
        {
            get { return _Confirm_Password; }
            set { SetProperty(ref _Confirm_Password, value); }
        }


        private string _First_Nm;
        public string First_Nm
        {
            get { return _First_Nm; }
            set { SetProperty(ref _First_Nm, value); }
        }

        private string _Email_Id;
        public string Email_Id
        {
            get { return _Email_Id; }
            set { SetProperty(ref _Email_Id, value); }
        }

        private string _Mobile_No;
        public string Mobile_No
        {
            get { return _Mobile_No; }
            set { SetProperty(ref _Mobile_No, value); }
        }

        private string _Type = "client";
        public string Type
        {
            get { return _Type; }
            set { SetProperty(ref _Type, value); }
        }
        private string _Crtd_Ip_Addr = "127.0.0.1";
        public string Crtd_Ip_Addr
        {
            get { return _Crtd_Ip_Addr; }
            set { SetProperty(ref _Crtd_Ip_Addr, value); }
        }



        public ResistrationViewModel(INavigation navigationService)
        {
            this.NavigationService = navigationService;
            RegisterButtonCommand = new DelegateCommand(RegisterButtonCommandClick);
             LoginButtonCommand = new DelegateCommand(LoginButtonCommandClick);

        }

        private async void RegisterButtonCommandClick()
        {


            if (string.IsNullOrWhiteSpace(User_Nm))
            {
                XFToast.ShortMessage("please enter Client Code.");
                return;
            }

            else if (string.IsNullOrWhiteSpace(Password))
            {
                XFToast.ShortMessage("please enter password");
                return;
            }
            else if (string.IsNullOrWhiteSpace(Confirm_Password))
            {
                XFToast.ShortMessage("please enter confirm password");
                return;
            }
            else if (!Confirm_Password.Equals(Password))
            {
                Password = string.Empty;
                Confirm_Password = string.Empty;
                XFToast.ShortMessage("password not matched");
                return;
            }

            else if (string.IsNullOrWhiteSpace(Confirm_Password))
            {
                XFToast.ShortMessage("please enter confirm password");
                return;
            }
            else if(string.IsNullOrEmpty(First_Nm))
            {

                XFToast.ShortMessage("please enter name");
                return;
            }

            else if (string.IsNullOrEmpty(Email_Id))
            {

                XFToast.ShortMessage("please enter email");
                return;
            }

            else if (!IsValidEmail(Email_Id))
            {

                XFToast.ShortMessage("please enter valid email id");
                return;
            }

            else if (string.IsNullOrEmpty(Mobile_No))
            {

                XFToast.ShortMessage("please enter mobile no.");
                return;
            }
            IsProcessing = true;
            Dictionary<string, object> PostParam = new Dictionary<string, object>();
            PostParam.Add("User_Nm", User_Nm);
            PostParam.Add("Password", Password);
            PostParam.Add("Type", "client");
            PostParam.Add("First_Nm", First_Nm);
            PostParam.Add("Email_Id", Email_Id);
            PostParam.Add("Mobile_No", Mobile_No);
            PostParam.Add("Crtd_Ip_Addr", Crtd_Ip_Addr);

            ServiceRequestHelper _WebRequestHelper = new ServiceRequestHelper();


            string url = AppConstants.HostName + AppConstants.Crud_EntityCredential;
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
                        IsProcessing = false;
                        await NavigationService.PushAsync(new Fijicare.ViewPage.LoginPage());
                        XFToast.ShortMessage("" + valChecksuccessorfail.ErrorMessage);
                        

                    }
                    else
                    {
                        IsProcessing = false;
                        XFToast.ShortMessage(""+ valChecksuccessorfail.ErrorMessage);
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
        private async void   LoginButtonCommandClick()
        {
           
            
            
                await NavigationService.PushAsync(new Fijicare.ViewPage.LoginPage());
            

        }
        bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}
