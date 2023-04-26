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
	public partial class GarageSearch : ContentPage
	{
		
        MotorGarageViewModel _MotorGarageViewModel;
        public GarageSearch()
        {
            InitializeComponent();
            _MotorGarageViewModel = new MotorGarageViewModel(Navigation);
            this.BindingContext = _MotorGarageViewModel;

        }
    }
}