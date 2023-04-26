using System;
using System.Collections.Generic;
using Fijicare.ViewModel;
using Xamarin.Forms;

namespace Fijicare.ViewPage.Activity
{
    

    public partial class HeartPointPage : ContentPage
    {
        private HeartPointViewModel heartPointViewModel;
        private DateTime startdate;
        private DateTime endDate;
        private DateTime selcteddate;
        private String SelectedDate_string;

        public HeartPointPage(string title, DateTime startdate, DateTime endDate, DateTime selcteddate)
        {
            InitializeComponent();
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
            this.startdate = startdate;
            this.selcteddate = selcteddate;
            this.SelectedDate_string = selcteddate.ToString("dddd,dd,MMM,yyyy");
            this.endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
            heartPointViewModel = new HeartPointViewModel(Navigation);
            this.BindingContext = heartPointViewModel;
            heartPointViewModel.webViewElement = webViewElement;
            Title = title;
            contentgrd.IsVisible = false;
            heartPointViewModel.SelectedDate = selcteddate;
            heartPointViewModel.SelectedDate_string = selcteddate.ToString("dddd,dd,MMM,yyyy");
        }

        public HeartPointPage()
        {
            InitializeComponent();
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
            contentgrd.IsVisible = false;
            heartPointViewModel = new HeartPointViewModel(Navigation);
            this.BindingContext = heartPointViewModel;
            heartPointViewModel.webViewElement = webViewElement;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (heartPointViewModel == null)
            {
                heartPointViewModel = new HeartPointViewModel(Navigation);
                this.BindingContext = heartPointViewModel;
            }
            heartPointViewModel.IsBusy = true;
            
            //await heartPointViewModel.GetWeekWiseData(0);
            long interval = 86400000;
            await heartPointViewModel.GetStepCountData(startdate, endDate, interval);
            contentgrd.IsVisible = true;

            heartPointViewModel.DayWiseCommandClick((int)selcteddate.DayOfWeek);

            heartPointViewModel.IsBusy = false;

        }

        private async void WebViewElement_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (!isDayWiseCommandclick)
            {
                heartPointViewModel.DayWiseCommandClick((int)selcteddate.DayOfWeek);
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
                //Fijicare.App.SelectedDate = startdate.AddDays(weekDay);
                heartPointViewModel.SelectedDate_string = startdate.AddDays(weekDay).ToString("dddd,dd,MMM,yyyy");
                heartPointViewModel.DayWiseCommandClick(weekDay);
            }
            IsDayWiseCommand = false;

        }
    }
}
