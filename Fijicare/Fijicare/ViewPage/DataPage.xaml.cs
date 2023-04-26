using System;
using System.Linq;
using Fijicare.Commands;
using Fijicare.Dbhelper;
using Fijicare.Interfaces;
using Fijicare.ViewModel;
using Xamarin.Forms;

namespace Fijicare.ViewPage
{
    public partial class DataPage : ContentPage
    {
        public DataPage()
        {
            InitializeComponent();
            if (DependencyService.Get<IStepCounter>().IsAvailable())
            {
                DependencyService.Get<IStepCounter>().InitSensorService();
                myBtn.IsVisible = true;
                this.BindingContext = new DataPageVeiwModel(Navigation);
            }
        }

        async void StepCounter_Clicked(System.Object sender, System.EventArgs e)
        {
            StepCounterDatabase stepCounterDatabase = await StepCounterDatabase.Instance;
            var items = await stepCounterDatabase.GetItemsAsync();
            list.ItemsSource = items.OrderByDescending(o => o.ID);
        }

        protected override bool OnBackButtonPressed()
        {
            Application.Current.MainPage = new NavigationPage(new Fijicare.ViewPage.Dashboardpage());
            return base.OnBackButtonPressed();
        }
    }

    public  class DataPageVeiwModel : BaseViewModel
    {
        INavigation Navigation;
        public DelegateCommand<string> BackCommand;
        public DataPageVeiwModel(INavigation navigation)
        {
            Navigation = navigation;
            BackCommand = new DelegateCommand<string>(BackCommandClick);
        }

        private void BackCommandClick(string obj)
        {
            Application.Current.MainPage = new NavigationPage(new Fijicare.ViewPage.Dashboardpage());
        }

        private async void LogoutButtonClicked()
        {
            await Navigation.PushAsync(new Fijicare.ViewPage.LoginPage());

        }
    }

}
