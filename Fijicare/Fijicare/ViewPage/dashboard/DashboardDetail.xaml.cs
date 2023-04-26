using Fijicare.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fijicare.ViewPage.dashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardDetail : ContentPage
    {
        DashboardVeiwModel dashboardVeiwModel;
        public DashboardDetail()
        {
            InitializeComponent();
            dashboardVeiwModel = new DashboardVeiwModel(Navigation);
            this.BindingContext = dashboardVeiwModel;
        }
    }
}