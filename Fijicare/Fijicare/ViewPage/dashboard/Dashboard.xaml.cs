using Fijicare.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fijicare.ViewPage.dashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : FlyoutPage
    {

        DashboardVeiwModel dashboardVeiwModel;
        public Dashboard()
        {
            InitializeComponent();
            dashboardVeiwModel = new DashboardVeiwModel(Navigation);
            this.BindingContext = dashboardVeiwModel;
            //Navigation.
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
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

        // }
        //    public Dashboard()
        //    {
        //        InitializeComponent();
        //        FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
        //    }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as DashboardFlyoutMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(new Dashboardpage());
            IsPresented = false;

            FlyoutPage.ListView.SelectedItem = null;
        }
    }

}