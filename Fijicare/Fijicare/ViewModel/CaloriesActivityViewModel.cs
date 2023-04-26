using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fijicare.Model;
using Fijicare.ViewModel;
using Fijicare.GoogleFitServices;
using Fijicare.Helper;

using Fijicare.Model.Response;

using Fijicare.Helpers;
using Microcharts;
using Xamarin.Forms;
namespace Fijicare.ViewModel
{
    public class CaloriesActivityViewModel : BaseViewModel
    {
       

        private long _Step;
        public long Step
        {
            get { return _Step; }
            set { SetProperty<long>(ref _Step, value); }
        }

        private string _Heartpoints;
        public string Heartpoints
        {
            get { return _Heartpoints; }
            set { SetProperty<string>(ref _Heartpoints, value); }
        }
        private string _Calories;
        public string Calories
        {
            get { return _Calories; }
            set { SetProperty<string>(ref _Calories, value); }
        }

        private DateTime _SelectedDate;
        public DateTime SelectedDate
        {
            get { return _SelectedDate; }
            set { SetProperty<DateTime>(ref _SelectedDate, value);
               
            }
             
        }
        private String _SelectedDate_string;
        public String SelectedDate_string
        {
            get { return _SelectedDate_string; }
            set
            {
                SetProperty<String>(ref _SelectedDate_string, value);

            }

        }
        private string _Distance;
        public string Distance
        {
            get { return _Distance; }
            set { SetProperty<string>(ref _Distance, value); }
        }

        public float[] StepColletion;
        private RadialGaugeChart _RadialGaugeGraph;
        public RadialGaugeChart RadialGaugeGraph
        {
            get { return _RadialGaugeGraph; }
            set { SetProperty<RadialGaugeChart>(ref _RadialGaugeGraph, value); }
        }
        private WeekWiseStepData _WeekWiseStepData;
        public WeekWiseStepData WeekWiseStepData
        {
            get { return _WeekWiseStepData; }
            set { SetProperty<WeekWiseStepData>(ref _WeekWiseStepData, value); }
        }
        public Command<int> DayWiseCommand { get; set; }



        public int TotalStep;

        private INavigation Navigation;
        public CaloriesActivityViewModel(INavigation navigation)
        {
            Navigation = navigation;
            WeekWiseStepData = new WeekWiseStepData();
            DayWiseCommand = new Command<int>(DayWiseCommandClick);
        }


        public async void DayWiseCommandClick(int day)
        {
          
            IsBusy = true;
            switch (day)
            {
                case 0:
                    WeekWiseStepData.SunSelectDay = day;
                   
                    Heartpoints = WeekWiseStepData.SunHeart.ToString();
                    Calories = WeekWiseStepData.SunCal.ToString();
                    Step = WeekWiseStepData.Sun;
                    Distance = WeekWiseStepData.SunDistance.ToString();
                     await webViewElement.EvaluateJavaScriptAsync("DrawChart(" + WeekWiseStepData.SunCal + ",'Calories')");
                    break;
                case 1:
                    WeekWiseStepData.MonSelectDay = day;
                    
                    Heartpoints = WeekWiseStepData.MonHeart.ToString();
                    Calories = WeekWiseStepData.MonCal.ToString();
                    Heartpoints = WeekWiseStepData.MonHeart.ToString();
                    Step = WeekWiseStepData.Mon;
                    Distance = WeekWiseStepData.MonDistance.ToString();
                     await webViewElement.EvaluateJavaScriptAsync("DrawChart(" + WeekWiseStepData.MonCal + ",'Calories')");
                    break;
                case 2:
                    WeekWiseStepData.TueSelectDay = day;
                   
                    Heartpoints = WeekWiseStepData.TueHeart.ToString();
                    Calories = WeekWiseStepData.TueCal.ToString();
                    Step = WeekWiseStepData.Tue;
                    Distance = WeekWiseStepData.TueDistance.ToString();
                     await webViewElement.EvaluateJavaScriptAsync("DrawChart(" + WeekWiseStepData.TueCal + ",'Calories')");

                    break;
                case 3:
                    WeekWiseStepData.WedSelectDay = day;
                   
                    Heartpoints = WeekWiseStepData.WedHeart.ToString();
                    Calories = WeekWiseStepData.WedCal.ToString();
                    Step = WeekWiseStepData.Wed;
                    Distance = WeekWiseStepData.WedDistance.ToString();
                     await webViewElement.EvaluateJavaScriptAsync("DrawChart(" + WeekWiseStepData.WedCal + ",'Calories')");
                    break;
                case 4:
                    WeekWiseStepData.ThuSelectDay = day;
                   
                    Heartpoints = WeekWiseStepData.ThuHeart.ToString();
                    Calories = WeekWiseStepData.ThuCal.ToString();
                    Step = WeekWiseStepData.Thu;
                    Distance = WeekWiseStepData.ThuDistance.ToString();
                     await webViewElement.EvaluateJavaScriptAsync("DrawChart(" + WeekWiseStepData.ThuCal + ",'Calories')");

                    break;
                case 5:
                    WeekWiseStepData.FriSelectDay = day;
                    
                    Heartpoints = WeekWiseStepData.FriHeart.ToString();
                    Calories = WeekWiseStepData.FriCal.ToString();
                    Step = WeekWiseStepData.Fri;
                    Distance = WeekWiseStepData.FriDistance.ToString();
                     await webViewElement.EvaluateJavaScriptAsync("DrawChart(" + WeekWiseStepData.FriCal + ",'Calories')");
                    break;
                case 6:
                    WeekWiseStepData.SatSelectDay = day;
                    
                    Heartpoints = WeekWiseStepData.SatHeart.ToString();
                    Calories = WeekWiseStepData.Sat.ToString();
                    Step = WeekWiseStepData.Sat;
                    Distance = WeekWiseStepData.SatDistance.ToString();
                     await webViewElement.EvaluateJavaScriptAsync("DrawChart(" + WeekWiseStepData.SatCal + ",'Calories')");
                    break;
                default:
                    // code block
                    break;
            }
            IsBusy = false;

            if (day == (int)DateTime.Today.DayOfWeek)
                Step = Settings.SensorSteps;
           // });
        }


        int CurrentMonthVall = 0;
        int CurrentVal = 0;
        string TypeOfCommandParam;
        internal WebView webViewElement;

        public async Task GetWeekWiseData(int currentVal)
        {
            DateTime sunday = Fijicare.Helper.Utility.FirstDateInWeek(DateTime.Now.AddDays(1));

            var baseDate = new DateTime(sunday.Year, sunday.Month, sunday.Day, 0, 0, 1); ;

            long interval = 86400000;
            var WeekStart = baseDate;

            var WeekEnd = WeekStart.AddDays(7).AddSeconds(-1);
            await GetStepCountData(WeekStart, WeekEnd, interval);

        }

        public async Task GetStepCountData(DateTime startdate, DateTime endDate, long interval)
        {
            IsBusy = true;
            Settings.APISecure = "1";
            TotalStep = 0;

            List<string> streamIds = new List<string>();
            streamIds.Add("derived:com.google.step_count.delta:com.google.android.gms:estimated_steps");
            // streamIds.Add("derived:com.google.calories.expended:com.google.android.gms:from_activities");
            streamIds.Add("derived:com.google.calories.expended:com.google.android.gms:platform_calories_expended");
    
                streamIds.Add("derived:com.google.distance.delta:com.google.android.gms:merge_distance_delta");
            streamIds.Add("derived:com.google.heart_minutes:com.google.android.gms:merge_heart_minutes");
            long startmilliseconds = new DateTimeOffset(startdate).ToUnixTimeMilliseconds();
            long startlongTime = new DateTimeOffset(endDate).ToUnixTimeMilliseconds();
            var result = await GoogleService.GetBucketCountData(startmilliseconds, startlongTime, interval, streamIds.ToArray());

            if (result != null && result.bucket != null && result.bucket.Count > 0)
            {
                StepColletion = new float[result.bucket.Count];
                int index = 0;
                WeekWiseActivityCalculator weekWiseActivityCalculator = new WeekWiseActivityCalculator(StepColletion);  
                foreach (var item in result.bucket)
                {
                    var dataset = (List<Dataset>)item.dataset;
                    WeekWiseStepData  =  await weekWiseActivityCalculator.InitialiseWeekWiseData(index, dataset, WeekWiseStepData, "cal");
                    StepColletion[index] = 0;
                    index++;
                }
            }

            IsBusy = false;
        }


        
    }
}
