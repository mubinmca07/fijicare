using System;
using System.Collections.Generic;
using Fijicare.ViewModel;
using Xamarin.Forms;

namespace Fijicare.ViewPage
{
    public partial class EmergencyContacts : ContentPage
    {

        EmergencyViewModel _EmergencyViewModel;
        public EmergencyContacts()
        {
            InitializeComponent();
            _EmergencyViewModel = new EmergencyViewModel(Navigation);
            this.BindingContext = _EmergencyViewModel;

        }
    }
}
