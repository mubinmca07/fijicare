using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Fijicare.Commands;
using Fijicare.GoogleFitServices;
using Fijicare.Helper;
using Fijicare.Model;
using Fijicare.ViewPage.Activity;
using Microcharts;
using Newtonsoft.Json;
using SkiaSharp;
using Xamarin.Essentials;
using Xamarin.Forms;
using Point = Fijicare.Model.Response.Point;

namespace Fijicare.ViewModel
{

    public class FitDashboardViewModel : BaseViewModel
    {

        public Command LoadItemsCommand { get; set; }

        private string _PageName;
        public string PageName
        {
            get { return _PageName; }
            set { SetProperty<string>(ref _PageName, value); }
        }

        private CWeek _WeekDays;
        public CWeek WeekDays
        {
            get { return _WeekDays; }
            set { SetProperty<CWeek>(ref _WeekDays, value); }
        }


        private string _DateTimeActivity;
        public string DateTimeActivity
        {
            get { return _DateTimeActivity; }
            set { SetProperty<string>(ref _DateTimeActivity, value); }
        }

        private long _StepCount;
        public long StepCount
        {
            get { return _StepCount; }
            set { SetProperty<long>(ref _StepCount, value);
                Fijicare.App.StepCount = value;
            }

        }


       /* private long _SensorStepCount;
        public long SensorStepCount
        {
            get { return _SensorStepCount; }
            set
            {
                SetProperty<long>(ref _SensorStepCount, value);
                Fijicare.App.SensorStepCount = value;
            }

        }*/

        private decimal _CalCount;
        public decimal Calories
        {
            get { return _CalCount; }
            set { SetProperty<decimal>(ref _CalCount, value); }
        }


        private int _HeartCount;
        public int HeartPoint
        {
            get { return _HeartCount; }
            set { SetProperty<int>(ref _HeartCount, value); }
        }

        private decimal _Disance;
        public decimal Disance
        {
            get { return _Disance; }
            set { SetProperty<decimal>(ref _Disance, value); }
        }

        private DateTime _SelectedDate;
        public DateTime SelectedDate
        {
            get { return _SelectedDate; }
            set { SetProperty<DateTime>(ref _SelectedDate, value);
                Fijicare.App.SelectedDate = value;
            }
        }


        private ObservableCollection<int> stepColletion;
        public ObservableCollection<int> StepColletion
        {
            get { return stepColletion; }
            set { SetProperty<ObservableCollection<int>>(ref stepColletion, value); }
        }
        private RadialGaugeChart _RadialGaugeGraph;
        public RadialGaugeChart RadialGaugeGraph
        {
            get { return _RadialGaugeGraph; }
            set { SetProperty<RadialGaugeChart>(ref _RadialGaugeGraph, value); }
        }
        public Command<string> StepCommand { get; set;}
        public Command<string> WeekDaysCommand { get; set; }
        public Command<string> PrevCommand { get; set; }
        public Command<string> NextCommand { get; set; }
        private INavigation Navigation;

        public DelegateCommand<string> BackCommand { get; set; }
        
        public DelegateCommand LogoutButtonCommand { get; set; }
        public FitDashboardViewModel(INavigation navigation)
        {
            Navigation = navigation;
            WeekDays = new CWeek();
            BackCommand = new DelegateCommand<string>(BackCommandClick);
            PageName = "My Fittness";
             LogoutButtonCommand = new DelegateCommand(LogoutButtonClicked);
            DateTimeActivity = DateTime.Now.ToString("d/MMM hh:mm tt");
            RadialGaugeGraph = new RadialGaugeChart();
            StepCommand = new Command<string>(StepCommandlick);
           // WeekDaysCommand = new Command<string>(WeekDaysClick);
            PrevCommand = new Command<string>(PrevCommandClick);
            NextCommand = new Command<string>(NextCommandClick);
        }

        private async void BackCommandClick(string obj)
        {
            Application.Current.MainPage = new NavigationPage(new Fijicare.ViewPage.Dashboardpage());
        }
        private async void LogoutButtonClicked()
        {
            await Navigation.PushAsync(new Fijicare.ViewPage.LoginPage());

        }
        private async void PrevCommandClick(string obj)
        {
            WeekDays.PrevWeek();
        }
        private async void NextCommandClick(string obj)
        {
            WeekDays.NextWeek();
        }
    

        private async void StepCommandlick(string obj)
        {
            if (obj == "Step Count")
            {
                await Navigation.PushAsync(new StepPage("Step count", WeekDays.Sun, WeekDays.Sat, SelectedDate));
            }
            else if (obj == "Calories")
            {
                await Navigation.PushAsync(new Fijicare.ViewPage.Activity.CaloriesActivity("Calories", WeekDays.Sun, WeekDays.Sat, SelectedDate));
            }
            else if (obj == "Heart Point")
            {
                await Navigation.PushAsync(new Fijicare.ViewPage.Activity.HeartPointPage("Heart point", WeekDays.Sun, WeekDays.Sat, SelectedDate));
            }
            else if (obj == "Distance")
            {
                await Navigation.PushAsync(new Fijicare.ViewPage.Activity.DistancePage("Distance", WeekDays.Sun, WeekDays.Sat, SelectedDate));
            }
        }
        public async Task GetAllData(DateTime startdate, DateTime endDate, long interval)
        {
            try
            {
                await GetAllDataFromfit(startdate, endDate, interval);
            }
            catch { }
        }

        
        public async Task GetAllDataFromfit(DateTime startdate, DateTime endDate, long interval)
        {
            // long localstepscount = App.SensorStepCount;
            Fijicare.App.GettingDataFromGoogle = true;
            try
            {

                IsBusy = true;

                StepCount = 0; Calories = 0; HeartPoint = 0; Disance = 0;
                Settings.APISecure = "1";
                List<string> streamIds = new List<string>();

                streamIds.Add("derived:com.google.step_count.delta:com.google.android.gms:estimated_steps");
                streamIds.Add("derived:com.google.calories.expended:com.google.android.gms:platform_calories_expended");
                streamIds.Add("derived:com.google.distance.delta:com.google.android.gms:merge_distance_delta");
                streamIds.Add("derived:com.google.heart_minutes:com.google.android.gms:merge_heart_minutes");

                if (startdate.Date == DateTime.Now.Date)
                {
                    DateTimeActivity = DateTime.Now.ToString("d/MMM hh:mm tt");
                }
                else
                {
                    DateTimeActivity = endDate.ToString("d-MMM-yyyy");
                }

                long startmilliseconds = new DateTimeOffset(startdate).ToUnixTimeMilliseconds();
                long startlongTime = new DateTimeOffset(endDate).ToUnixTimeMilliseconds();
                var result = await GoogleService.GetBucketCountData(startmilliseconds, startlongTime, interval, streamIds.ToArray());

                if (result != null && result.bucket != null && result.bucket.Count > 0)
                {
                    foreach (var bucket in result.bucket)
                    {
                        var datasets = bucket.dataset;
                        List<Point> steps = datasets[0].point;
                        List<Point> calories = datasets[1].point;
                        List<Point> distance = datasets[2].point;
                        List<Point> heart_minutes = datasets[3].point;

                        foreach (var point in steps)
                        {
                            StepCount = StepCount + point.value[0].intVal;
                        }

                        foreach (var point in calories)
                        {
                            foreach (var cal in point.value)
                            {
                                Calories = Calories + Convert.ToDecimal(string.Format("{0:0.00}", (cal.fpVal)));
                            }
                        }

                        foreach (var point in distance)
                        {
                            foreach (var dis in point.value)
                            {
                                Disance = Disance + Convert.ToDecimal(string.Format("{0:0.00}", (dis.fpVal / 1000)));
                            }
                        }

                        foreach (var point in heart_minutes)
                        {
                            foreach (var heart in point.value)
                            {
                                HeartPoint += heart.intVal;
                            }
                        }
                    }

                    if (Settings.SensorSteps_time != "0" && Convert.ToDateTime(Settings.SensorSteps_time).Date < DateTime.Now.Date)
                    {
                        Settings.SensorSteps = 0;
                    }

                    if ((StepCount < Settings.SensorSteps) && (SelectedDate.Date == DateTime.Now.Date))
                    {

                        StepCount = Settings.SensorSteps;
                    }

                    if ((StepCount >= Settings.SensorSteps) && (SelectedDate.Date == DateTime.Now.Date))
                    {
                        Settings.SensorSteps = StepCount;
                    }
                }
                else if (result == null)
                {
                    string url = string.Empty;
                    bool accept = await Fijicare.App.Current.MainPage.DisplayAlert("Error ", "Please Install and register with google fit App", "Ok", "Cancel");
                    if (accept)
                    {
                        var location = RegionInfo.CurrentRegion.Name.ToLower();
                        if (Device.RuntimePlatform == Device.Android)
                            url = "https://play.google.com/store/apps/details?id=com.google.android.apps.fitness";
                        else
                            url = "https://itunes.apple.com/" + location + "/app/contractor-action-solution/id1433864494?mt=8";
                        await Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
                    }
                }
                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                await GoogleFitAuth();
              //  await Fijicare.App.Current.MainPage.DisplayAlert("Error ", ex.Message, "Ok");
            }

            Fijicare.App.GettingDataFromGoogle = false;
        }

        public async Task RadialGraph()
        {
            await Task.Delay(1);
            var spt = (StepCount * 100) / (10000);
            var cal = (float)(Calories * 100) / 10000;
            var dist = (float)(Disance * 100) / 10;
            var hrt = (HeartPoint * 100) / 100;

            var entries = new[]
            {
                new ChartEntry(cal)
                 {
                     Label = "Calories",
                     ValueLabel = cal.ToString(),
                     Color = SKColor.Parse("#77d065")
                 },
                 new ChartEntry(spt)
                 {
                     Label = "Step",
                     ValueLabel = spt.ToString(),
                     Color = SKColor.Parse("#2c3e50")
                 },

                 new ChartEntry(hrt)
                 {
                     Label = "Heart Point",
                     ValueLabel = hrt.ToString(),
                     Color = SKColor.Parse("#3498db")
                },
                 new ChartEntry(dist)
                 {
                     Label = "Distance",
                     ValueLabel = dist.ToString(),
                     Color = SKColor.Parse("#b455b6")
                 },
            };

            RadialGaugeGraph = new RadialGaugeChart()
            {
                MinValue = 0,
                MaxValue = 100,
                Entries = entries,
                BackgroundColor = SKColor.Parse("#000000")
            };

        }

        public string AuthentiCationUrl = Constant.BASE_API_AUTH;
        private JsonSerializer _serializer = new JsonSerializer();


        public string AuthToken;



       public async Task GoogleFitAuth()
        {

            string scheme = "Google";
            AuthToken = string.Empty;
            Fijicare.App.AuthSucces = string.Empty;
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
                Settings.Authorization = AuthToken;
               

            }
            catch (TaskCanceledException ex)
            {
                AuthToken = string.Empty;
                Fijicare.App.AuthSucces = "User has canceled.";
            }

            catch (Exception ex)
            {
                Fijicare.App.AuthSucces = "Authentication failed.";
                AuthToken = string.Empty;
            }
           
            

        }

        public async Task<bool> GetUserInfoUsingToken()
        {

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://www.googleapis.com/oauth2/v3/");
            var httpResponseMessage = await httpClient.GetAsync("tokeninfo?access_token=" + Settings.Authorization);
            using (var stream = await httpResponseMessage.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var json = new JsonTextReader(reader))
            {
                JsonSerializer _serializer = new JsonSerializer();
                var jsoncontent = _serializer.Deserialize<GoogleResponseModel>(json);
                //  Preferences.Set("UserTokenFijiCare", authToken);
                // Settings.Authorization = authToken;

                Settings.Expired_in = long.Parse(jsoncontent.expires_in == null ? "0" : jsoncontent.expires_in);
                Settings.token_time = DateTime.Now.ToString();
                if (Settings.Expired_in > 0)
                {
                    return true;
                    // await Navigation.PushAsync(new FitDashBoard());
                }
                else
                {
                    return false;
                    //await DisplayAlert("Fijicare Auth", "Please try again", "Retry");
                }
            }

        }

    }
}
