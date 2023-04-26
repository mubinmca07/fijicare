
using Fijicare.Commands;
using Fijicare.Helpers;
using Fijicare.Model;
using Fijicare.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;
using Fijicare.ViewPage;

namespace Fijicare.ViewModel
{
    public class HospitalViewModel : ModelBase
    {
        public DelegateCommand<string> BackCommand { get; set; }
        public DelegateCommand<string> DetailCommand { get; set; }
        protected INavigation NavigationService { get; set; }

        private ObservableCollection<Fijicare.Model.MedicalProviders.MedicalProvidersObj> _Hospital;
        public ObservableCollection<Fijicare.Model.MedicalProviders.MedicalProvidersObj> Hospital
        {
            get { return _Hospital; }
            set { SetProperty(ref _Hospital, value); }
        }

        private ObservableCollection<Fijicare.Model.MedicalProviders.MedicalProvidersObj> _HospitalD;
        public ObservableCollection<Fijicare.Model.MedicalProviders.MedicalProvidersObj> HospitalDetail
        {
            get { return _HospitalD; }
            set { SetProperty(ref _HospitalD, value); }
        }

        public DelegateCommand LogoutButtonCommand { get; set; }
        public DelegateCommand SearchButtonCommand { get; set; }
        public string PageParameter { get; set; }
        public HospitalViewModel(INavigation _NavigationService)
        {
            this.NavigationService = _NavigationService;
            BackCommand = new DelegateCommand<string>(BackCommandClicked);
            LogoutButtonCommand = new DelegateCommand(LogoutButtonClicked);
            SearchButtonCommand = new DelegateCommand(SearchButtonCommandClicked);
            PageParameter = Session.PageParameter;
            DetailCommand = new DelegateCommand<string>(DetailCommandClick);
            NotificationCommand = new DelegateCommand(NotificationCommandClick);
            //   IsSearch = true
            FooterCommand = new DelegateCommand<string>(Footer_Click);
        }
        public DelegateCommand<string> FooterCommand { get; set; }
        private async void   Footer_Click(string obj)
        {
            Session.PageParameter = obj;

            if (obj == "Escalation")
            {
                await NavigationService.PushAsync(new Fijicare.ViewPage.EscaltionMatrixPage());
            }
            if (obj == "Garage")
            {
                await NavigationService.PushAsync(new Fijicare.ViewPage.GarageSearch());
            }
            /*if (obj == "Hospitals")
            {
                await NavigationService.PushAsync(new Fijicare.ViewPage.SearchHospitalpage());
            }*/
            if (obj == "Home")
            {
                await NavigationService.PushAsync(new WelcomepPage());
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
            if (!string.IsNullOrWhiteSpace(LocalStorageHelper.RetriveFromLocalSetting("IsLogin")))
            {
                if (LocalStorageHelper.RetriveFromLocalSetting("IsLogin") == "true")
                {
                    await NavigationService.PushAsync(new Fijicare.ViewPage.NotificationPage());
                }
                else
                    XFToast.ShortMessage("Something went wrong...");

            }
            else
                XFToast.ShortMessage("Something went wrong...");


        }


        public DelegateCommand NotificationCommand { get; set; }


        private bool isSearch;
        public bool IsSearch
        {
            get { return isSearch; }
            set { SetProperty(ref isSearch, value); }

        }
        private bool isDetail;
        public bool IsDetail
        {
            get { return isDetail; }
            set { SetProperty(ref isDetail, value); }

        }

        private async void   DetailCommandClick(string obj)
        {
            if (string.IsNullOrEmpty(obj))
                return;
           // int Id = Convert.ToInt32(obj);
           // HospitalDetail = new ObservableCollection<Model.MedicalProviders.MedicalProvidersObj>();
            Session.Hospital =(from p in Hospital where p.Id.ToString() == obj select p).ToList();
           // HospitalDetail.Add(ob);
            await NavigationService.PushAsync(new ViewPage.HospitalsPage());
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


        private string _Provider ="Hiranandani";
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
                XFToast.ShortMessage("Please enter Provider");
                return;
            }
            IsListVisible = false;
            Dictionary<string, object> PostParam = new Dictionary<string, object>();
            PostParam.Add("Provider_Nm", Provider);
            ServiceRequestHelper _WebRequestHelper = new ServiceRequestHelper();
            string url = AppConstants.HostName + AppConstants.MedicalPovider;
            ServiceRequestHelper.WebServiceResult result = await _WebRequestHelper.SendJsonDataPostRequest(url, PostParam);
            if (result != null && result.status == 200)
            {
                Model.MedicalProviders.RootObject root = Newtonsoft.Json.JsonConvert.DeserializeObject<Fijicare.Model.MedicalProviders.RootObject>(result.Result);

                List<Model.MedicalProviders.ErrorObj> errorcode = root.ErrorObj;
                if (errorcode != null && errorcode.Count > 0)
                {
                    Model.MedicalProviders.ErrorObj valChecksuccessorfail = errorcode[0];
                    if (valChecksuccessorfail.ErrorMessage == "Success" && valChecksuccessorfail.ErrorCode == "0")
                    {
                        IsListVisible = true;
                        Model.MedicalProviders.ResponseObj Responseresult = root.ResponseObj;
                        Hospital = new ObservableCollection<Fijicare.Model.MedicalProviders.MedicalProvidersObj>(Responseresult.MedicalProvidersObj);
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
