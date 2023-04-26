using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Fijicare.Commands;
using Fijicare.Helper;
using Fijicare.Model;
using Fijicare.Utility;
using FijiCareViews;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Fijicare.ViewModel
{
    public class DashboardVeiwModel : ModelBase
    {
        protected INavigation NavigationService { get; private set; }

        public DelegateCommand HamburgerMenuClick { get; set; }
        public DelegateCommand LogoutButtonCommand { get; set; }
        private string _username = "";
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

        private string _client;
        public string Client
        {
            get { return _client; }
            set { SetProperty(ref _client, value); }
        }
        private bool _IsPressent;
        public bool IsPressent
        {
            get { return _IsPressent; }
            set { SetProperty(ref _IsPressent, value); }
        }
        private bool _IsBusy;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set { SetProperty(ref _IsBusy, value); }
        }
        

        private ObservableCollection<MenuItemList> _MenuItems;
        public ObservableCollection<MenuItemList> MenuItems
        {
            get { return _MenuItems; }
            set
            {
                SetProperty<ObservableCollection<MenuItemList>>(ref _MenuItems, value);
            }
        }
        private string _lastLogin = "";
        public string LastLogin
        {
            get { return _lastLogin; }
            set { SetProperty(ref _lastLogin, value); }
        }
        public DelegateCommand<string> MainCommand { get; set; } 
        public DelegateCommand NotificationCommand { get; set; } 
        public DelegateCommand<string> FooterCommand { get; set; }
        public Page page { get; set; }
        public DashboardVeiwModel(INavigation navigationService)
        {
            Session.IsComeFromDownLoad = false;

            this.NavigationService = navigationService;
            HamburgerMenuClick = new DelegateCommand(HamburgerMenuClicked);
            LogoutButtonCommand = new DelegateCommand(LogoutButtonClicked);

            MainCommand = new DelegateCommand<string>(MainCommand_Click);
            FooterCommand = new DelegateCommand<string>(Footer_Click);
            NotificationCommand = new DelegateCommand(NotificationCommandClick);

            MenuItems = new ObservableCollection<MenuItemList>();
            if (Session.userDetail != null)
            {
                Username = Session.userDetail.Entity_Nm;
            }
            else
            {
               // Username = "10000224";
            }

            if (!string.IsNullOrEmpty(Session.lastlogin))
            {
                    // "Sun, Mar 9, 2008"
              DateTime dt = Convert.ToDateTime(Session.lastlogin);
              this.LastLogin =  "Last Login : "+$"{dt:MMM d, yyyy}";
            }
            else
            {
                DateTime dt = DateTime.Now;
                this.LastLogin = "Last Login : " + $"{dt:MMM d, yyyy}";
            }
            
        GetMenuItems();
           // LoginButtonCommand = new DelegateCommand(LoginButtonCommandClick);
        }

        private async void NotificationCommandClick()
        {

           await NavigationService.PushAsync(new Fijicare.ViewPage.NotificationPage());

        }

        private async void Footer_Click(string obj)
        {
            Session.PageParameter = obj;


            if (obj == "PHARMACY")
            {
               await NavigationService.PushAsync(new Fijicare.ViewPage.PharmecyPage());
            }
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
               await NavigationService.PushAsync(new ViewPage.WelcomepPage());
            }
            if (obj == "EMERGENCY")
            {
               await NavigationService.PushAsync(new Fijicare.ViewPage.EmergencyContacts());
            }

        }
        
        private async void MainCommand_Click(string obj)
        {
            Session.IsComeFromDownLoad = false;
            Session.IsComeFromPolicy = false;
            IsPressent = false;
            switch (obj)
            {

                case "Claims":
                    Session.PageParameter = obj;
                    Session.IsComeFromDownLoad = true;
                    await NavigationService.PushAsync(new ViewPage.PolicyListPage());
                  
                    break;
                case "Insured":
                    Session.IsPolicyDoc = false;
                    Session.IsComeFromDownLoad = false;
                    Session.PageParameter = obj;
                   await NavigationService.PushAsync(new ViewPage.PolicyListPage());
                    break;
                case "Policy":
                    Session.PageParameter = obj;
                    Session.IsComeFromDownLoad = true;
                    Session.IsComeFromPolicy = true;
                   await NavigationService.PushAsync(new ViewPage.PolicyListPage());
                    break;
                case "Benifit":
                    Session.PageParameter = "Benefit";
                    Session.IsComeFromDownLoad = true;
                   await NavigationService.PushAsync(new ViewPage.PolicyListPage());
                    break;
                case "Help":
                    Session.PageParameter = obj;
                    Session.IsComeFromDownLoad = true;
                   await NavigationService.PushAsync(new Fijicare.ViewPage.HelpPage());
                    break;
                case "Download":
                    Session.PageParameter = obj;
                    Session.IsComeFromDownLoad = true;
                   await NavigationService.PushAsync(new ViewPage.PolicyListPage());
                    break;
                case "Upload":

                    Session.PageParameter = obj;
                    Session.IsComeFromDownLoad = true;
                   await NavigationService.PushAsync(new Fijicare.ViewPage.UploadDocPage());
                    break;

                case "fitData":

                    Session.PageParameter = obj;
                    Session.IsComeFitData = true;
                    Fijicare.App.AuthSucces = string.Empty;
                    await NavigationService.PushAsync(new FitDashBoard());

                    // new FitDashBoard(NavigationService);
                    // await NavigationService.PushAsync();
                    // await NavigationService.PushAsync(new FitDashBoard());
                    break;
               /* case "data":

                    Session.PageParameter = obj;
                    Session.IsComeFromDownLoad = true;
                    // await NavigationService.PushAsync(new FitMainPage());
                    await NavigationService.PushAsync(new DataPage());
                    break;*/
            }

        }
        public string AuthentiCationUrl = Constant.BASE_API_AUTH;
        private JsonSerializer _serializer = new JsonSerializer();

        private string _AuthToken;
        public string AuthToken
        {
            get => _AuthToken;

            set { SetProperty<string>(ref _AuthToken, value); }
        }

        async Task GoogleFitAuth()
        {

            string scheme = "Google";
            AuthToken = string.Empty;
            Fijicare.App.SelectedDate = DateTime.Now.Date;
            try
            {
                WebAuthenticatorResult r = null;

                if (scheme.Equals("Apple")
                    && DeviceInfo.Platform == DevicePlatform.iOS
                    && DeviceInfo.Version.Major >= 13)
                {
                    r = await AppleSignInAuthenticator.AuthenticateAsync();
                }
                else if (scheme.Equals("Google"))
                {
                    Fijicare.App.SelectedDate = DateTime.Now.Date;
                    var authUrl = new Uri(AuthentiCationUrl + scheme);
                    var callbackUrl = new Uri("xamarinessentials://");
                    r = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);
                }

                AuthToken = r?.AccessToken ?? r?.IdToken;

            }
            catch (TaskCanceledException ex)
            {
              AuthToken = string.Empty;

                await App.Current.MainPage.DisplayAlert("Error ", ex.Message, "Ok");
            }

            catch (Exception ex)
            {
                AuthToken = string.Empty;

                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
            }
            GetUserInfoUsingToken(AuthToken);
            
        }

        private async void GetUserInfoUsingToken(string authToken)
        {

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://www.googleapis.com/oauth2/v3/");
            var httpResponseMessage = await httpClient.GetAsync("tokeninfo?access_token=" + authToken);
            using (var stream = await httpResponseMessage.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var json = new JsonTextReader(reader))
            {
                JsonSerializer _serializer = new JsonSerializer();
                var jsoncontent = _serializer.Deserialize<GoogleResponseModel>(json);
                //  Preferences.Set("UserTokenFijiCare", authToken);
                Settings.Authorization = authToken;

                Settings.Expired_in = long.Parse(jsoncontent.expires_in == null ? "0" : jsoncontent.expires_in);
                Settings.token_time = DateTime.Now.ToString();
                if (Settings.Expired_in > 0)
                {

                    await NavigationService.PushAsync(new FitDashBoard());
                }
                else
                {
                    await page.DisplayAlert("Fijicare Auth", "Please try again", "Retry");
                }
            }

        }

        private async void LogoutButtonClicked()
        {
           await NavigationService.PushAsync(new ViewPage.LoginPage());

        }


        public void GetMenuItems()
        {
            MenuItemList policy = new MenuItemList();
            policy.Name = "POLICY";
            policy.Parameter = "Policy";
            policy.Icon = "IC_Policy";
            MenuItems.Add(policy);

            MenuItemList insured = new MenuItemList();
            insured.Name = "UPLOAD";
            insured.Parameter = "Insured";
            insured.Icon = "IC_UploadDoc";
            MenuItems.Add(insured);

            MenuItemList claims = new MenuItemList();
            claims.Name = "CLAIMS";
            claims.Parameter = "Claims";
            claims.Icon = "IC_Claims";
            MenuItems.Add(claims);

            MenuItemList download = new MenuItemList();
            download.Name = "E-CARD";
            download.Parameter = "Download";
            download.Icon = "Ic_Download";
            MenuItems.Add(download);

            MenuItemList policyBenifit = new MenuItemList();
            policyBenifit.Name = "POLICY BENEFIT";
            policyBenifit.Parameter = "Benifit";
            policyBenifit.Icon = "Ic_Policy_enefits";
            MenuItems.Add(policyBenifit);

            MenuItemList selfHelp = new MenuItemList();
            selfHelp.Name = "SELF HELP";
            selfHelp.Parameter = "Help";
            selfHelp.Icon = "Ic_Self_Help";
            MenuItems.Add(selfHelp);

           // MenuItemList fit = new MenuItemList();
          //  fit.Name = "MY FITTNESS";
            //fit.Parameter = "fitData";
            //fit.Icon = "IC_fittnes";
           // MenuItems.Add(fit);

/*
            MenuItemList SqlData = new MenuItemList();
            SqlData.Name = "Sql Data";
            SqlData.Parameter = "data";
            SqlData.Icon = "Ic_Self_Help";
            MenuItems.Add(SqlData);*/

            //MenuItemList upload = new MenuItemList();
            //selfHelp.Name = "UPLOAD";
            //selfHelp.Parameter = "Upload";
            //selfHelp.Icon = "Ic_Self_Help";
            //MenuItems.Add(upload);

            /*
              case "Claims":
                    Session.PageParameter = obj;
                   await NavigationService.PushAsync(new ViewPage.PolicyListPage());
                    break;
                case "Insured":
                    Session.PageParameter = obj;
                   await NavigationService.PushAsync(new ViewPage.PolicyListPage());
                    break;
                case "Policy":
                    Session.PageParameter = obj;
                   await NavigationService.PushAsync(new ViewPage.PolicyListPage());
                    break;
                case "Benifit":
                    Session.PageParameter = obj;
                   await NavigationService.PushAsync(new ViewPage.PolicyListPage());
                    break;
                case "":
                    Session.PageParameter = obj;
                   await NavigationService.PushAsync(new Fijicare.ViewPage.HelpPage());
                    break;
                case "":
                    Session.PageParameter = obj;
                   await NavigationService.PushAsync(new ViewPage.PolicyListPage());
                    break;
            }*/

        }

        private async void   HamburgerMenuClicked()
        {
            if (IsPressent == false)
            {
                IsPressent = true;
            }
            else
            {
                IsPressent = false;
            }
        }



    }
}
