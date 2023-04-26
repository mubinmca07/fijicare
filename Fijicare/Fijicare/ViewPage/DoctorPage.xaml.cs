using Fijicare.Helpers;
using Fijicare.Utility;
using Fijicare.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
                                              
namespace Fijicare.ViewPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DoctorPage : ContentPage
    {
        

        DoctorPageViewModel _DoctorPageViewModel;
        public DoctorPage()
        {
            InitializeComponent();
            Session.DoctorType = "";
            _DoctorPageViewModel = new DoctorPageViewModel(Navigation);
            this.BindingContext = _DoctorPageViewModel;

        }


        async void Captation_Clicked(object sender, System.EventArgs e)
        {
            Session.DoctorType = "Capitation";
            await Navigation.PushAsync(new ViewPage.MainDoctor());
        }


       async void Bulk_Billing_Clicked(object sender, System.EventArgs e)
        {
            Session.DoctorType = "Bulkbilling";
            await Navigation.PushAsync(new ViewPage.MainDoctor());
        }
    }

    //        /api/MobileApp/GetBulkCapitionProvider
    //provide search by name and all
    //For : Capitation doctor
    //        {
    //  "Category": "Capitation",
    //  "Provider_Nm": ""
    //}
    //for : Bulkbilling Doctor
    //        {
    //  "Category": "Bulkbilling",
    //  "Provider_Nm": ""
    //}

}