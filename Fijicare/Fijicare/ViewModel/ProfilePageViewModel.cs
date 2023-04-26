using System.Collections.Generic;
using System.Text.RegularExpressions;
using Fijicare.Commands;
using Fijicare.Helpers;
using Fijicare.Model;
using Fijicare.Utility;
using Fijicare.Model.Profile;
using Xamarin.Forms;

namespace Fijicare.ViewModel
{
    public class ProfilePageViewModel : ModelBase
    {
        protected INavigation NavigationService { get; private set; }
        public DelegateCommand<string> BackCommand { get; set; }
        public DelegateCommand RegisterButtonCommand { get; set; }
        public DelegateCommand LoginButtonCommand { get; set; }
        public DelegateCommand NotificationCommand { get; set; }
        public DelegateCommand LogoutButtonCommand { get; set; }



        private bool _IsProcessing = false;
        public bool IsProcessing
        {
            get { return _IsProcessing; }
            set { SetProperty(ref _IsProcessing, value); }
        }

        private bool _IsMr;
        public bool IsMr
        {
            get { return _IsMr; }
            set
            {

                SetProperty(ref _IsMr, value);
                if (_IsMr)
                {
                    UserFofile.Title = "Mr.";
                    IsMs = false;
                    IsMrs = false;
                }
            }
        }


        private bool _IsMs;
        public bool IsMs
        {
            get { return _IsMs; }
            set
            {
                SetProperty(ref _IsMs, value);

                if (_IsMs)
                {
                    UserFofile.Title = "Ms.";
                    IsMrs = false;
                    IsMr = false;
                }
            }
        }

        private bool _IsMrs;
        public bool IsMrs
        {
            get { return _IsMrs; }
            set
            {

                SetProperty(ref _IsMrs, value);
                if (_IsMrs)
                {
                    UserFofile.Title = "Mrs.";
                    IsMs = false;
                    IsMr = false;
                }
            }
        }

        private EntityCredentialObj _UserFofile;
        public EntityCredentialObj UserFofile
        {
            get { return _UserFofile; }
            set { SetProperty(ref _UserFofile, value); }
        }



        // api/MobileApp/CrudEntityUpdateProfile
        public ProfilePageViewModel(INavigation navigationService)
        {
            this.NavigationService = navigationService;
            UserFofile = new EntityCredentialObj();
            LoginButtonCommand = new DelegateCommand(LoginButtonCommandClick);
            RegisterButtonCommand = new DelegateCommand(RegisterButtonCommandClick);
            BackCommand = new DelegateCommand<string>(BackCommandClick);
            NotificationCommand = new DelegateCommand(NotificationCommandClick);
            LogoutButtonCommand = new DelegateCommand(LogoutButtonClicked);
            GetProfileDetail();


        }

        private async void   setTitle(string title)
        {
            title = title.ToLower().Trim().Replace(".", "");
            IsMr = false;
            IsMs = false;
            IsMrs = false;
            if (title == "mr")
            {
                IsMr = true;
            }
            else if (title == "ms")
            {
                IsMs = true;
            }
            else
            {
                IsMrs = true;
            }
        }
        private async void GetProfileDetail()
        {
            IsProcessing = true;

            Dictionary<string, object> PostParam = new Dictionary<string, object>();
            PostParam.Add("StrMode", "G");
            if (Session.userDetail == null)
            {
                PostParam.Add("User_Nm", "10033576");
            }
            else
            {
                PostParam.Add("User_Nm", Session.userDetail.Entity_Cd);
            }
            PostParam.Add("Type", "client");

            ServiceRequestHelper _WebRequestHelper = new ServiceRequestHelper();
            string url = AppConstants.HostName + AppConstants.getProfile;
            ServiceRequestHelper.WebServiceResult result = await _WebRequestHelper.SendJsonDataPostRequest(url, PostParam);
            if (result != null && result.status == 200)
            {
                ProfileRoot root = Newtonsoft.Json.JsonConvert.DeserializeObject<ProfileRoot>(result.Result);
                List<ErrorObj> errorcode = root.ErrorObj;
                if (errorcode != null && errorcode.Count > 0)
                {
                    ErrorObj valChecksuccessorfail = errorcode[0];
                    if (valChecksuccessorfail.ErrorMessage == "Success" && valChecksuccessorfail.ErrorCode == "0")
                    {
                        IsProcessing = false;

                        ProfileResponseObj profile = root.ResponseObj;
                        if (profile.EntityCredentialObj.Count > 0)
                        {
                            UserFofile = profile.EntityCredentialObj[0];
                            setTitle(UserFofile.Title==null?"": UserFofile.Title);
                        }


                    }
                    else
                    {
                        IsProcessing = false;
                        XFToast.ShortMessage("" + valChecksuccessorfail.ErrorMessage);
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
        private async void RegisterButtonCommandClick()
        {


            if (string.IsNullOrWhiteSpace(UserFofile.First_Nm))
            {
                XFToast.ShortMessage("please enter first Name");
                return;
            }


            else if (string.IsNullOrWhiteSpace(UserFofile.Last_Nm))
            {
                XFToast.ShortMessage("please enter last Name");
                return;
            }
            else if (string.IsNullOrEmpty(UserFofile.Mobile1))
            {

                XFToast.ShortMessage("please enter Mobile no");
                return;
            }

            else if (string.IsNullOrEmpty(UserFofile.Email1))
            {

                XFToast.ShortMessage("please enter email");
                return;
            }

            else if (!IsValidEmail(UserFofile.Email1))
            {

                XFToast.ShortMessage("please enter valid email id");
                return;
            }

            IsProcessing = true;
            Dictionary<string, object> PostParam = new Dictionary<string, object>();
            PostParam.Add("StrMode", "U");
            PostParam.Add("User_Nm", UserFofile.Entity_Cd);
            PostParam.Add("Type", "client");
            PostParam.Add("Title", UserFofile.Title);
            PostParam.Add("First_Nm", UserFofile.First_Nm);
            PostParam.Add("Last_Nm", UserFofile.Last_Nm);
            PostParam.Add("Address_Line1", UserFofile.Address_Line1);
            PostParam.Add("Email1", UserFofile.Email1);
            PostParam.Add("Mobile1", UserFofile.Mobile1);
            PostParam.Add("Crtd_Ip_Addr", "::1");

            ServiceRequestHelper _WebRequestHelper = new ServiceRequestHelper();


            string url = AppConstants.HostName + AppConstants.getProfile;
            ServiceRequestHelper.WebServiceResult result = await _WebRequestHelper.SendJsonDataPostRequest(url, PostParam);
            if (result != null && result.status == 200)
            {
                RootObjectUpd root = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObjectUpd>(result.Result);

                List<ErrorObj> errorcode = root.ErrorObj;
                if (errorcode != null && errorcode.Count > 0)
                {
                    ErrorObj valChecksuccessorfail = errorcode[0];
                    if (valChecksuccessorfail.ErrorMessage == "Success" && valChecksuccessorfail.ErrorCode == "0")
                    {
                        IsProcessing = false;

                        XFToast.ShortMessage("Profile update successfully");


                    }
                    else
                    {
                        IsProcessing = false;
                        XFToast.ShortMessage("" + valChecksuccessorfail.ErrorMessage);
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
        private async void   BackCommandClick(string obj)
        {
             await  NavigationService.PopAsync();
        }
        private async void   NotificationCommandClick()
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.NotificationPage());

        }
        private async void   LogoutButtonClicked()
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.LoginPage());

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