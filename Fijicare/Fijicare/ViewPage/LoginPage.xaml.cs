using Fijicare.ViewModel;
using Xamarin.Forms;

namespace Fijicare.ViewPage
{
    public partial class LoginPage : ContentPage
    {
        private LoginViewModel loginViewModel;
        public LoginPage()
        {
            InitializeComponent();
            loginViewModel = new LoginViewModel(Navigation);
            this.BindingContext = loginViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
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
