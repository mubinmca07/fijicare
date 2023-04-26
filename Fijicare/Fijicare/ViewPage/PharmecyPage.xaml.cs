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
    public partial class PharmecyPage : ContentPage
    {
        PharmecyViewModal _PharmecyViewModal;
        public PharmecyPage()
        {
            InitializeComponent();
            Session.DoctorType = "Pharmacy";
            _PharmecyViewModal = new PharmecyViewModal(Navigation);
            this.BindingContext = _PharmecyViewModal;

        }
    }
}