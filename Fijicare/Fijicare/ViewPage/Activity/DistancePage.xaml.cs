using System;
using System.Collections.Generic;
using Fijicare.ViewModel;
using Xamarin.Forms;

namespace Fijicare.ViewPage.Activity
{
    //public partial class DistancePage : ContentPage
    //{
    //    public DistancePage()
    //    {
    //        InitializeComponent();
    //    }
    //}

    public partial class DistancePage : ContentPage
    {
        private DistanceViewModel distanceViewModel;
        private DateTime startdate;
        private DateTime endDate;
        private DateTime selecteddate;
        private String Selecteddate_string;
        public DistancePage(string title, DateTime startdate, DateTime endDate,DateTime selecteddate)
        {
            InitializeComponent();
            this.startdate = startdate;
            this.selecteddate = selecteddate;
            this.Selecteddate_string = selecteddate.ToString("dddd,dd,MMM,yyyy");
            this.endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
            distanceViewModel = new DistanceViewModel(Navigation);
            this.BindingContext = distanceViewModel;
            distanceViewModel.webViewElement = webViewElement;
            Title = title;
            contentgrd.IsVisible = false;
            distanceViewModel.SelectedDate = selecteddate;
            distanceViewModel.SelectedDate_string = selecteddate.ToString("dddd,dd,MMM,yyyy");
        }

        public DistancePage()
        {
            InitializeComponent();
            contentgrd.IsVisible = false;
            distanceViewModel = new DistanceViewModel(Navigation);
            this.BindingContext = distanceViewModel;
            distanceViewModel.webViewElement = webViewElement;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (distanceViewModel == null)
            {
                distanceViewModel = new DistanceViewModel(Navigation);
                this.BindingContext = distanceViewModel;
            }
            distanceViewModel.IsBusy = true;
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = @"<html>
                                <head>
                                <script>
                                function nextpage(){
                                   self.location = 'index.html';
                                }
                                </script>
                                </head>
                                <body onload='nextpage()'>
                                </body>
                                </html>";
            htmlSource.BaseUrl = DependencyService.Get<Fijicare.Interfaces.IBaseUrl>().Get();
            webViewElement.Source = htmlSource;
            webViewElement.Navigated += WebViewElement_Navigated;
            //await distanceViewModel.GetWeekWiseData(0);
            long interval = 86400000;
            await distanceViewModel.GetStepCountData(startdate, endDate, interval);
            contentgrd.IsVisible = true;
            distanceViewModel.DayWiseCommandClick((int)selecteddate.DayOfWeek);
            /*
            distanceViewModel.Heartpoints = distanceViewModel.WeekWiseStepData.SunHeart.ToString();
            distanceViewModel.Calories = distanceViewModel.WeekWiseStepData.SunCal.ToString();

            distanceViewModel.Distance = distanceViewModel.WeekWiseStepData.SunDistance.ToString();
            await webViewElement.EvaluateJavaScriptAsync("DrawChart(" + distanceViewModel.WeekWiseStepData.SunDistance + ",'Distance')");
            */
            distanceViewModel.IsBusy = false;
        }

        private async void WebViewElement_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (!isDayWiseCommandclick)
            {
                distanceViewModel.DayWiseCommandClick((int)selecteddate.DayOfWeek);
            }
        }

        void WeekDaysTapped(System.Object sender, System.EventArgs e)
        {
        }
        bool isDayWiseCommandclick;
        bool IsDayWiseCommand;
        async void DayWiseCommand(System.Object sender, System.EventArgs e)
        {
            if (IsDayWiseCommand) return;
            IsDayWiseCommand = true;
            isDayWiseCommandclick = true;
            var param = (TappedEventArgs)e;
            if (param == null) return;
            if (int.TryParse(param.Parameter.ToString(), out int weekDay))
            {
               // Fijicare.App.SelectedDate = startdate.AddDays(weekDay);
                distanceViewModel.SelectedDate_string = startdate.AddDays(weekDay).ToString("dddd,dd,MMM,yyyy");
                distanceViewModel.DayWiseCommandClick(weekDay);
            }
            IsDayWiseCommand = false;

        }
    }
}
