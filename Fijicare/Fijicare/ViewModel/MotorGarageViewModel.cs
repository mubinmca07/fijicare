
using Fijicare.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Fijicare.Model.Escaltion;
using System.Collections.ObjectModel;
using Fijicare.Model;
using Fijicare.Commands;
using Fijicare.Helpers;
using System.Linq;
namespace Fijicare.ViewModel
{
    public class MotorGarageViewModel : ModelBase
    {
        public DelegateCommand<string> BackCommand { get; set; }
        protected INavigation NavigationService { get; set; }
        public DelegateCommand<string> DetailCommand { get; set; }
        private ObservableCollection<Fijicare.Model.MotorGarage.MotorGarageObj> _MotorGarage;
        public ObservableCollection<Fijicare.Model.MotorGarage.MotorGarageObj> MotorGarage
        {
            get { return _MotorGarage; }
            set { SetProperty(ref _MotorGarage, value); }
        }
        public DelegateCommand LogoutButtonCommand { get; set; }
        public DelegateCommand SearchButtonCommand { get; set; }
        public string PageParameter { get; set; }
        public MotorGarageViewModel(INavigation _NavigationService)
        {
            this.NavigationService = _NavigationService;
            BackCommand = new DelegateCommand<string>(BackCommandClicked);
            LogoutButtonCommand = new DelegateCommand(LogoutButtonClicked);
            SearchButtonCommand = new DelegateCommand(SearchButtonCommandClicked);
            DetailCommand = new DelegateCommand<string>(DetailCommandClick);
            NotificationCommand = new DelegateCommand(NotificationCommandClick);
            PageParameter = Session.PageParameter;
            FooterCommand = new DelegateCommand<string>(Footer_Click);
        }
        public DelegateCommand<string> FooterCommand { get; set; }
        private async void   Footer_Click(string obj)
        {
            Session.PageParameter = obj;

            if (obj == "DOCTORS")
            {
                await NavigationService.PushAsync(new Fijicare.ViewPage.DoctorPage());
            }
            if (obj == "Garage")
            {
                await NavigationService.PushAsync(new Fijicare.ViewPage.GarageSearch());
            }
            if (obj == "Hospitals")
            {
                await NavigationService.PushAsync(new Fijicare.ViewPage.SearchHospitalpage());
            }
            if (obj == "Home")
            {
                await NavigationService.PushAsync(new Fijicare.ViewPage.WelcomepPage());
            }

            if (obj == "PHARMACY")
            {
                await NavigationService.PushAsync(new Fijicare.ViewPage.PharmecyPage());
            }
            if (obj == "EMERGENCY")
            {
                await NavigationService.PushAsync(new Fijicare.ViewPage.EmergencyContacts());
            }
        }
        private async void   NotificationCommandClick()
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.NotificationPage());

        }


        public DelegateCommand NotificationCommand { get; set; }

        


        private async void   DetailCommandClick(string obj)
        {
            if (string.IsNullOrEmpty(obj))
                return;
            // int Id = Convert.ToInt32(obj);
            // HospitalDetail = new ObservableCollection<Model.MedicalProviders.MedicalProvidersObj>();
            Session.Garage = (from p in MotorGarage where p.Id.ToString() == obj select p).ToList();
            // HospitalDetail.Add(ob);
            await NavigationService.PushAsync(new ViewPage.MotorGaragePage());
        }

        private async void SearchButtonCommandClicked()
        {
            LoadExclation();
        }

        private async void   LogoutButtonClicked()
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.LoginPage());

        }

        private async void   BackCommandClicked(string obj)
        {
             await  NavigationService.PopAsync();
        }


        private string _Provider;
        public string Provider
        {
            get { return _Provider; }
            set { SetProperty(ref _Provider, value); }
        }

        private bool _IsListVisible = false;
        public bool IsListVisible
        {
            get { return _IsListVisible; }
            set { SetProperty(ref _IsListVisible, value); }
        }

        

        public async void LoadExclation()
        {
            if (string.IsNullOrWhiteSpace(Provider))
            {
                XFToast.ShortMessage("please enter Provider");
                return;
            }
            IsListVisible = false;
            Dictionary<string, object> PostParam = new Dictionary<string, object>();
            PostParam.Add("Provider_Nm", Provider);
            ServiceRequestHelper _WebRequestHelper = new ServiceRequestHelper();
            string url = AppConstants.HostName + AppConstants.MotorGarage;
            ServiceRequestHelper.WebServiceResult result = await _WebRequestHelper.SendJsonDataPostRequest(url, PostParam);
            if (result != null && result.status == 200)
            {
                Model.MotorGarage.RootObject root = Newtonsoft.Json.JsonConvert.DeserializeObject<Fijicare.Model.MotorGarage.RootObject>(result.Result);

                List<Model.MotorGarage.ErrorObj> errorcode = root.ErrorObj;
                if (errorcode != null && errorcode.Count > 0)
                {
                    Model.MotorGarage.ErrorObj valChecksuccessorfail = errorcode[0];
                    if (valChecksuccessorfail.ErrorMessage == "Success" && valChecksuccessorfail.ErrorCode == "0")
                    {
                        IsListVisible = true;
                        Model.MotorGarage.ResponseObj Responseresult = root.ResponseObj;
                        MotorGarage = new ObservableCollection<Fijicare.Model.MotorGarage.MotorGarageObj>(Responseresult.MotorGarageObj);
                    }
                    else
                    {
                        XFToast.ShortMessage("no record found!");
                    }

                }
                else
                {
                    XFToast.ShortMessage("Something went wrong...");
                }

            }
            else
            {
                XFToast.ShortMessage("Please check your internet connection.");
            }
        }
    }
}
