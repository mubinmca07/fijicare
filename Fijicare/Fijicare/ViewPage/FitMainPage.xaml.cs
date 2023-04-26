using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
//using Android.Webkit;

using Fijicare.Helper;
using Fijicare.Model;
using FijiCareViews;

using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace Fijicare.ViewPage
{

    public partial class FitMainPage : ContentPage
    {
        string authenticationUrl = Constant.BASE_API_AUTH;
        private JsonSerializer _serializer = new JsonSerializer();

        private string _AuthToken;
        public string AuthToken
        {
            get => _AuthToken;
            set
            {
                if (value == _AuthToken) return;
                _AuthToken = value;
                OnPropertyChanged();
            }
        }

        public FitMainPage()
        {
            InitializeComponent();
            this.BindingContext = this;
            Button_Clicked(null, null);
           
            IsBusy = true;
        }


        async void Button_Clicked(System.Object sender, System.EventArgs e)
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
                    var authUrl = new Uri(authenticationUrl + scheme);
                    var callbackUrl = new Uri("xamarinessentials://");

                    r = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);
                }
                

                AuthToken = r?.AccessToken ?? r?.IdToken;
                GetUserInfoUsingToken(AuthToken);
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
                
                var jsoncontent = _serializer.Deserialize<GoogleResponseModel>(json);
                //  Preferences.Set("UserTokenFijiCare", authToken);
                Settings.Authorization = authToken;
                
                Settings.Expired_in = long.Parse(jsoncontent.expires_in == null ? "0" : jsoncontent.expires_in);
                Settings.token_time = DateTime.Now.ToString();
                if (Settings.Expired_in > 0)
                {
                    /*var cookieManager = CookieManager.Instance;
                    cookieManager.RemoveAllCookie();

                    cookieManager.RemoveSessionCookie();
                    cookieManager.Flush();*/
                    /* WebAuthenticatorResult r = null;
                     var authUrl = new Uri("https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?");
                     var callbackUrl = new Uri("xamarinessentials://");

                     r = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);

                     AuthToken = r?.AccessToken ?? r?.IdToken;
                     GetUserInfoUsingToken(AuthToken);
                     */
                    //OpenLogoutPage(new Uri("http://localhost/clear_cookies.html"));
                     await Navigation.PushAsync(new FitDashBoard());
                }
                else
                {
                    //var cookieManager = CookieManager.Instance;
                    //cookieManager.RemoveAllCookie();

                    //cookieManager.RemoveSessionCookie();
                    //cookieManager.Flush();


                    
                        OpenLogoutPage(new Uri("https://www.google.com"));
                    //OpenLogoutPage(new Uri("https://www.google.com/accounts/Logout"));

                    /*WebAuthenticatorResult r = null;
                    var authUrl = new Uri("https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?");
                    var callbackUrl = new Uri("xamarinessentials://");

                    r = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);

                    AuthToken = r?.AccessToken ?? r?.IdToken;
                    GetUserInfoUsingToken(AuthToken);*/

                    /* var auth = new RefreshOAuth2Authenticator(
                  clientId: "1050669903445-hksvua7afgfbpcq9pvebl8dr3aunovbe.apps.googleusercontent.com",
                  clientSecret: "pdd6VUA_fftBCFwOLDMC0Yi1",
                  scope: "scope", // just 'openid' in my case
                  authorizeUrl: new Uri("https://accounts.google.com/o/oauth2/v2/auth"),
                  redirectUrl: new Uri("https://www.Fijicare.com.fj/FijiAuthMobile/signin-google"),
                  accessTokenUrl: new Uri("https://www.googleapis.com/oauth2/v4/token"));*/


                    // auth.GetUI();
                    await DisplayAlert("Fijicare Auth", "Please try again", "Retry");
                }
                //await Navigation.PushAsync(new FitDashBoard(jsoncontent.email));
            }

        }

       
            public  async Task<bool> OpenLogoutPage(Uri logoutUri)
            {
                 await Browser.OpenAsync(logoutUri, BrowserLaunchMode.SystemPreferred);

            
                return true;
            }
        

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
           
            IsBusy = false;
            //if (!string.IsNullOrEmpty(Preferences.Get("UserTokenFijiCare", "")))
            //{
            //    Settings.Authorization = Preferences.Get("UserTokenFijiCare", "");
            //    HttpClient httpClient = new HttpClient();
            //    httpClient.BaseAddress = new Uri("https://www.googleapis.com/oauth2/v3/");
            //    var httpResponseMessage = await httpClient.GetAsync("tokeninfo?access_token=" + Settings.Authorization);
            //    using (var stream = await httpResponseMessage.Content.ReadAsStreamAsync())
            //    using (var reader = new StreamReader(stream))
            //    using (var json = new JsonTextReader(reader))
            //    {
            //        var jsoncontent = _serializer.Deserialize<GoogleResponseModel>(json);

            //        if (jsoncontent.access_type == "online")
            //        {
            //            await Navigation.PushAsync(new FitDashBoard(jsoncontent.email));
            //        }
            //        else
            //        {
            //             return;
            //        }

            //    }

            //}
            //else
            //{
            //   // GoogleLogin();
            //}

        }

        async void GoogleLogin()
        {
            string scheme = "Google";

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
                    var authUrl = new Uri(authenticationUrl + scheme);
                    var callbackUrl = new Uri("xamarinessentials://");

                    r = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);

                }

                AuthToken = r?.AccessToken ?? r?.IdToken;
                GetUserInfoUsingToken(AuthToken);
            }
            catch (Exception ex)
            {
                AuthToken = string.Empty;

                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
            }
        }


        protected override bool OnBackButtonPressed()
        {
            var _navigation = Application.Current.MainPage.Navigation;
            _navigation.PopToRootAsync();
            return false;
        }

    }
}
