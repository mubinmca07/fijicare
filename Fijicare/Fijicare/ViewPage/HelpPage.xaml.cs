using Fijicare.Commands;
using Fijicare.Helpers;
using Fijicare.Model;
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
    
    public partial class HelpPage : ContentPage
    {
       HelpViewModel _helpViewModel;
        public HelpPage()
        {
            InitializeComponent();
            _helpViewModel = new HelpViewModel(Navigation);
            this.BindingContext = _helpViewModel;
        }
    }

    /*public class HelpViewModel : ModelBase
    {
        public DelegateCommand<string> BackCommand { get; set; }
        protected INavigation NavigationService { get; set; }
        public DelegateCommand LogoutButtonCommand { get; set; }

        public string PageParameter { get; set; }
        public HelpViewModel(INavigation _NavigationService)
        {
            this.NavigationService = _NavigationService;
            BackCommand = new DelegateCommand<string>(BackCommandClicked);
            LogoutButtonCommand = new DelegateCommand(LogoutButtonClicked);


        }

        private async void   LogoutButtonClicked()
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.LoginPage());

        }

        private async void   BackCommandClicked(string obj)
        {
             await  NavigationService.PopAsync();
        }

    }*/
}