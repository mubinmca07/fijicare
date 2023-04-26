
using Fijicare.Commands;
using Fijicare.Helpers;
using Fijicare.Utility;
using Fijicare.Model.policy;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using System;
using System.IO;
using Plugin.Permissions.Abstractions;
using Fijicare.Model;

namespace Fijicare.ViewModel
{
    public class PolicyListVeiwModel : Fijicare.Model.ModelBase
    {
        protected INavigation NavigationService { get; private set; }

        private string _PageName;
        public string PageName
        {
            get { return _PageName; }
            set { SetProperty(ref _PageName, value); }
        }
        private bool _IsProcessing;
        public bool IsProcessing
        {
            get { return _IsProcessing; }
            set { SetProperty(ref _IsProcessing, value); }
        }

        private string _cmdName;
        public string CommandView
        {
            get { return _cmdName; }
            set { SetProperty(ref _cmdName, value); }
        }

        private ObservableCollection<Fijicare.Model.policy.HealthPoliciesObj> _HealthPolicies;
        public ObservableCollection<Fijicare.Model.policy.HealthPoliciesObj> HealthPolicies
        {
            get { return _HealthPolicies; }
            set { SetProperty(ref _HealthPolicies, value); }
        }

        private bool _IsComeFromDownLoadT;
        public bool IsComeFromDownLoadT
        {
            get { return _IsComeFromDownLoadT; }
            set { SetProperty(ref _IsComeFromDownLoadT, value); }
        }

        private bool _IsComeFromDownLoadF;
        public bool IsComeFromDownLoadF
        {
            get { return _IsComeFromDownLoadF; }
            set { SetProperty(ref _IsComeFromDownLoadF, value); }
        }

        private bool _IsComeFromPolicy;
        public bool IsComeFromPolicy
        {
            get { return _IsComeFromPolicy; }
            set { SetProperty(ref _IsComeFromPolicy, value); }
        }


        public DelegateCommand<string> BackCommand { get; set; }
        public DelegateCommand<string> InsuredCommand { get; set; }
        public DelegateCommand LogoutButtonCommand { get; set; }
        public DelegateCommand<string> PolicyCommand { get; set; }
        public DelegateCommand<string> UploadCommand { get; set; }
        public DelegateCommand ProfileButtonCommand { get; set; }
        //private string _Imageurl = "View_Eye.png";
        //public string Imageurl
        //{
        //    get { return _Imageurl; }
        //    set { SetProperty(ref _Imageurl, value); }
        //}
        public PolicyListVeiwModel(INavigation _NavigationService)
        {
            this.NavigationService = _NavigationService;
            IsComeFromDownLoadF = Session.IsComeFromDownLoad==true?false:true;
            IsComeFromDownLoadT = Session.IsComeFromDownLoad;
            IsComeFromPolicy = Session.IsComeFromPolicy;
            PageName = Utility.Session.PageParameter;
            if (PageName == "Insured")
            {
                PageName = "Upload Document";
            }
            BackCommand = new DelegateCommand<string>(BackCommandClick);
            InsuredCommand = new DelegateCommand<string>(InsuredCommandClick);
            LogoutButtonCommand = new DelegateCommand(LogoutButtonClicked);
            PolicyCommand = new DelegateCommand<string>(PolicyCommandClicked);
            NotificationCommand = new DelegateCommand(NotificationCommandClick);
            UploadCommand = new DelegateCommand<string>(UploadCommandClick);
            ProfileButtonCommand = new DelegateCommand(ProfileButtonCommandClick);
            getParameter();


            FooterCommand = new DelegateCommand<string>(Footer_Click);
        }

        private async void ProfileButtonCommandClick()
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.ProfilePage());
        }

        private async void UploadCommandClick(string obj)
        {
            HealthPoliciesObj policyObj = (from p in HealthPolicies where p.Policy_No == obj select p).FirstOrDefault();
            Session.Policy_type = policyObj.is_motor_type;
            if (Session.Policy_type == "01")
            {
                var policiesObjs = (from s in HealthPolicies
                                    where s.Policy_No == obj
                                    select s).FirstOrDefault();
                Session.UploadDocPolicy = policiesObjs;
            }
            else
            {
                //var policiesObjsM = (from s in HealthPolicies
                //                     where s.Policy_No == obj
                //                     select s).FirstOrDefault();
                //Session.UploadDocMotterPolicy = policiesObjsM;
            }
            await NavigationService.PushAsync(new Fijicare.ViewPage.UploadDocPage());
        }

        public DelegateCommand<string> FooterCommand { get; set; }
        private async void Footer_Click(string obj)
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

        private async void NotificationCommandClick()
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.NotificationPage());

        }


        public DelegateCommand NotificationCommand { get; set; }




        private async void PolicyCommandClicked(string obj)
        {
            Session.Policy_Id = obj;
            HealthPoliciesObj policyObj = (from p in HealthPolicies where p.Policy_Id == obj select p).FirstOrDefault();

            if (policyObj == null || string.IsNullOrEmpty(policyObj.Policy_No))
            {
                XFToast.ShortMessage("Policy does not exist.");
                return;
            }
            Session.IsComingFromInsured = false;
            Session.HealthPolicies = new ObservableCollection<HealthPoliciesObj>();
            Session.HealthPolicies.Add(policyObj);
            //Session.Policy_type = policyObj.ProductType_Cd;
            Session.Policy_type = policyObj.is_motor_type;
            switch (Session.PageParameter)
            {

                case "Policy":

                    await NavigationService.PushAsync(new Fijicare.ViewPage.PolicyDetails());
                    break;

                case "Claims":
                    Session.Policy_No = policyObj.Policy_No;
                    await NavigationService.PushAsync(new Fijicare.ViewPage.HealthClaimPage());
                    break;
                case "Insured":
                    if (policyObj.ProductType_Cd == "01")
                    {
                        Session.IsComingFromInsured = true;
                        await NavigationService.PushAsync(new Fijicare.ViewPage.MemberDetail());
                    }
                    else
                    {
                        await NavigationService.PushAsync(new Fijicare.ViewPage.PolicyDetails());
                    }
                    break;

                case "Benefit":
                    BenefitClicked();

                    break;
                case "Help":
                    //  Session.PageParameter = obj;
                    // await NavigationService.PushAsync(new Fijicare.ViewPage.HelpPage());
                    break;
                case "Download":
                  
                    string path = Fijicare.App.path + "/" + policyObj.Dcn_No + ".pdf";
                    XDPermision.CheckSMSPermision(Permission.Storage);

                    if (System.IO.File.Exists(path))
                    {
                       
                        await NavigationService.PushAsync(new Fijicare.ViewPage.PdfViewer(policyObj.Dcn_No + ".pdf"));
                    }
                    else
                    {

                        await Download(obj);
                    }
                    break;
            }
        }

        private async Task Download(string obj)
        {
            Session.Policy_Id = obj;
            string FileName = string.Empty;
            HealthPoliciesObj policyObj = (from p in HealthPolicies where p.Policy_Id == obj select p).FirstOrDefault();
            Dictionary<string, object> PostParam = new Dictionary<string, object>();
            PostParam.Add("flag", "pdf");
            PostParam.Add("Ref_No", policyObj.Dcn_No);
            IsProcessing = true;
            ServiceRequestHelper _WebRequestHelper = new ServiceRequestHelper();

            string path = DependencyService.Get<Fijicare.Interfaces.IContentPath>().GetContentPath();
            string url = AppConstants.HostName + AppConstants.E_CardDownload;
            ServiceRequestHelper.WebServiceResult result = await _WebRequestHelper.SendJsonDataPostRequest(url, PostParam);
            if (result != null && result.status == 200)
            {
                Fijicare.Model.PolicyLatter.RootObject root = Newtonsoft.Json.JsonConvert.DeserializeObject<Fijicare.Model.PolicyLatter.RootObject>(result.Result);

                List<Fijicare.Model.PolicyLatter.ErrorObj> errorcode = root.ErrorObj;
                if (errorcode != null && errorcode.Count > 0)
                {
                    Fijicare.Model.PolicyLatter.ErrorObj valChecksuccessorfail = errorcode[0];
                    if (valChecksuccessorfail.ErrorMessage == "Success" && valChecksuccessorfail.ErrorCode == "0")
                    {
                        Fijicare.Model.PolicyLatter.ResponseObj Responseresult = root.ResponseObj;
                        if (Responseresult != null && Responseresult.PolicyLetter != null)
                        {
                            ObservableCollection<Fijicare.Model.PolicyLatter.PolicyLetter> polictLaters = new ObservableCollection<Fijicare.Model.PolicyLatter.PolicyLetter>(Responseresult.PolicyLetter);
                            foreach (Fijicare.Model.PolicyLatter.PolicyLetter polictLater in polictLaters)
                            {
                                string base64BinaryStr = polictLater.byteStrem;
                                path = path + "/" + policyObj.Dcn_No + ".pdf";
                                FileName = policyObj.Dcn_No +".pdf";
                                if (!File.Exists(path))
                                {
                                    using (FileStream stream = System.IO.File.Create(path))
                                    {
                                        byte[] byteArray = Convert.FromBase64String(base64BinaryStr);
                                        stream.Write(byteArray, 0, byteArray.Length);
                                    }
                                }
                            }
                            await NavigationService.PushAsync(new Fijicare.ViewPage.PdfViewer(FileName));
                            // XFToast.ShortMessage("Your Card is downloaded.. please check in download folder");
                        }
                        else
                        {
                            XFToast.ShortMessage("Something went wrong.");
                        }

                        IsProcessing = false;
                    }
                    else
                    {
                        // XFToast.ShortMessage("Please enter valid credential.");
                        /// XFToast.ShortMessage("there is no member detail available.");
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

        void getParameter()
        {

           // Imageurl = "View_Eye.png";
            switch (Session.PageParameter)
            {


                case "Claims":
                    CommandView = "Claim View";
                    // Session.PageParameter = obj;
                    //  await NavigationService.PushAsync(new ViewPage.PolicyListPage());
                    break;
                case "Insured":
                    //CommandView = "Insured View ";
                    CommandView = "Upload Doc ";
                    // Session.PageParameter = obj;
                    // await NavigationService.PushAsync(new ViewPage.PolicyListPage());
                    break;
                case "Policy":
                    CommandView = "Policy View ";
                    // Session.PageParameter = obj;
                    // await NavigationService.PushAsync(new ViewPage.PolicyListPage());
                    break;
                case "Benefit":
                    CommandView = "Policy Benefit View ";

                    // Session.PageParameter = obj;
                    //  await NavigationService.PushAsync(new Fijicare.ViewPage.PolicyBenifit());
                    break;
                case "Help":
                    //  Session.PageParameter = obj;
                    // await NavigationService.PushAsync(new Fijicare.ViewPage.HelpPage());
                    break;
                case "Download":
                    CommandView = "Download View ";
                   // Imageurl = "download_btn.png";
                    // Session.PageParameter = obj;
                    // await NavigationService.PushAsync(new ViewPage.PolicyListPage());
                    break;
            }
        }





        private async void MemberDetailClicked(string obj)
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

        private async void InsuredCommandClick(string obj)
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.MemberBenifitPage());
        }

        public async Task getAllPolicyList()
        {
            Dictionary<string, object> PostParam = new Dictionary<string, object>();
            PostParam.Add("Client_Cd", Session.userDetail.Entity_Cd);
            ServiceRequestHelper _WebRequestHelper = new ServiceRequestHelper();
            string url = AppConstants.HostName + AppConstants.GetHealthPolicies;
            ServiceRequestHelper.WebServiceResult result = await _WebRequestHelper.SendJsonDataPostRequest(url, PostParam);
            if (result != null && result.status == 200)
            {
                HealthRootObject root = Newtonsoft.Json.JsonConvert.DeserializeObject<HealthRootObject>(result.Result);

                List<ErrorObj> errorcode = root.ErrorObj;
                if (errorcode != null && errorcode.Count > 0)
                {
                    ErrorObj valChecksuccessorfail = errorcode[0];
                    if (valChecksuccessorfail.ErrorMessage == "Success" && valChecksuccessorfail.ErrorCode == "0")
                    {
                        HealthResponseObj Responseresult = root.ResponseObj;
                        HealthPolicies = new System.Collections.ObjectModel.ObservableCollection<HealthPoliciesObj>(Responseresult.HealthPoliciesObj);
                        if (!CommandView.Contains("Download"))
                        {
                            string url1 = AppConstants.HostName + AppConstants.GetMotorPolicies;
                            ServiceRequestHelper.WebServiceResult result1 = await _WebRequestHelper.SendJsonDataPostRequest(url1, PostParam);
                            if (result1 != null && result1.status == 200)
                            {
                                result1.Result = result1.Result.Replace("MotorPoliciesObj", "HealthPoliciesObj");
                                
                                HealthRootObject root1 = Newtonsoft.Json.JsonConvert.DeserializeObject<HealthRootObject>(result1.Result);
                                List<ErrorObj> errorcode1 = root1.ErrorObj;
                                if (errorcode1 != null && errorcode1.Count > 0)
                                {
                                    ErrorObj valChecksuccessorfail1 = errorcode1[0];
                                    if (valChecksuccessorfail1.ErrorMessage == "Success" && valChecksuccessorfail1.ErrorCode == "0")
                                    {
                                        HealthResponseObj Responseresult1 = root1.ResponseObj;
                                        ObservableCollection<HealthPoliciesObj> temp = new System.Collections.ObjectModel.ObservableCollection<HealthPoliciesObj>(Responseresult1.HealthPoliciesObj);
                                        foreach (HealthPoliciesObj item in temp)
                                        {
                                            item.is_motor_type = "02";
                                            HealthPolicies.Add(item);
                                        }
                                    }
                                }
                            }
                        }

                        ////Session.userDetail = lsl[0];
                        // await NavigationService.PushAsync(new ViewPage.Dashboardpage());
                    }
                    else
                    {
                        //XFToast.ShortMessage("Please enter valid cred");
                        if (!CommandView.Contains("Download"))
                        {
                            string url1 = AppConstants.HostName + AppConstants.GetMotorPolicies;
                            ServiceRequestHelper.WebServiceResult result1 = await _WebRequestHelper.SendJsonDataPostRequest(url1, PostParam);
                            if (result1 != null && result1.status == 200)
                            {
                                result1.Result = result1.Result.Replace("MotorPoliciesObj", "HealthPoliciesObj");
                                HealthRootObject root1 = Newtonsoft.Json.JsonConvert.DeserializeObject<HealthRootObject>(result1.Result);
                                List<ErrorObj> errorcode1 = root1.ErrorObj;
                                if (errorcode1 != null && errorcode1.Count > 0)
                                {
                                    ErrorObj valChecksuccessorfail1 = errorcode1[0];
                                    if (valChecksuccessorfail1.ErrorMessage == "Success" && valChecksuccessorfail1.ErrorCode == "0")
                                    {
                                        HealthResponseObj Responseresult1 = root1.ResponseObj;
                                        HealthPolicies = new System.Collections.ObjectModel.ObservableCollection<HealthPoliciesObj>();
                                        ObservableCollection<HealthPoliciesObj> temp = new System.Collections.ObjectModel.ObservableCollection<HealthPoliciesObj>(Responseresult1.HealthPoliciesObj);
                                        foreach (HealthPoliciesObj item in temp)
                                        {
                                            item.is_motor_type = "02";
                                            HealthPolicies.Add(item);
                                        }
                                    }
                                }
                            }
                        }
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

        private async void BenefitClicked()
        {
            Dictionary<string, object> PostParam = new Dictionary<string, object>();
            PostParam.Add("Policy_Id", Session.Policy_Id);
            Session.HealthPoliciesBenefit = null;

            ServiceRequestHelper _WebRequestHelper = new ServiceRequestHelper();


            string url = AppConstants.HostName + AppConstants.GetPolicyBenefitUtilization;
            ServiceRequestHelper.WebServiceResult result = await _WebRequestHelper.SendJsonDataPostRequest(url, PostParam);
            if (result != null && result.status == 200)
            {
                PolicyBenefitUtilizationRootObject root = Newtonsoft.Json.JsonConvert.DeserializeObject<PolicyBenefitUtilizationRootObject>(result.Result);

                List<Fijicare.Model.ErrorObj> errorcode = root.ErrorObj;
                if (errorcode != null && errorcode.Count > 0)
                {
                    Fijicare.Model.ErrorObj valChecksuccessorfail = errorcode[0];
                    if (valChecksuccessorfail.ErrorMessage == "Success" && valChecksuccessorfail.ErrorCode == "0")
                    {
                      var responseresult = root.ResponseObj;
                        Session.HealthPoliciesBenefit = new ObservableCollection<Fijicare.Model.PolicyBenefitUtilizationObj>(responseresult.PolicyBenefitUtilizationObj);

                        await NavigationService.PushAsync(new Fijicare.ViewPage.PolicyBenifit());
                    }
                    else
                    {
                        // XFToast.ShortMessage("Please enter valid credential.");
                        XFToast.ShortMessage("there is no benefit detail available.");
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

        private async void   BackCommandClick(string obj)
        {
             await  NavigationService.PopAsync();
        }
        private async void   LogoutButtonClicked()
        {
            await NavigationService.PushAsync(new ViewPage.LoginPage());

        }
    }
}
