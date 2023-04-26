using System;
using System.Collections.Generic;
using Fijicare.ViewModel;
using Xamarin.Forms;

namespace Fijicare.ViewPage
{
    public partial class MainDoctor : ContentPage
    {
        MainDoctorPageViewModel _MainDoctorPageViewModel;
        public MainDoctor()
        {
            InitializeComponent();
            _MainDoctorPageViewModel = new MainDoctorPageViewModel(Navigation);
            this.BindingContext = _MainDoctorPageViewModel;

        }
    }
}
