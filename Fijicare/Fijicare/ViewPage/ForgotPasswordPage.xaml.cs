using Fijicare.ViewModel;
using Xamarin.Forms;

namespace Fijicare.ViewPage
{
    public partial class ForgotPasswordPage : ContentPage
    {
        private ForgotPasswordViewModel forgotPasswordViewModel;
        public ForgotPasswordPage()
        {
            InitializeComponent();
            forgotPasswordViewModel = new ForgotPasswordViewModel(Navigation);
            this.BindingContext = forgotPasswordViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
       /* protected override bool OnBackButtonPressed()
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
        */
    }
}
