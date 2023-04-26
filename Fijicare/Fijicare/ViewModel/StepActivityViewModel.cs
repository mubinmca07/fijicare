using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fijicare.Dbhelper;
using Fijicare.GoogleFitServices;
using Fijicare.Helper;
using Fijicare.Model;
using Fijicare.Model.Response;
using Fijicare.ViewModel;
using Fijicare.Helpers;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;

namespace Fijicare.ViewModel
{
    public class StepActivityViewModel : BaseViewModel
    {
        
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
        public Command<int> DayWiseCommand { get; set; }
        


        public int TotalStep;

        private INavigation Navigation;
        public StepActivityViewModel(INavigation navigation)
        {
            Navigation = navigation;
            WeekWiseStepData = new WeekWiseStepData();
            DayWiseCommand = new Command<int>(DayWiseCommandClick);
        }


        public async void DayWiseCommandClick(int day)
        {
            IsBusy = true;
            try
            {
                int _step = 0;

                switch (day)
                {
                    case 0:
                        _step = WeekWiseStepData.Sun;
                        await webViewElement.EvaluateJavaScriptAsync("DrawChart(" + WeekWiseStepData.Sun + ",'Step Count')");
                        Heartpoints = WeekWiseStepData.SunHeart.ToString();
                        Calories = WeekWiseStepData.SunCal.ToString();
                        Distance = WeekWiseStepData.SunDistance.ToString();
                        WeekWiseStepData.SunSelectDay = day;
                        break;
                    case 1:
                        _step = WeekWiseStepData.Mon;
                        await webViewElement.EvaluateJavaScriptAsync("DrawChart(" + WeekWiseStepData.Mon + ",'Step Count')");
                        Heartpoints = WeekWiseStepData.MonHeart.ToString();
                        Calories = WeekWiseStepData.MonCal.ToString();
                        Heartpoints = WeekWiseStepData.MonHeart.ToString();
                        Distance = WeekWiseStepData.MonDistance.ToString();
                        WeekWiseStepData.MonSelectDay = day;
                        break;
                    case 2:
                        _step = WeekWiseStepData.Tue;
                        await webViewElement.EvaluateJavaScriptAsync("DrawChart(" + WeekWiseStepData.Tue + ",'Step Count')");
                        Heartpoints = WeekWiseStepData.TueHeart.ToString();
                        Calories = WeekWiseStepData.TueCal.ToString();
                        Distance = WeekWiseStepData.TueDistance.ToString();
                        WeekWiseStepData.TueSelectDay = day;

                        break;
                    case 3:
                        _step = WeekWiseStepData.Wed;
                        await webViewElement.EvaluateJavaScriptAsync("DrawChart(" + WeekWiseStepData.Wed + ",'Step Count')");
                        Heartpoints = WeekWiseStepData.WedHeart.ToString();
                        Calories = WeekWiseStepData.WedCal.ToString();
                        Distance = WeekWiseStepData.WedDistance.ToString();
                        WeekWiseStepData.WedSelectDay = day;
                        break;
                    case 4:
                        _step = WeekWiseStepData.Thu;
                        await webViewElement.EvaluateJavaScriptAsync("DrawChart(" + WeekWiseStepData.Thu + ",'Step Count')");
                        Heartpoints = WeekWiseStepData.ThuHeart.ToString();
                        Calories = WeekWiseStepData.ThuCal.ToString();
                        Distance = WeekWiseStepData.ThuDistance.ToString();
                        WeekWiseStepData.ThuSelectDay = day;
                        break;
                    case 5:
                        _step = WeekWiseStepData.Fri;
                        await webViewElement.EvaluateJavaScriptAsync("DrawChart(" + WeekWiseStepData.Fri + ",'Step Count')");
                        Heartpoints = WeekWiseStepData.FriHeart.ToString();
                        Calories = WeekWiseStepData.FriCal.ToString();
                        Distance = WeekWiseStepData.FriDistance.ToString();
                        WeekWiseStepData.FriSelectDay = day;
                        break;
                    case 6:
                        _step = WeekWiseStepData.Sat;
                        await webViewElement.EvaluateJavaScriptAsync("DrawChart(" + WeekWiseStepData.Sat + ",'Step Count')");
                        Heartpoints = WeekWiseStepData.SatHeart.ToString();
                        Calories = WeekWiseStepData.SatCal.ToString();
                        Distance = WeekWiseStepData.SatDistance.ToString();
                        WeekWiseStepData.SatSelectDay = day;
                        break;
                    default:
                        // code block
                        break;
                }
                if (SelectedDate.Date.CompareTo(DateTime.Today.Date) == 0)
                {
                    if(_step==0)
                    await webViewElement.EvaluateJavaScriptAsync("DrawChart(" + Settings.SensorSteps + ",'Step Count')");
                }
            }
            catch (Exception ex)
            {

            }

            IsBusy = false;
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
            DateTime sunday = Helper.Utility.FirstDateInWeek(DateTime.Now.AddDays(1));

            var baseDate = new DateTime(sunday.Year, sunday.Month, sunday.Day, 0, 0, 1); ;

            long interval = 86400000;
            var WeekStart = baseDate;

            var WeekEnd = WeekStart.AddDays(7).AddSeconds(-1);
            await GetStepCountData(WeekStart, WeekEnd, interval);

        }



        public async Task GetStepCountData(DateTime startdate, DateTime endDate, long interval)
        {
            IsBusy = true;
            await GetStepCountDataByGoogleFit(startdate, endDate, interval);


            IsBusy = false;
        }



        public async Task GetStepCountDataByGoogleFit(DateTime startdate, DateTime endDate, long interval)
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
                    WeekWiseStepData = await weekWiseActivityCalculator.InitialiseWeekWiseData(index, dataset, WeekWiseStepData,"Step");
                    StepColletion[index] = 0;
                    index++;
                }
            }

            IsBusy = false;
        }
    }
}
