using Fijicare.ViewModel;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fijicare.ViewPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PolicyListPage : ContentPage
	{
        PolicyListVeiwModel _PolicyListVeiwModel;
        public PolicyListPage()
		{
			InitializeComponent ();
            if(_PolicyListVeiwModel==null)
            _PolicyListVeiwModel = new PolicyListVeiwModel(Navigation);
            this.BindingContext = _PolicyListVeiwModel;

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if(_PolicyListVeiwModel != null)
            {
                _PolicyListVeiwModel.IsProcessing = true;
                if(_PolicyListVeiwModel.HealthPolicies==null ||_PolicyListVeiwModel.HealthPolicies.Count==0)
                await _PolicyListVeiwModel.getAllPolicyList();
                _PolicyListVeiwModel.IsProcessing = false;
            }

        }
    }

 
}