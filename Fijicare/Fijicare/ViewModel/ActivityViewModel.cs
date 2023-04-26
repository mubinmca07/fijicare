using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Fijicare.GoogleFitServices;
using Fijicare.Helper;
using Fijicare.Model.Response;
using FijiCareUtility;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
//using Xamarin.Forms;

namespace Fijicare.ViewModel
{
    public class ActivityViewModel : BaseViewModel
    {

        public float[] StepColletion;
        
        private Xamarin.Forms.INavigation Navigation;
        public DelegateCommand<string> PrevCommand { get; set; }
        public DelegateCommand<string> NextCommand { get; set; }
        public DelegateCommand<string> TypeOfCommand { get; set; }

        public ActivityViewModel(Xamarin.Forms.INavigation navigation)
        {
            this.Navigation = navigation;
            BarChartGraph = new BarChart();
            TypeOfCommand = new DelegateCommand<string>(TypeOfCommandClick);
            PrevCommand = new DelegateCommand<string>(PrevCommandClick);
            NextCommand = new DelegateCommand<string>(NextCommandClick);
        }

        private async void PrevCommandClick(string obj)
        {
            if(TypeOfCommandParam=="W")
            {
                CurrentVal = CurrentVal - 1;
                CurrentVal = CurrentVal == 0 ? -1 : CurrentVal;

                 GetWeekWiseData(CurrentVal);
            }
            else if(TypeOfCommandParam == "M")
            {
                CurrentMonthVall = CurrentMonthVall - 1;
                CurrentMonthVall = CurrentMonthVall == 0 ? -1 : CurrentMonthVall;
                GetMonthWiseData(CurrentMonthVall, false);
            }
        }

        private async void   NextCommandClick(string obj)
        {
            if (TypeOfCommandParam == "W")
            {
                CurrentVal = CurrentVal + 1;
                CurrentVal = CurrentVal == 0 ? 1 : CurrentVal;

                GetWeekWiseData(CurrentVal);
            }
            else if (TypeOfCommandParam == "M")
            {
                CurrentMonthVall = CurrentMonthVall + 1;
                CurrentMonthVall = CurrentMonthVall == 0 ? 1 : CurrentMonthVall;
                GetMonthWiseData(CurrentMonthVall, true);
            }
        }


        int CurrentMonthVall = 0;

        int CurrentVal = 0;

        private async void GetWeekWiseData(int currentVal)
        {       
            DateTime baseDate = DateTime.Today;
            var today = baseDate;
            var yesterday = baseDate.AddDays(-1);
            long interval = 86400000;
            if (currentVal > 0)
            {
                var CurrentWeekStart = baseDate.AddDays(-(int)baseDate.DayOfWeek);
                var CurrentWeekEnd = CurrentWeekStart.AddDays(7).AddSeconds(-1);
                DisplayCurrentInt = CurrentWeekStart.ToString() + "--" + CurrentWeekEnd.ToString();
                await GetStepCountData(CurrentWeekStart, CurrentWeekEnd, interval);
                await DrawWeekWiseData();
            }
            else
            {

                var WeekStart = baseDate.AddDays(-(int)baseDate.DayOfWeek + 7 * (currentVal));
                var WeekEnd = WeekStart.AddDays(7).AddSeconds(-1);
                DisplayCurrentInt = WeekStart.ToString() + "--" + WeekEnd.ToString();
                await GetStepCountData(WeekStart, WeekEnd, interval);
                await DrawWeekWiseData();
            }
        }


        DateTime CurrentMonthStart;
        DateTime CurrentMonthEnd;

        private async void GetMonthWiseData(int currentMonthVal, bool IsNext)
        {     
            DateTime baseDate = DateTime.Today;
            var today = baseDate;
            var yesterday = baseDate.AddDays(-1);

            long interval = 86400000;

            if (currentMonthVal > 0)
            {
                CurrentMonthStart = baseDate.AddDays(-(int)baseDate.Day + 1);
                CurrentMonthEnd = CurrentMonthStart.AddMonths(1).AddSeconds(-1);
                DisplayCurrentInt = CurrentMonthStart.ToString() + "--" + CurrentMonthEnd.ToString();
                await GetStepCountData(CurrentMonthStart, CurrentMonthEnd, interval);
                await DrawMontwiseWiseData();
            }
            else
            {
                CurrentMonthStart = CurrentMonthStart.AddMonths(IsNext ? 1 : -1);
                CurrentMonthEnd = CurrentMonthStart.AddMonths(1).AddSeconds(-1);

                DisplayCurrentInt = CurrentMonthStart.ToString() + "--" + CurrentMonthEnd.ToString();
                await GetStepCountData(CurrentMonthStart, CurrentMonthEnd, interval);
                await DrawMontwiseWiseData();
            }
        }


        private string _DisplayCurrentInt;
        public string DisplayCurrentInt
        {
            get { return _DisplayCurrentInt; }

            set { SetProperty<string>(ref _DisplayCurrentInt, value); }
        }
        private Xamarin.Forms.Color _HColor;
        public Xamarin.Forms.Color HColor
        {
            get { return _HColor; }

            set { SetProperty<Xamarin.Forms.Color>(ref _HColor, value); }
        }

        private Xamarin.Forms.Color _DColor;
        public Xamarin.Forms.Color DColor
        {
            get { return _DColor; }

            set { SetProperty<Xamarin.Forms.Color>(ref _DColor, value); }
        }
        private Xamarin.Forms.Color _WColor;
        public Xamarin.Forms.Color WColor
        {
            get { return _WColor; }

            set { SetProperty<Xamarin.Forms.Color>(ref _WColor, value); }
        }

        private Xamarin.Forms.Color _MColor;
        public Xamarin.Forms.Color MColor
        {
            get { return _MColor; }

            set { SetProperty<Xamarin.Forms.Color>(ref _MColor, value); }
        }

        private Xamarin.Forms.Color _YColor;
        public Xamarin.Forms.Color YColor
        {
            get { return _YColor; }

            set { SetProperty<Xamarin.Forms.Color>(ref _YColor, value); }
        }

        private BarChart _BarChartGraph;
        public BarChart BarChartGraph
        {
            get { return _BarChartGraph; }

            set { SetProperty<BarChart>(ref _BarChartGraph, value) ; }
        }

        private long _TotalStep;
        public long TotalStep
        {
            get { return _TotalStep; }

            set { SetProperty<long>(ref _TotalStep, value); }
        }

        public async Task GetStepCountData(DateTime startdate, DateTime endDate, long interval)
        {
            Settings.APISecure = "1";
            TotalStep = 0;
            string streamId = "derived:com.google.step_count.delta:com.google.android.gms:estimated_steps";
            long startmilliseconds = new DateTimeOffset(startdate).ToUnixTimeMilliseconds();
            long startlongTime = new DateTimeOffset(endDate).ToUnixTimeMilliseconds();
            var result = await GoogleService.GetBucketCountData(startmilliseconds, startlongTime, interval, streamId);

            // StepColletion = new ObservableCollection<float>();

            if (result != null && result.bucket != null && result.bucket.Count > 0)
            {
                StepColletion = new float[result.bucket.Count];
                int index = 0;
                foreach (var item in result.bucket)
                {
                    var dataset = (List<Dataset>)item.dataset;
                    StepColletion[index] = 0;
                    if (dataset != null && dataset.Count > 0)
                    {
                        foreach (var inner in dataset)
                        {
                            var Points = (List<Model.Response.Point>)inner.point;

                            if (dataset != null && dataset.Count > 0)
                            {
                                foreach (var point in Points)
                                {
                                    // StepColletion.Add(point.value[0].intVal);
                                    TotalStep += point.value[0].intVal;
                                    StepColletion[index] = point.value[0].intVal;
                                    //DebugHelper.WriteLine( point.dataTypeName+"---mubin-------"+point.value[0].intVal);
                                }
                            }
                        }
                    }
                    index++;
                }
            }

           //await DrawLineChart();
        }

        public async Task DrawMontwiseWiseData()
        {
            List<ChartEntry> entries = new List<ChartEntry>();
            if (StepColletion != null && StepColletion.Length > 0)
            {
                int i = 31;
                foreach (var item in StepColletion)
                {

                    string lbl = i.ToString() + " ";
                    i++;
                    entries.Add(new ChartEntry(item)
                    {
                        Label = lbl,
                        ValueLabel = item.ToString(),
                        Color = SKColor.Parse("#3498db")
                    });
                }
            }
            BarChartGraph.Entries = entries.ToArray();
        }

        public async Task DrawWeekWiseData()
        {
            List<ChartEntry> entries = new List<ChartEntry>();
            if (StepColletion != null && StepColletion.Length > 0)
            {
                int i = 1;
                foreach (var item in StepColletion)
                {

                    string lbl = i.ToString() + " ";
                    i++;
                    entries.Add(new ChartEntry(item)
                    {
                        Label = lbl,
                        ValueLabel = item.ToString(),
                        Color = SKColor.Parse("#3498db")
                    });
                }
            }
            BarChartGraph.Entries = entries.ToArray();
        }
        public async Task DrawLineChart()
        {
            List<ChartEntry> entries = new List<ChartEntry>();
            if (StepColletion != null && StepColletion.Length > 0)
            {
                int i = 0;
                foreach (var item in StepColletion)
                {
                    string am = i < 12 ? "AM" : "PM";
                    string lbl = (i % 12).ToString() + " " + am;
                    i++;
                    entries.Add(new ChartEntry(item)
                    {
                        Label = lbl,
                        ValueLabel = item.ToString(),
                        Color = SKColor.Parse("#3498db")
                    });
                }
            }
            BarChartGraph.Entries = entries.ToArray();
        }
        string TypeOfCommandParam;
        private async void TypeOfCommandClick(string obj)
        {
            if (IsBusy) return;
            IsBusy = true;
            TypeOfCommandParam = obj;
            DoAllUnselect();
            if(obj=="H")
            {
                HColor = Xamarin.Forms.Color.FromHex("#e7614c");
            }
            else if (obj == "D")
            {
                DColor = Xamarin.Forms.Color.FromHex("#e7614c");
            }
            else if (obj == "W")
            {
                CurrentVal = -1;
                GetWeekWiseData(CurrentVal);
                WColor = Xamarin.Forms.Color.FromHex("#e7614c");
            }
            else if (obj == "M")
            {
                MColor = Xamarin.Forms.Color.FromHex("#e7614c");
                CurrentMonthVall = 1;
                GetMonthWiseData(CurrentMonthVall, false);
            }
            else if (obj == "Y")
            {
                YColor = Xamarin.Forms.Color.FromHex("#e7614c");
            }
            IsBusy = false;
        }

        private async void DoAllUnselect()
        {
            HColor = Xamarin.Forms.Color.Transparent;
            DColor = Xamarin.Forms.Color.Transparent;
            WColor = Xamarin.Forms.Color.Transparent;
            MColor = Xamarin.Forms.Color.Transparent;
            YColor = Xamarin.Forms.Color.Transparent;
        }

    }
}
