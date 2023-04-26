using Fijicare.Model.Login;
using Fijicare.Utility;

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Fijicare
{
    public partial class App : Application
    {

        public static string path = DependencyService.Get<Fijicare.Interfaces.IContentPath>().GetContentPath();
        

        public static long StepCount { get; internal set; }
        public static long SensorStepCount { get; internal set; }
        public static DateTime SelectedDate { get; internal set; }
        public static bool GettingDataFromGoogle { get; internal set; }

        public  App()
        {
            InitializeComponent();
            if (string.IsNullOrWhiteSpace(LocalStorageHelper.RetriveFromLocalSetting("IsLogin")))
            {       
                MainPage = new NavigationPage(new Fijicare.ViewPage.WelcomepPage());
            }
            else
            {

                if (!string.IsNullOrWhiteSpace(LocalStorageHelper.RetriveFromLocalSetting("userDetail")))
                {
                  var userdetail =  LocalStorageHelper.RetriveFromLocalSetting("userDetail");
                  Session.userDetail =  Newtonsoft.Json.JsonConvert.DeserializeObject<UserDetail>(userdetail);
                  Session.UserId = LocalStorageHelper.RetriveFromLocalSetting(AppConstants.USERID);
                }

                if (LocalStorageHelper.RetriveFromLocalSetting("IsLogin") == "true")
                {
                    MainPage = new NavigationPage(new Fijicare.ViewPage.Dashboardpage());
                }
                else
                {
                    MainPage = new NavigationPage(new Fijicare.ViewPage.WelcomepPage());
                }
            }


            //  LocalStorageHelper.StoreInLocalSetting("IsLogin", "true");
            //Session.UserId = LocalStorageHelper.RetriveFromLocalSetting(AppConstants.USERID);
        


        }

    

    protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static string AuthSucces { get;  set; }
    }
}


////testing

/*
using Fijicare.Model.Login;
using Fijicare.Utility;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FijiCare
{
    public partial class App : Application
    {

        public static string path = DependencyService.Get<Fijicare.Interfaces.IContentPath>().GetContentPath();

        public App()
        {
            InitializeComponent();

            if (string.IsNullOrWhiteSpace(LocalStorageHelper.RetriveFromLocalSetting("IsLogin")))
            {
                MainPage = new TransitionNavigationPage(new ViewPage.ProfilePage());
                
            }
            else
            {
                MainPage = new TransitionNavigationPage(new ViewPage.ProfilePage());

                
            }


            



        }



        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}*/
