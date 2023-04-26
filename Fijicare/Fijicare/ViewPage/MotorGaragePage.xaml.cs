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
	public partial class MotorGaragePage : ContentPage
	{
        GarageDetailViewModal _MotorGarageViewModel; 
        public MotorGaragePage ()
		{
			InitializeComponent ();
            _MotorGarageViewModel = new GarageDetailViewModal(Navigation);
            this.BindingContext = _MotorGarageViewModel;

        }
	}
}