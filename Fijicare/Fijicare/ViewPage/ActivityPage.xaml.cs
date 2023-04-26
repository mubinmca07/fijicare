using System;
using System.Collections.Generic;
using Fijicare.ViewModel;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;

namespace FijiCareViews
{
    public partial class ActivityPage : ContentPage
    {
        private ActivityViewModel activityViewModel;
        public ActivityPage(string title)
        {
            InitializeComponent();
            titlename.Text = title;
            prevBtn.Text = "<";
            nextBtn.Text = ">";

          BindingContext =  activityViewModel = new ActivityViewModel(Navigation);

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            DateTime baseDate = DateTime.Today;

            var today = baseDate;
            var yesterday = baseDate.AddDays(-1);
            DateTime startdate = baseDate;
            DateTime endDate = DateTime.Now;

            endDate  = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);

            long interval = 86400000/24;
            await activityViewModel.GetStepCountData(baseDate, endDate, interval);
            await activityViewModel.DrawLineChart();
        }
    }
}
