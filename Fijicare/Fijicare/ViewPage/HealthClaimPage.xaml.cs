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
	public partial class HealthClaimPage : ContentPage
	{
        HealthClaimVeiwModel _HealthClaimVeiwModel;
        public HealthClaimPage ()
		{
			InitializeComponent ();
            _HealthClaimVeiwModel = new HealthClaimVeiwModel(Navigation);
            this.BindingContext = _HealthClaimVeiwModel;

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (_HealthClaimVeiwModel != null)
            {
                if (Session.Policy_type == "02")
                {

                    if (_HealthClaimVeiwModel.MotorClaims==null)
                   await _HealthClaimVeiwModel.MotorClaim();
                }
                else
                {

                    if (_HealthClaimVeiwModel.HealthClaims == null)
                        await _HealthClaimVeiwModel.HealthClaim();
                }
            }
        }
    }
}