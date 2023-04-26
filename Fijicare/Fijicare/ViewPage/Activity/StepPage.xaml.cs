using System;
using System.Collections.Generic;
using Fijicare.Interfaces;
using Fijicare.ViewModel;
using Microcharts;
using Xamarin.Forms;

namespace Fijicare.ViewPage.Activity
{
    public partial class StepPage : ContentPage
    {
        private StepActivityViewModel stepActivityViewModel;
        private DateTime startdate;
        private DateTime endDate;
        private DateTime selcteddate;
        private String SelectedDate_string;
        public StepPage(string title, DateTime startdate, DateTime endDate,DateTime selcteddate)
        {
            InitializeComponent();
            this.startdate = startdate;
            this.selcteddate = selcteddate;
            this.SelectedDate_string = selcteddate.ToString("dddd,dd,MMM,yyyy");
            this.endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day,23,59,59);
            stepActivityViewModel = new StepActivityViewModel(Navigation);
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
            htmlSource.BaseUrl = DependencyService.Get<IBaseUrl>().Get();
            webViewElement.Source = htmlSource;
            webViewElement.Navigated += WebViewElement_Navigated;
            this.BindingContext = stepActivityViewModel;
            stepActivityViewModel.webViewElement = webViewElement;
            Title = title;
            contentgrd.IsVisible = false;
            stepActivityViewModel.SelectedDate = selcteddate;
            stepActivityViewModel.SelectedDate_string = SelectedDate_string;
        }

       
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (Fijicare.Helper.Utility.TokenExpired())
                await Navigation.PopAsync();
            if (stepActivityViewModel == null)
            {
                stepActivityViewModel = new StepActivityViewModel(Navigation);
                this.BindingContext = stepActivityViewModel;
            }
            stepActivityViewModel.IsBusy = true;
            long interval = 86400000;
            await stepActivityViewModel.GetStepCountData(startdate,endDate, interval);
            contentgrd.IsVisible = true;
            
              stepActivityViewModel.DayWiseCommandClick((int)selcteddate.DayOfWeek);
           
           
            stepActivityViewModel.IsBusy = false;
        }

        private async void   WebViewElement_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (!isDayWiseCommandclick)
            {
                stepActivityViewModel.DayWiseCommandClick((int)selcteddate.DayOfWeek);
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
                //stepActivityViewModel.SelectedDate = new DateTime(this.selcteddate);

                //Fijicare.App.SelectedDate = startdate.AddDays(weekDay);
                stepActivityViewModel.SelectedDate_string = startdate.AddDays(weekDay).ToString("dddd,dd,MMM,yyyy");
                stepActivityViewModel.DayWiseCommandClick(weekDay);

            }
            IsDayWiseCommand = false;

        }
    }
}
