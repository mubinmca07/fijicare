using Fijicare.ViewModel;
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
	public partial class PolicyBenifit : ContentPage
	{
        BenifitViewModal _BenifitViewModal;
        public PolicyBenifit ()
		{
			InitializeComponent ();
            _BenifitViewModal = new BenifitViewModal(Navigation);
            this.BindingContext = _BenifitViewModal;
            _BenifitViewModal.IsProcessing = true;

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            _BenifitViewModal.IsProcessing = true;
        }
    }
}