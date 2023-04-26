using Fijicare.Commands;
using Fijicare.Helpers;
using Fijicare.Model;
using Fijicare.Utility;
using Fijicare.Model.policy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fijicare.ViewModel
{

    public class PolicyDetailVeiwModel : ModelBase
    {
        protected INavigation NavigationService { get; private set; }

        private string _PageName;
        public string PageName
        {
            get { return _PageName; }
            set { SetProperty(ref _PageName, value); }
        }

        private ObservableCollection<Fijicare.Model.policy.HealthPoliciesObj> _HealthPolicies;
       public  ObservableCollection<Fijicare.Model.policy.HealthPoliciesObj> HealthPolicies
        {
            get { return _HealthPolicies; }
            set { SetProperty(ref _HealthPolicies, value); }
        }

        private ObservableCollection<Fijicare.Model.MotorRiskDtl.MotorRiskDtl> _MotorRiskDtl;
        public ObservableCollection<Fijicare.Model.MotorRiskDtl.MotorRiskDtl> MotorRiskDtl
        {
            get { return _MotorRiskDtl; }
            set { SetProperty(ref _MotorRiskDtl, value); }
        }
        private bool _IsMotor = false;
        public bool IsMotor
        {
            get { return _IsMotor; }
            set { SetProperty(ref _IsMotor, value); }
        }
        private bool _IsHealth = false;
        public bool IsHealth
        {
            get { return _IsHealth; }
            set { SetProperty(ref _IsHealth, value); }
        }

        private bool _IsProcessing;
        public bool IsProcessing
        {
            get { return _IsProcessing; }
            set { SetProperty(ref _IsProcessing, value); }
        }


        public DelegateCommand<string> BackCommand { get; set; }
        public DelegateCommand<string> InsuredCommand { get; set; }
        public DelegateCommand LogoutButtonCommand { get; set; }
        public DelegateCommand<string> MemberDetailCommand { get; set; }
        public DelegateCommand<string> UploadDocCommand { get; set; }
        public PolicyDetailVeiwModel(INavigation _NavigationService)
        {
            this.NavigationService = _NavigationService;
            Session.UploadDocMotterPolicy = null;
            Session.UploadDocMotterPolicy = null;
            PageName = Utility.Session.PageParameter;
            NotificationCommand = new DelegateCommand(NotificationCommandClick);
            BackCommand = new DelegateCommand<string>(BackCommandClick);
            InsuredCommand = new DelegateCommand<string>(InsuredCommandClick);
            LogoutButtonCommand = new DelegateCommand(LogoutButtonClicked);
            MemberDetailCommand = new DelegateCommand<string>(MemberDetailClicked);
            // getPolicy();
            FooterCommand = new DelegateCommand<string>(Footer_Click);
            UploadDocCommand = new DelegateCommand<string>(UploadDocCommandClicked);
        }

        private async void UploadDocCommandClicked(string obj)
        {
            Session.IsPolicyDoc = true;

            if (Session.Policy_type == "01")
            {
                var policiesObjs = (from s in HealthPolicies
                                    where s.Policy_No == obj
                                    select s).FirstOrDefault();
                Session.UploadDocPolicy = policiesObjs;
            }
            else
            {
                var policiesObjsM = (from s in MotorRiskDtl
                                    where s.Policy_No == obj
                                    select s).FirstOrDefault();
                Session.UploadDocMotterPolicy = policiesObjsM;
            }
            await NavigationService.PushAsync(new Fijicare.ViewPage.UploadDocPage());
        }
        public DelegateCommand<string> FooterCommand { get; set; }
        private async void Footer_Click(string obj)
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
            if (obj == "Hospitals")
            {
                await NavigationService.PushAsync(new Fijicare.ViewPage.SearchHospitalpage());
            }
            if (obj == "Home")
            {
                await NavigationService.PushAsync(new ViewPage.WelcomepPage());
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

       


        void MemberDetailClicked1(string obj)
        {
            switch (Session.PageParameter)
            {

                case "Claims":
                   // Session.PageParameter = obj;
                  //  await NavigationService.PushAsync(new ViewPage.PolicyListPage());
                    break;
                case "Insured":
                   // Session.PageParameter = obj;
                   // await NavigationService.PushAsync(new ViewPage.PolicyListPage());
                    break;
                case "Policy":
                   // Session.PageParameter = obj;
                   // await NavigationService.PushAsync(new ViewPage.PolicyListPage());
                    break;
                case "Benifit":
                   // Session.PageParameter = obj;
                  //  await NavigationService.PushAsync(new Fijicare.ViewPage.PolicyBenifit());
                    break;
                case "Help":
                  //  Session.PageParameter = obj;
                   // await NavigationService.PushAsync(new Fijicare.ViewPage.HelpPage());
                    break;
                case "Download":
                   // Session.PageParameter = obj;
                   // await NavigationService.PushAsync(new ViewPage.PolicyListPage());
                    break;
            }
        }

        private async void MemberDetail(string obj)
        {
        }




           // private async void MemberDetailClicked(string obj)
        private async void   MemberDetailClicked(string obj)
        {
            if(Session.Policy_type=="01")
            {
               
                IsMotor = false;
                IsHealth = true;
                Health(obj);
            }
            else
            {
                //IsMotor = true;
                //IsHealth = false;
                //Motor(obj);

            }
        }

    




        private async void Health(string obj)
        {
            Dictionary<string, object> PostParam = new Dictionary<string, object>();
            PostParam.Add("Policy_Id", obj);


            ServiceRequestHelper _WebRequestHelper = new ServiceRequestHelper();


            string url = AppConstants.HostName + AppConstants.GetHealthRiskDtls;
            ServiceRequestHelper.WebServiceResult result = await _WebRequestHelper.SendJsonDataPostRequest(url, PostParam);
            if (result != null && result.status == 200)
            {
                Fijicare.Model.MemberDetails.RootObject root = Newtonsoft.Json.JsonConvert.DeserializeObject<Fijicare.Model.MemberDetails.RootObject>(result.Result);

                List<Fijicare.Model.MemberDetails.ErrorObj> errorcode = root.ErrorObj;
                if (errorcode != null && errorcode.Count > 0)
                {
                    Fijicare.Model.MemberDetails.ErrorObj valChecksuccessorfail = errorcode[0];
                    if (valChecksuccessorfail.ErrorMessage == "Success" && valChecksuccessorfail.ErrorCode == "0")
                    {
                        Fijicare.Model.MemberDetails.ResponseObj Responseresult = root.ResponseObj;
                        Session.HealthRiskDtl = new ObservableCollection<Fijicare.Model.MemberDetails.HealthRiskDtl>(Responseresult.HealthRiskDtls);

                        await NavigationService.PushAsync(new Fijicare.ViewPage.MemberDetail());
                    }
                    else
                    {
                        // XFToast.ShortMessage("Please enter valid credential.");
                        XFToast.ShortMessage("there is no record available.");
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
        private async void MotorPolicy(string obj)
        {
            Dictionary<string, object> PostParam = new Dictionary<string, object>();
            PostParam.Add("Policy_Id", obj);
            ServiceRequestHelper _WebRequestHelper = new ServiceRequestHelper();

            string url = AppConstants.HostName + AppConstants.GetMotorRiskDtls;
            ServiceRequestHelper.WebServiceResult result = await _WebRequestHelper.SendJsonDataPostRequest(url, PostParam);
            if (result != null && result.status == 200)
            {
                Fijicare.Model.MotorRiskDtl.RootObject root = Newtonsoft.Json.JsonConvert.DeserializeObject<Fijicare.Model.MotorRiskDtl.RootObject>(result.Result);
                List<Fijicare.Model.MotorRiskDtl.ErrorObj> errorcode = root.ErrorObj;
                if (errorcode != null && errorcode.Count > 0)
                {
                    Fijicare.Model.MotorRiskDtl.ErrorObj valChecksuccessorfail = errorcode[0];
                    if (valChecksuccessorfail.ErrorMessage == "Success" && valChecksuccessorfail.ErrorCode == "0")
                    {
                        Fijicare.Model.MotorRiskDtl.ResponseObj Responseresult = root.ResponseObj;
                        MotorRiskDtl = new ObservableCollection<Fijicare.Model.MotorRiskDtl.MotorRiskDtl>(Responseresult.MotorRiskDtls);

                    //    await NavigationService.PushAsync(new Fijicare.ViewPage.MemberDetail());
                    }
                    else
                    {
                        
                        XFToast.ShortMessage("there is no record available.");
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
        private async void   InsuredCommandClick(string obj)
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.MemberBenifitPage());
        }

        public async  Task getPolicy()
        {

            if (Session.Policy_type == "01")
            {

                IsMotor = false;
                IsHealth = true;
              //  Health(Session.Policy_Id);
             HealthPolicies =   Session.HealthPolicies;
            }
            else
            {
                IsMotor = true;
                IsHealth = false;
                MotorPolicy(Session.Policy_Id);

            }



            
        }
    

        private async void BackCommandClick(string obj)
        {
            await  NavigationService.PopAsync();
        }
        private async void LogoutButtonClicked()
        {
            await NavigationService.PushAsync(new ViewPage.LoginPage());

        }
    }
    }
