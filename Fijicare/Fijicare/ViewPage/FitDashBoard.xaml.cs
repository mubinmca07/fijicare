using System;
using System.Collections.Generic;
using Microcharts;
using SkiaSharp;
using Xamarin.Essentials;
using Xamarin.Forms;
using Fijicare.ViewModel;
using Fijicare.Helpers;
using Fijicare.Helper;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;
using Fijicare.Model;

namespace FijiCareViews
{
    public partial class FitDashBoard : ContentPage
    {
        FitDashboardViewModel viewModel;
        public FitDashBoard()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
                Padding = new Thickness(0, 0, 0, 0);
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#00AF7A");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
          
            BindingContext = viewModel = new FitDashboardViewModel(Navigation);
            DayName.Text = DateTime.Now.ToString("dddd,MMM dd,yyyy");
            viewModel.WeekDays.Selected(DateTime.Now);
            viewModel.SelectedDate = DateTime.Now;

            //GoogleFitAuth();
        }
        public FitDashBoard(string email)
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#00AF7A");
            BindingContext = viewModel = new FitDashboardViewModel(Navigation);
            DayName.Text = DateTime.Now.ToString("dddd,MMM dd,yyyy");
            viewModel.WeekDays.Selected(DateTime.Now);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            // viewModel.SelectedDate = Fijicare.App.SelectedDate;
            viewModel.WeekDays.Selected(viewModel.SelectedDate);
            await viewModel.RadialGraph();

            if (string.IsNullOrEmpty(Fijicare.App.AuthSucces))
            {
                if (!string.IsNullOrEmpty(Settings.Authorization))
                {
                    bool result = await viewModel.GetUserInfoUsingToken();// return token expired or not
                    if (result) // To check the Token expireation
                    {
                        DataInitialization();
                    }
                    else
                    {
                        Fijicare.App.AuthSucces = string.Empty;
                        await viewModel.GoogleFitAuth();
                    }
                }
                else
                {
                    Fijicare.App.AuthSucces = string.Empty;
                    await viewModel.GoogleFitAuth();
                }
            }
            else
            {
               
                bool IsCancleOrNot = await DisplayAlert(Fijicare.App.AuthSucces, "Do you want to launch fitness dashboard?", "Yes", "No");
                if (IsCancleOrNot)
                {
                    Fijicare.App.AuthSucces = string.Empty;
                    await viewModel.GoogleFitAuth();
                }
                else
                {
                    Fijicare.App.AuthSucces = string.Empty;
                    await Navigation.PopAsync();
                }
            }
        }

        private async void DataInitialization()
        {
          
            DateTime baseDate = viewModel.SelectedDate;
            DateTime startdate = baseDate;
            DateTime endDate = viewModel.SelectedDate;

            startdate = new DateTime(startdate.Year, startdate.Month, startdate.Day, 00, 00, 01);
            endDate = new DateTime(startdate.Year, startdate.Month, startdate.Day, 23, 59, 59);
            viewModel.WeekDays.Selected(startdate);
           
            long interval = 86400000 / (24);

            await viewModel.GetAllData(startdate, endDate, interval);
            await viewModel.RadialGraph();
            if (Device.RuntimePlatform == Device.Android)
            {
                MessagingCenter.Subscribe<string>(this, "Stepcount", (sender) =>
                {
                    if (sender == "1")
                    {
                      
                        Settings.SensorSteps = Settings.SensorSteps + 1;
                        Settings.SensorSteps_time = DateTime.Now.ToString();

                        if (viewModel.SelectedDate.Date == DateTime.Now.Date && !Fijicare.App.GettingDataFromGoogle)
                        {
                            viewModel.DateTimeActivity = DateTime.Now.ToString("d/MMM hh:mm tt");
                            viewModel.StepCount = Settings.SensorSteps;
                        }
                    }

                });
            }
        }


        protected async  void OnAppearing_old()
        {
            base.OnAppearing();
            if (Fijicare.Helper.Utility.TokenExpired())
            {
                await Navigation.PopAsync();
                return;
            }

            await viewModel.RadialGraph();
            DateTime baseDate = DateTime.Today;
            DateTime startdate = baseDate;
            DateTime endDate = DateTime.Now;
            startdate = new DateTime(startdate.Year, startdate.Month, startdate.Day, 00, 00, 01);
            endDate = new DateTime(startdate.Year, startdate.Month, startdate.Day, 23, 59, 59);
            viewModel.WeekDays.Selected(startdate);
            viewModel.SelectedDate = DateTime.Now;
            long interval = 86400000 / (24);
            await viewModel.GetAllData(startdate, endDate, interval);
            await viewModel.RadialGraph();
            if (Device.RuntimePlatform == Device.Android)
            {
                MessagingCenter.Subscribe<string>(this, "Stepcount", (sender) =>
                {
                    if (sender == "1")
                    {

                        Settings.SensorSteps = Settings.SensorSteps + 1;
                        Settings.SensorSteps_time = DateTime.Now.ToString();

                        if (viewModel.SelectedDate.Date == DateTime.Now.Date && !Fijicare.App.GettingDataFromGoogle)
                        {
                            viewModel.DateTimeActivity = DateTime.Now.ToString("d/MMM hh:mm tt");
                            viewModel.StepCount = Settings.SensorSteps;
                        }




                    }

                });
            }

        }

        async void ActivityClicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new ActivityPage("Activity"));
        }
        

       

        bool isWeekDaysTapped;
       async void WeekDaysTapped(System.Object sender, System.EventArgs e)
        {
            if (Fijicare.Helper.Utility.TokenExpired())
                await Navigation.PopAsync();
            if (isWeekDaysTapped) return;
            isWeekDaysTapped = true;
            if (e == null) { isWeekDaysTapped = false;  return; }


            var param = (TappedEventArgs)e;

            DateTime date = (DateTime)param.Parameter;
            viewModel.SelectedDate = date;
            DateTime startdate = new DateTime(date.Year, date.Month, date.Day, 0, 0, 1); ;
            viewModel.WeekDays.Selected(startdate);
          
            DayName.Text = date.ToString("dddd,MMM dd,yyyy");
            DateTime endDate = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);

            long interval = 86400000;
            await viewModel.GetAllData(startdate, endDate, interval);
           
            await viewModel.RadialGraph();
            isWeekDaysTapped = false;
        }


       

    }
}
