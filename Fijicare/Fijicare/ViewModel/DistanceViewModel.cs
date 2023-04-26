using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fijicare.ViewModel;
using Fijicare.GoogleFitServices;
using Fijicare.Helper;
using Fijicare.Model;
using Fijicare.Model.Response;
using Fijicare.Helpers;
using Microcharts;
using Xamarin.Forms;
namespace Fijicare.ViewModel
{
    public class DistanceViewModel : BaseViewModel
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

        private DateTime _SelectedDate;
        public DateTime SelectedDate
        {
            get { return _SelectedDate; }
            set { SetProperty<DateTime>(ref _SelectedDate, value); }
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

        private string _Calories;
        public string Calories
        {
            get { return _Calories; }
            set { SetProperty<string>(ref _Calories, value); }
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
        public DistanceViewModel(INavigation navigation)
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
                    await webViewElement.EvaluateJavaScriptAsync("DrawChart(" + WeekWiseStepData.SunDistance + ",'Distance')");
                    Heartpoints = WeekWiseStepData.SunHeart.ToString();
                    Calories = WeekWiseStepData.SunCal.ToString();
                    Distance = WeekWiseStepData.SunDistance.ToString();
                    Step = WeekWiseStepData.Sun;
                    break;
                case 1:
                    await webViewElement.EvaluateJavaScriptAsync("DrawChart(" + WeekWiseStepData.MonDistance + ",'Distance')");
                    Heartpoints = WeekWiseStepData.MonHeart.ToString();
                    Calories = WeekWiseStepData.MonCal.ToString();
                    Heartpoints = WeekWiseStepData.MonHeart.ToString();
                    Distance = WeekWiseStepData.MonDistance.ToString();
                    WeekWiseStepData.MonSelectDay = day;
                    Step = WeekWiseStepData.Mon;
                    break;
                case 2:
                    await webViewElement.EvaluateJavaScriptAsync("DrawChart(" + WeekWiseStepData.TueDistance + ",'Distance')");
                    Heartpoints = WeekWiseStepData.TueHeart.ToString();
                    Calories = WeekWiseStepData.TueCal.ToString();
                    Distance = WeekWiseStepData.TueDistance.ToString();
                    Step = WeekWiseStepData.Tue;
                    WeekWiseStepData.TueSelectDay = day;
                    break;
                case 3:
                    await webViewElement.EvaluateJavaScriptAsync("DrawChart(" + WeekWiseStepData.WedDistance + ",'Distance')");
                    Heartpoints = WeekWiseStepData.WedHeart.ToString();
                    Calories = WeekWiseStepData.WedCal.ToString();
                    Distance = WeekWiseStepData.WedDistance.ToString();
                    Step = WeekWiseStepData.Wed;
                    WeekWiseStepData.WedSelectDay = day;
                    break;
                case 4:
                    await webViewElement.EvaluateJavaScriptAsync("DrawChart(" + WeekWiseStepData.ThuDistance + ",'Distance')");
                    Heartpoints = WeekWiseStepData.ThuHeart.ToString();
                    Calories = WeekWiseStepData.ThuCal.ToString();
                    Distance = WeekWiseStepData.ThuDistance.ToString();
                    Step = WeekWiseStepData.Thu;
                    WeekWiseStepData.ThuSelectDay = day;
                    break;
                case 5:
                    await webViewElement.EvaluateJavaScriptAsync("DrawChart(" + WeekWiseStepData.FriDistance + ",'Distance')");
                    Heartpoints = WeekWiseStepData.FriHeart.ToString();
                    Calories = WeekWiseStepData.FriCal.ToString();
                    Distance = WeekWiseStepData.FriDistance.ToString();
                    Step = WeekWiseStepData.Fri;
                    WeekWiseStepData.FriSelectDay = day;
                    break;
                case 6:
                    await webViewElement.EvaluateJavaScriptAsync("DrawChart(" + WeekWiseStepData.SatDistance + ",'Distance')");
                    Heartpoints = WeekWiseStepData.SatHeart.ToString();
                    Calories = WeekWiseStepData.SatCal.ToString();
                    Distance = WeekWiseStepData.SatDistance.ToString();
                    Step = WeekWiseStepData.Sat;
                    WeekWiseStepData.SatSelectDay = day;
                    break;
                default:
                    // code block
                    break;
            }
            IsBusy = false;

            if (day == (int)DateTime.Today.DayOfWeek)
                Step = (long)Settings.SensorSteps;
        }

        void initial()
        {

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
            //streamIds.Add("derived:com.google.calories.expended:com.google.android.gms:from_activities");
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
                    WeekWiseStepData = await weekWiseActivityCalculator.InitialiseWeekWiseData(index, dataset, WeekWiseStepData, "Distance");
                    StepColletion[index] = 0;
                    index++;
                }
            }

            IsBusy = false;
        }
    }
}
