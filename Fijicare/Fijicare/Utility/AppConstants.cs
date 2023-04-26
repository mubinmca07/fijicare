using System;
using System.Collections.Generic;
using System.Text;

namespace Fijicare.Utility
{
   public static class AppConstants
    {
#if DEBUG
        public static string HostName = "http://www.Fijicare.com.fj/fijiwebapi/";
        //public static string HostName = "https://healthcorev3api.azurewebsites.net/";
        //  public static string HostName = "http://14.143.236.193/HealthCoreV3Api/";
        //   public static string HostName = "http://developer.Fijicare.com/";
        // public static string HostName = "https://www.Fijicare.com/";
        // public const string HostName = "http://emstage.Fijicare.com/";
        //  public static string HostName = "http://beta.Fijicare.co.za/";

#else
            public static string HostName = "http://www.Fijicare.com.fj/fijiwebapi/";
        //public static string HostName = "https://healthcorev3api.azurewebsites.net/";
        //      public static string HostName = "http://14.143.236.193/";
        //  public static string HostName = "http://developer.Fijicare.com/";
        //   public const string HostName = "http://www.Fijicare.co.za/";
        // public const string HostName =  "http://www.Fijicare.com/";
        /*---------------------------for azure server ------------------ */
        // public static string HostName = "http://www.Fijicare.net/"; 
        // http://www.Fijicare.com.fj/fijiwebapi/api/MobileApp/ValidateEntityCredential

#endif

        public const string ValidateEntityCredential = "api/MobileApp/ValidateEntityCredential";
        public const string GetHealthPolicies = "api/MobileApp/GetHealthPolicies";

        public const string GetMotorPolicies = "api/MobileApp/GetMotorPolicies";

        public const string GetHealthRiskDtls = "api/MobileApp/GetHealthRiskDtls";
        public const string GetHealthClaims = "api/MobileApp/GetHealthClaims";
        public const string GetMotorClaims = "api/MobileApp/GetMotorClaims";
        public const string E_CardDownload = "api/MobileApp/E_CardDownload";

        public const string getDoctors = "api/MobileApp/GetBulkCapitionProvider";
//        "Category": "Pharmacy",
//  "Provider_Nm": "",
//  "Area": ""
//}
    //[10:55 AM, 6/3/2019] Wasim: MobileApp/MobileApp_GetBulkCapitionProviders
         public const string getPharamacy = "api/MobileApp/GetBulkCapitionProviders";
        public const int UserNotRegisteredResponseCode = 5027;

        public const int UserRegisteredResponseCode = 5006;

        public const int UserNotVerified = 5010;


        public const int ContetnPrice = 1500;
        public const string Contetn_Store_package = "science_experiment";
        public const string UPDATEPASSWORD = "api/v1.4/tabletregistration";
        public const string CheckDeviceId = "api/v1.0/checkdevice";
        public const string GetPolicyBenefitUtilization = "api/MobileApp/GetPolicyBenefitUtilization";

        public const string LogedInWithClass = "LogedInWithClass";
        public const string BORADCLASSLISTS = "api/v1.1/boardclasslists";        

        public const string USERMOBILENUMBER = "UserMobileNumber";

        public const string SERVER_APP_NAME = "2DINTERACTIVE";

        public const string CheckedPrice = "checked_price_page";

        public const string APPLE_STORE_NAME = "itunes";

        public const string GOOGLE_STORE_NAME = "google play store";

        public const string WINDOWS_STORE_NAME = "windows store";

        public const string APP_VERSION = "1.0.0.2";

        public const string STRING_AFTER_PURCHASE = "Purchased";
        public const string IsDeviceIdRegister = "IsDeviceIdRegister";

        public const string USERID = "UserId";

        public const string USERDETAILS = "UserDetails";

        public const string USERprofilepic = "USERprofilepic";

        public const string SELECTBOARD = "SelectedBoard";

        public const string SELECTEDCLASS = "SelectedClass";

    

        public const string TransitionMessage = "Transition";

        public const string PASSWORD = "Password";

        public const string CHANGEPASSWORD = "api/v1.4/tabletregistration";

        public const string GETUSERINFO = "api/v1.2/tabletregistration";

        public const string APP_NAME = "2DINTERACTIVE";

        public const string APPUSER = "APPUSER";

        public const string DASHBOARDCONTENTLIST = "api/v1.0/dashboardcontentlist";

        public const string APPTYPE = "2D";

        public const string APPCART = "appcart";

        public const string PROFILEPICUPDATE = "api/v1.1/profilepicupdate";

        public const string PROFILECREATED= "ProfileCreated";
        public const string NOTIFIFATION= "api/MobileApp/GetNotifications";

        public const string ERROR_MESSAGE = "Something went wrong! please try again";

        public static string EscaltionMatrix = "api/MobileApp/GetEscaltionMatrix";

        public static string MotorGarage = "api/MobileApp/GetMotorGarage";

        public static string MedicalPovider = "api/MobileApp/GetMedicalProviders";

        public static string GetMotorRiskDtls  = "api/MobileApp/GetMotorRiskDtls";

        public const string Crud_EntityCredential = "api/MobileApp/Crud_EntityCredential";
        public const string CrudEntityUpdatePassword = "api/MobileApp/CrudEntityUpdatePassword";
        public const string forgotPasword = "api/MobileApp/CrudEntityForgotPassSessions?api_key=forgot";

        public const string UploadDoc = "api/MobileApp/CreateUpdateEFileUpload";
        public const string getProfile = "api/MobileApp/CrudEntityUpdateProfile";
        public const string UploadPolicyDCN = "api/MobileApp/PolicyEFileUpload";
    }
}
