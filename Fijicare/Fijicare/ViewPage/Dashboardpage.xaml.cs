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
    public partial class Dashboardpage : ContentPage
    {
        DashboardVeiwModel dashboardVeiwModel;
       public Dashboardpage()
        {
            InitializeComponent();
            dashboardVeiwModel = new DashboardVeiwModel(Navigation);
            this.BindingContext = dashboardVeiwModel;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //dashboardVeiwModel.GetMenuItems();
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                bool result = await this.DisplayAlert("Alert!", "Do you really want to exit?", "Yes", "No");
                if (result)
                {
                    // await this.Navigation.PopAsync(); // or anything else
                    // Session.is_logout = false;
                    Helpers.XFCloseApplication.CloseApplication();

                }

            });
            return true;
        }

    }
}
