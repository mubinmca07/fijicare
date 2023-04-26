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
	public partial class SearchHospitalpage : ContentPage
	{
		

        HospitalViewModel _HospitalViewModel;
        public SearchHospitalpage()
        {
            InitializeComponent();
            _HospitalViewModel = new HospitalViewModel(Navigation);
            this.BindingContext = _HospitalViewModel;
        }
    }
}