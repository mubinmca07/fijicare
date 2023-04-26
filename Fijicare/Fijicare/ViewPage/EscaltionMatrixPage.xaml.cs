using Fijicare.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fijicare.ViewPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EscaltionMatrixPage : ContentPage
	{
        DoctorPageViewModel _DoctorPageViewModel;
        public EscaltionMatrixPage ()
		{
			InitializeComponent ();
            _DoctorPageViewModel = new DoctorPageViewModel(Navigation);
            this.BindingContext = _DoctorPageViewModel;

        }
	}
}