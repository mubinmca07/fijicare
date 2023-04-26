using Fijicare.Commands;
using Fijicare.Helpers;
using Fijicare.Model;
using Fijicare.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Fijicare.Model.Escaltion;
using System.Collections.ObjectModel;
using Fijicare.ViewPage;

namespace Fijicare.ViewModel
{
    public class EscaltionMatrixViewModel : ModelBase
    {
        public DelegateCommand<string> BackCommand { get; set; }
        protected INavigation NavigationService { get;  set; }

        private ObservableCollection<EscaltionMatrixObj> _EscaltionMatrix;
        public ObservableCollection<EscaltionMatrixObj> EscaltionMatrix
        {
            get { return _EscaltionMatrix; }
            set { SetProperty(ref _EscaltionMatrix, value); }
        }
        public DelegateCommand LogoutButtonCommand { get; set; }

        public string PageParameter { get; set; }
        public EscaltionMatrixViewModel(INavigation _NavigationService)
        {
            this.NavigationService = _NavigationService;
            BackCommand = new DelegateCommand<string>(BackCommandClicked);
            LogoutButtonCommand = new DelegateCommand(LogoutButtonClicked);
            PageParameter =  Session.PageParameter;

             LoadExclation();

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


        private async void   LogoutButtonClicked()
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.LoginPage());

        }

        private async void   BackCommandClicked(string obj)
        {
             await  NavigationService.PopAsync();
        }

        public async void LoadExclation()
        {
            Dictionary<string, object> PostParam = new Dictionary<string, object>();
            PostParam.Add("Esclation_Type", "Policy");
            ServiceRequestHelper _WebRequestHelper = new ServiceRequestHelper();
            string url = AppConstants.HostName + AppConstants.EscaltionMatrix;
            ServiceRequestHelper.WebServiceResult result = await _WebRequestHelper.SendJsonDataPostRequest(url, PostParam);
            if (result != null && result.status == 200)
            {
                EscaltionRootObject root = Newtonsoft.Json.JsonConvert.DeserializeObject<Fijicare.Model.Escaltion.EscaltionRootObject>(result.Result);

                List<ErrorObj> errorcode = root.ErrorObj;
                if (errorcode != null && errorcode.Count > 0)
                {
                    ErrorObj valChecksuccessorfail = errorcode[0];
                    if (valChecksuccessorfail.ErrorMessage == "Success" && valChecksuccessorfail.ErrorCode == "0")
                    {
                        EscaltionResponseObj Responseresult = root.ResponseObj;
                        EscaltionMatrix = new ObservableCollection<EscaltionMatrixObj>(Responseresult.EscaltionMatrixObj);
                    }
                    else
                    {
                        XFToast.ShortMessage("Something went wrong");
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
