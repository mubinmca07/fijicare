using Fijicare.Model;
using Fijicare.Model;
using Fijicare.Model.Doctor;
using Fijicare.Model.MedicalProviders;
using Fijicare.Model.MemberDetails;
using Fijicare.Model.MotorRiskDtl;
using Fijicare.Model.policy;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;


namespace Fijicare.Utility
{
    public static class Session
    {
        public static string lastlogin { get; set; }

        public static string mobile { get; set; }
        public static string username { get; set; } = "";

        public static bool IsDeviceExist { get; set; }
        public static bool IsComeFromDownLoad { get; set; } = false;
        public static bool IsComeFromPolicy { get; set; } = false;
        public static bool IsPolicyDoc { get; set; } = false;
        public static bool IsComeFitData { get; set; } = false;

        public static Model.Login.UserDetail userDetail { get; set; }


        public static string amount { get; set; }
        public static int is_password_created { get; set; }
        public static string UserId { get;  set; }
        public static string PageParameter { get;  set; }
        public static ObservableCollection<HealthRiskDtl> HealthRiskDtl { get;  set; }
        public static string Policy_Id { get; internal set; }
        public static ObservableCollection<PolicyBenefitUtilizationObj> HealthPoliciesBenefit { get;  set; }

        public static ObservableCollection<Fijicare.Model.policy.HealthPoliciesObj> HealthPolicies { get; set; }

        public static string Policy_type { get;  set; }

        public static bool IsComingFromInsured { get;  set; }
        public static string Policy_No { get; internal set; }
        public static List<MedicalProvidersObj> Hospital { get; internal set; }
        public static List<Fijicare.Model.MotorGarage.MotorGarageObj> Garage { get; internal set; }
        public static List<BulkCapitionProviderObj> Doctor { get;  set; }
        public static string DoctorType { get;  set; }
        public static HealthPoliciesObj UploadDocPolicy { get;  set; }
        public static MotorRiskDtl UploadDocMotterPolicy { get;  set; }
    }

}
