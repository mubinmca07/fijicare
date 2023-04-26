using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Fijicare.ViewPage.dashboard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardFlyout : ContentPage
    {
        public ListView ListView;

        public DashboardFlyout()
        {
            InitializeComponent();

            BindingContext = new DashboardFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class DashboardFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<DashboardFlyoutMenuItem> MenuItems { get; set; }

            public DashboardFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<DashboardFlyoutMenuItem>(new[]
                {
                    new DashboardFlyoutMenuItem { Id = 0, Title = "Page 1" },
                    new DashboardFlyoutMenuItem { Id = 1, Title = "Page 2" },
                    new DashboardFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    new DashboardFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    new DashboardFlyoutMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}