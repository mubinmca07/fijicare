using System;
using System.Collections.Generic;
using Fijicare.ViewModel;
using Xamarin.Forms;



namespace Fijicare.ViewPage.Activity
{
    
    public partial class CaloriesActivity : ContentPage
    {
        private CaloriesActivityViewModel caloriesActivityViewModel;
        private DateTime startdate;
        private DateTime endDate;
        private DateTime selcteddate;
        private String SelectedDate_string;
        public CaloriesActivity(string title, DateTime startdate, DateTime endDate, DateTime selcteddate)
        {
            InitializeComponent();
           
            this.startdate = startdate;
            this.selcteddate = selcteddate;
            this.SelectedDate_string = selcteddate.ToString("dddd,dd,MMM,yyyy");
            this.endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
            
            caloriesActivityViewModel = new CaloriesActivityViewModel(Navigation);
            this.BindingContext = caloriesActivityViewModel;
            caloriesActivityViewModel.webViewElement = webViewElement;
            Title = title;
            contentgrd.IsVisible = false;
            caloriesActivityViewModel.SelectedDate = selcteddate;
            caloriesActivityViewModel.SelectedDate_string = SelectedDate_string;
        }

        public CaloriesActivity()
        {
            InitializeComponent();
            
            caloriesActivityViewModel = new CaloriesActivityViewModel(Navigation);
            this.BindingContext = caloriesActivityViewModel;
            caloriesActivityViewModel.webViewElement = webViewElement;
        }

       

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (Fijicare.Helper.Utility.TokenExpired())
            {
                await Navigation.PopAsync();
                return;
            }
            if (caloriesActivityViewModel == null)
            {
                caloriesActivityViewModel = new CaloriesActivityViewModel(Navigation);
                this.BindingContext = caloriesActivityViewModel;
            }


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
            contentgrd.IsVisible = false;

            caloriesActivityViewModel.IsBusy = true;
            
            webViewElement.Navigated += WebViewElement_Navigated;
            //await caloriesActivityViewModel.GetWeekWiseData(0);
            long interval = 86400000;
            await caloriesActivityViewModel.GetStepCountData(startdate, endDate, interval);
            contentgrd.IsVisible = true;
           
            caloriesActivityViewModel.DayWiseCommandClick((int)selcteddate.DayOfWeek);

            caloriesActivityViewModel.IsBusy = false;
        }

        private async void WebViewElement_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if(!isDayWiseCommandclick)
            {
                caloriesActivityViewModel.DayWiseCommandClick((int)selcteddate.DayOfWeek);
            }
        }
        bool isDayWiseCommandclick;
        bool IsDayWiseCommand;
        async void DayWiseCommand(System.Object sender, System.EventArgs e)
        {
            if (IsDayWiseCommand) return;
            isDayWiseCommandclick = true;
            IsDayWiseCommand = true;
            var param = (TappedEventArgs)e;
            if (param == null) return;
            if (int.TryParse(param.Parameter.ToString(), out int weekDay))
            {
                //Fijicare.App.SelectedDate = startdate.AddDays(weekDay);
                caloriesActivityViewModel.SelectedDate_string = startdate.AddDays(weekDay).ToString("dddd,dd,MMM,yyyy");
                caloriesActivityViewModel.DayWiseCommandClick(weekDay);
            }
            IsDayWiseCommand = false;

        }
    }
}
