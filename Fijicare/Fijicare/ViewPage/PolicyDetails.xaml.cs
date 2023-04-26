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
	public partial class PolicyDetails : ContentPage
	{
        PolicyDetailVeiwModel _PolicyDetailVeiwModel;
        public PolicyDetails ()
		{
			InitializeComponent ();
            _PolicyDetailVeiwModel = new PolicyDetailVeiwModel(Navigation);
            this.BindingContext = _PolicyDetailVeiwModel;
           

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (_PolicyDetailVeiwModel != null)
            {
                _PolicyDetailVeiwModel.IsProcessing = true;
                await _PolicyDetailVeiwModel.getPolicy();
                _PolicyDetailVeiwModel.IsProcessing = false;
            }
        }
    }
}