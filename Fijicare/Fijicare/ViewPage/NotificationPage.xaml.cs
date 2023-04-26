using Fijicare.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fijicare.ViewPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotificationPage : ContentPage
	{
        NotificationViewModel notificationViewModel;
        public NotificationPage()
        {
            InitializeComponent();
            notificationViewModel = new NotificationViewModel(Navigation);
            this.BindingContext = notificationViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            notificationViewModel.GetNotification();
        }
    }
}