using Fijicare.Commands;
using Fijicare.Helpers;
using Fijicare.Interfaces;
using Fijicare.Model;
using Fijicare.Utility;
using Fijicare.Interfaces;
using Fijicare.Model;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Fijicare.ViewModel
{

    public class UploadDocViewModel : ModelBase
    {
        INavigation NavigationService;

        //  StackOrientation.Horizontal

        private string _PageName;
        public string PageName
        {
            get { return _PageName; }
            set { SetProperty(ref _PageName, value); }
        }

        private StackOrientation _Orientation;
        public StackOrientation Orientation
        {
            get { return _Orientation; }
            set { SetProperty(ref _Orientation, value); }
        }
        public double OriginalHeight;
        private double _Height;
        public double Height
        {
            get { return _Height; }
            set { SetProperty(ref _Height, value); }
        }

        private bool _IsVisible;
        public bool IsVisible
        {
            get { return _IsVisible; }
            set { SetProperty(ref _IsVisible, value); }
        }

        private bool _IsGridVisible;
        public bool IsGridVisible
        {
            get { return _IsGridVisible; }
            set { SetProperty(ref _IsGridVisible, value); }
        }

        private bool _IsStackLayoutVisible = true;
        public bool IsStackLayoutVisible
        {
            get { return _IsStackLayoutVisible; }
            set { SetProperty(ref _IsStackLayoutVisible, value); }
        }



        private bool _IsProcessing;
        public bool IsProcessing
        {
            get { return _IsProcessing; }
            set { SetProperty(ref _IsProcessing, value); }
        }
        public DelegateCommand<string> BrowseCommand { get; set; }
        public DelegateCommand<string> UploadCommand { get; set; }
        public DelegateCommand<string> BackCommand { get; set; }
        public DelegateCommand LogoutButtonCommand { get; set; }
        public DelegateCommand<string> TakePhotoCommand { get; set; }
        public UploadDocViewModel(INavigation navigation)
        {
            NavigationService = navigation;
            BrowseCommand = new DelegateCommand<string>(BrowseClick);
            PhotoImage = string.Empty;
            UploadCommand = new DelegateCommand<string>(UploadClick);
            BackCommand = new DelegateCommand<string>(BackCommandClick);

            LogoutButtonCommand = new DelegateCommand(LogoutButtonClicked);
            TakePhotoCommand = new DelegateCommand<string>(TakePhotoClick);

            if (Session.IsPolicyDoc)
            {
                PageName = "Upload Policy Document";
            }
            else
            {
                PageName = "Upload claim Document";
            }
        }

        public async void TakePhotoClick(string obj)
        {
            try
            {
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    XFToast.ShortMessage(" No camera avaialble.");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    CompressionQuality = 25,
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                    //Name = DateTime.Now.ToString() + ".jpg" 
                });
                if (PathList == null)
                {
                    PathList = new ObservableCollection<string>();
                }
                if (file != null && file.Path != null)
                {
                    PathList.Add(file.Path);
                    if (DocList == null)
                        DocList = new ObservableCollection<NameAndPath>();
                    //foreach (var item in PathList)
                    {

                        NameAndPath andPath = new NameAndPath();
                        andPath.ImagePath = file.Path;
                        andPath.ImageName = System.IO.Path.GetFileName(file.Path);
                        DocList.Add(andPath);
                    }
                    IsStackLayoutVisible = false;
                    IsGridVisible = true;
                    IsVisible = true;
                }
            }
            catch (Exception)
            {


            }






        }

        private string _ImageSource;
        public string PhotoImage
        {
            get { return _ImageSource; }
            set { SetProperty(ref _ImageSource, value); }
        }
        private async void   BackCommandClick(string obj)
        {
             await  NavigationService.PopAsync();
        }
        private async void   LogoutButtonClicked()
        {
            await NavigationService.PushAsync(new Fijicare.ViewPage.LoginPage());

        }
        private ObservableCollection<string> _PathList = null;
        public ObservableCollection<string> PathList
        {
            get { return _PathList; }
            set
            {


                SetProperty(ref _PathList, value);

                if (value != null && value.Count > 0)
                {
                    DocList = new ObservableCollection<NameAndPath>();
                    foreach (var item in value)
                    {
                        NameAndPath andPath = new NameAndPath();
                        andPath.ImagePath = item;
                        andPath.ImageName = System.IO.Path.GetFileName(item);
                        DocList.Add(andPath);
                    }
                }
            }
        }

        private async void UploadClick(string obj)
        {
            if (Session.IsPolicyDoc)
            {
                await UploadClickWithDCN(obj);
            }
            else
            {
                await UploadClickWithPolicy(obj); 
            }
        }
        private async Task UploadClickWithPolicy(string obj)
        {
            var uploadcount = 0;

            IsProcessing = true;

            if (DocList != null && DocList.Count > 0)
            {



                foreach (var item in DocList)
                {
                    UploadDocRequestData uploadDocRequestData = new UploadDocRequestData();
                    if (Session.Policy_type == "01")
                    {
                        if (item.IsUploadVisible == false)
                        {
                            uploadDocRequestData.policy_No = Session.UploadDocPolicy.Policy_No;
                            uploadDocRequestData.member_Cd = string.Empty;
                            uploadDocRequestData.type = "ClaimDocument";
                            uploadDocRequestData.source_System_Nm = "MobileApp";
                            uploadDocRequestData.crtd_Usr = Session.UploadDocPolicy.Crtd_Usr;
                            uploadDocRequestData.crtd_Ip_Addr = Session.UploadDocPolicy.Crtd_Ip_Addr;


                            List<EFileUploadList> eFileUploadLists = new List<EFileUploadList>();
                            uploadDocRequestData.eFileUploadListObj = eFileUploadLists;

                            EFileUploadList eFileUploadList = new EFileUploadList();
                            byte[] AsBytes = File.ReadAllBytes(item.ImagePath);
                            String AsBase64String = Convert.ToBase64String(AsBytes);
                            eFileUploadList.file_Stream = AsBase64String;
                            eFileUploadList.file_Nm = Path.GetFileName(item.ImagePath);
                            eFileUploadList.file_Extn = Path.GetExtension(item.ImagePath);
                            eFileUploadLists.Add(eFileUploadList);
                            ServiceRequestHelper _WebRequestHelper = new ServiceRequestHelper();
                            string url = AppConstants.HostName + AppConstants.UploadDoc;
                            ServiceRequestHelper.WebServiceResult result =
                                        await _WebRequestHelper.SendJsonDataPostRequest(url, uploadDocRequestData);
                            if (result != null && result.status == 200)
                            {
                                RootObjectUpload root = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObjectUpload>(result.Result);
                                if (root.ErrorObj != null && root.ErrorObj.Count > 0)
                                {
                                    if (root.ErrorObj[0].ErrorCode == "0")
                                    {
                                        // IsProcessing = false;

                                        // XFToast.ShortMessage(item.ImageName + "\n Image Upload Succesfully");
                                        item.IsUploadVisible = true;
                                        // DocList = new ObservableCollection<NameAndPath>();

                                    }
                                    else
                                    {
                                        item.IsUploadFailed = true;
                                        uploadcount++;
                                        //IsProcessing = false;
                                        XFToast.ShortMessage("1111 " + root.ErrorObj[0].ErrorMessage);
                                    }
                                }
                            }
                            else
                            {
                                XFToast.ShortMessage("Not upload succefully");
                            }
                            //DocList = new ObservableCollection<NameAndPath>();
                        }
                    }

                    else
                    {
                        XFToast.ShortMessage(" This feature is not for motor policy.");
                        //uploadDocRequestData.policy_No = Session.UploadDocMotterPolicy.Policy_No;
                        //uploadDocRequestData.member_Cd = Session.UploadDocMotterPolicy.Client_Cd;
                        // uploadDocRequestData.type = Session.UploadDocMotterPolicy.Business_Type;
                        // uploadDocRequestData.crtd_Ip_Addr = Session.UploadDocMotterPolicy.Crtd_Ip_Addr;
                    }


                }

            }
            else
            {
                XFToast.ShortMessage("Please select doc");
            }


            IsProcessing = false;

            if (uploadcount == 0)
            {




            }
            else
            {
                var res = await Fijicare.App.Current.MainPage.DisplayAlert("Upload Failed", "One or more document are not uploaded succefully. Will you Retry? ", "Yes", "No");
                // XFToast.ShortMessage("Not uploaded succefully. \n Please Retry");

                if (!res)
                {
                    DocList = new ObservableCollection<NameAndPath>();
                    IsStackLayoutVisible = true;
                    IsGridVisible = false;
                    IsVisible = false;
                    IsProcessing = false;

                }
            }


            /* DocList = new ObservableCollection<NameAndPath>();
         IsStackLayoutVisible = true;
         IsGridVisible = false;
         IsVisible = false;
         IsProcessing = false;*/




        }


        private async Task UploadClickWithDCN(string obj)
        {
            var uploadcount = 0;

            IsProcessing = true;

            if (DocList != null && DocList.Count > 0)
            {



                foreach (var item in DocList)
                {
                    UploadDocRequestDCN uploadDocRequestData = new UploadDocRequestDCN();
                    /*if (Session.Policy_type == "01")
                    {*/
                        if (item.IsUploadVisible == false)
                        {
                            uploadDocRequestData.policy_No = Session.UploadDocPolicy.Policy_No;
                            uploadDocRequestData.member_Cd = string.Empty;
                            uploadDocRequestData.type = "PolicyDocument";
                            uploadDocRequestData.source_System_Nm = "MobileApp";
                            uploadDocRequestData.crtd_Usr = Session.UploadDocPolicy.Crtd_Usr;
                            uploadDocRequestData.crtd_Ip_Addr = Session.UploadDocPolicy.Crtd_Ip_Addr;
                            uploadDocRequestData.Dcn_No = Session.UploadDocPolicy.Dcn_No;

                            List<EFileUploadList> eFileUploadLists = new List<EFileUploadList>();
                            uploadDocRequestData.eFileUploadListObj = eFileUploadLists;

                            EFileUploadList eFileUploadList = new EFileUploadList();
                            byte[] AsBytes = File.ReadAllBytes(item.ImagePath);
                            String AsBase64String = Convert.ToBase64String(AsBytes);
                            eFileUploadList.file_Stream = AsBase64String;
                            eFileUploadList.file_Nm = Path.GetFileName(item.ImagePath);
                            eFileUploadList.file_Extn = Path.GetExtension(item.ImagePath);
                            eFileUploadLists.Add(eFileUploadList);
                            ServiceRequestHelper _WebRequestHelper = new ServiceRequestHelper();
                            string url = AppConstants.HostName + AppConstants.UploadPolicyDCN;
                            ServiceRequestHelper.WebServiceResult result =
                                        await _WebRequestHelper.SendJsonDataPostRequest(url, uploadDocRequestData);
                            if (result != null && result.status == 200)
                            {
                                RootObjectUpload root = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObjectUpload>(result.Result);
                                if (root.ErrorObj != null && root.ErrorObj.Count > 0)
                                {
                                    if (root.ErrorObj[0].ErrorCode == "0")
                                    {
                                        // IsProcessing = false;

                                        // XFToast.ShortMessage(item.ImageName + "\n Image Upload Succesfully");
                                        item.IsUploadVisible = true;
                                        // DocList = new ObservableCollection<NameAndPath>();

                                    }
                                    else
                                    {
                                        item.IsUploadFailed = true;
                                        uploadcount++;
                                        //IsProcessing = false;
                                        XFToast.ShortMessage("1111 " + root.ErrorObj[0].ErrorMessage);
                                    }
                                }
                            }
                            else
                            {
                                XFToast.ShortMessage("Not upload succefully");
                            }
                            //DocList = new ObservableCollection<NameAndPath>();
                        }
                    /*}

                    else
                    {
                        XFToast.ShortMessage(" This feature is not for motor policy.");
                        //uploadDocRequestData.policy_No = Session.UploadDocMotterPolicy.Policy_No;
                        //uploadDocRequestData.member_Cd = Session.UploadDocMotterPolicy.Client_Cd;
                        // uploadDocRequestData.type = Session.UploadDocMotterPolicy.Business_Type;
                        // uploadDocRequestData.crtd_Ip_Addr = Session.UploadDocMotterPolicy.Crtd_Ip_Addr;
                    }*/


                }

            }
            else
            {
                XFToast.ShortMessage("Please select doc");
            }


            IsProcessing = false;

            if (uploadcount == 0)
            {




            }
            else
            {
                var res = await Fijicare.App.Current.MainPage.DisplayAlert("Upload Failed", "One or more document are not uploaded succefully. Will you Retry? ", "Yes", "No");
                // XFToast.ShortMessage("Not uploaded succefully. \n Please Retry");

                if (!res)
                {
                    DocList = new ObservableCollection<NameAndPath>();
                    IsStackLayoutVisible = true;
                    IsGridVisible = false;
                    IsVisible = false;
                    IsProcessing = false;

                }
            }


            /* DocList = new ObservableCollection<NameAndPath>();
         IsStackLayoutVisible = true;
         IsGridVisible = false;
         IsVisible = false;
         IsProcessing = false;*/




        }

        private ObservableCollection<NameAndPath> _DocList;
        public ObservableCollection<NameAndPath> DocList
        {
            get { return _DocList; }
            set { SetProperty(ref _DocList, value); }
        }

        private async void BrowseClick(string obj)
        {

            if (Device.RuntimePlatform == Device.Android)
            {

                await DependencyService.Get<IMediaService>().OpenGallery();
            }
            else
            {
                // string[] fileTypes = null;
                //fileTypes = new string[] { "public.image" };
                //var pickedFile = await CrossFilePicker.Current.PickFile();
                var pickedFile = await CrossMedia.Current.PickPhotoAsync();
                if (pickedFile != null)
                {
                    if (DocList == null)
                        DocList = new ObservableCollection<NameAndPath>();
                    NameAndPath andPath = new NameAndPath();
                    andPath.ImagePath = pickedFile.Path;
                    andPath.ImageName = System.IO.Path.GetFileName(pickedFile.Path);
                    DocList.Add(andPath);
                }
                else
                if (pickedFile == null)
                    return; // user canceled file picking


            }

            IsStackLayoutVisible = false;
            IsGridVisible = true;
            IsVisible = true;
        }
    }
    public class NameAndPath : ModelBase
    {

        public string ImageName { get; set; }

        public string ImagePath { get; set; }
        private bool _IsUploadVisible = false;
        public bool IsUploadVisible
        {
            get { return _IsUploadVisible; }
            set { SetProperty(ref _IsUploadVisible, value); }
        }

        private bool _IsUploadFailed = false;
        public bool IsUploadFailed
        {
            get { return _IsUploadFailed; }
            set { SetProperty(ref _IsUploadFailed, value); }
        }

    }
    public class UploadDocRequestDCN : UploadDocRequestData
    {
        public string Dcn_No { get; set; }
        
    }

    public class EFileUploadList
    {
        public string file_Nm { get; set; }
        public string file_Extn { get; set; }
        public string file_Stream { get; set; }
    }

    public class UploadDocRequestData
    {
        public string policy_No { get; set; }
        public string member_Cd { get; set; }
        public string type { get; set; }
        public string source_System_Nm { get; set; }
        public string crtd_Usr { get; set; }
        public string crtd_Ip_Addr { get; set; }
        public List<EFileUploadList> eFileUploadListObj { get; set; }
    }




    public class EFileUploadObj
    {
        public string Dcn_No { get; set; }
    }

    public class ResponseObjupload
    {
        public List<EFileUploadObj> EFileUploadObj { get; set; }
    }

    public class RootObjectUpload
    {
        public List<ErrorObj> ErrorObj { get; set; }
        public ResponseObjupload ResponseObj { get; set; }
    }

}