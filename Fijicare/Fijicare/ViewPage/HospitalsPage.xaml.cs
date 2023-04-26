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
	public partial class HospitalsPage : ContentPage
	{
        HospitalDeatailViewModel _HospitalViewModel;
        public HospitalsPage ()
		{
            InitializeComponent ();
            _HospitalViewModel = new HospitalDeatailViewModel(Navigation);
            this.BindingContext = _HospitalViewModel;
        }
	}
}