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
	public partial class MemberDetail : ContentPage
	{
        MemberDetailViewModel memberDetailViewModel;
        public MemberDetail ()
		{
			InitializeComponent ();
            memberDetailViewModel = new MemberDetailViewModel(Navigation);
            this.BindingContext = memberDetailViewModel;

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}