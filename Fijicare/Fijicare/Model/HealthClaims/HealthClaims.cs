using System;
using System.Collections.Generic;
using System.Text;

namespace Fijicare.Model.HealthClaims
{
    public class ErrorObj
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class HealthClaimsObj
    {
        public string Claim_Id { get; set; }
        public string ClaimVersion_Id { get; set; }
        public string Policy_Id { get; set; }
        public string Client_Cd { get; set; }
        public string Client_Nm { get; set; }
        public string Provider_Cd { get; set; }
        public string Provider_Nm { get; set; }
        public string Status_Cd { get; set; }
        public string Status_Nm { get; set; }
        public string Insured_Cd { get; set; }
        private string _Insured_Nm;
        public string Insured_Nm { get
            { return _Insured_Nm; } set { _Insured_Nm = value; } }
        public string Branch_Cd { get; set; }
        public string Branch_Nm { get; set; }
        public string Product_Cd { get; set; }
        public string Product_Nm { get; set; }
        public string Scheme_Cd { get; set; }
        public string Scheme_Nm { get; set; }
        public string ClaimType_Cd { get; set; }
        public string ClaimType_Nm { get; set; }
        public string LossType_Cd { get; set; }
        public string LossType_Nm { get; set; }
        public string Dcn_No { get; set; }
        public string Claim_No { get; set; }
        public string Loss_Dt { get; set; }
        public string Policy_No { get; set; }
        public string stringermediary_Cd { get; set; }
        public string stringermediary_Nm { get; set; }
        public string Actv_Ind { get; set; }
        public string Version { get; set; }
        public string Lst_Updt_Dt { get; set; }
        public string Lst_Updt_Usr { get; set; }
        public string Lst_Updt_Ip_Addr { get; set; }
        public string Registration_No { get; set; }
        public string Engine_No { get; set; }
        public string Chassis_No { get; set; }
        public string Manufacturer_Cd { get; set; }
        public string Manufacturer_Nm { get; set; }
        public string stringimation_Mode { get; set; }
        public string stringimation_Dt { get; set; }
        public string stringimation_Tm { get; set; }
        public string stringimation_Source { get; set; }
        public string Estimated_Amt { get; set; }
        public string DOA { get; set; }
        public string DOA_Tm { get; set; }
        public string DOD { get; set; }
        public string DOD_Tm { get; set; }
        public string Loss_Cd { get; set; }
        public string Loss_Nm { get; set; }
        public string Ailment_Nm { get; set; }
        private string _Paid_Amt { get; set; }
        public string Paid_Amt
        {
            get
            {
                return _Paid_Amt;
            }
            set
            {
                _Paid_Amt = Fijicare.Utility.Helper.GetCurrencyFormatting(value);
            }
        }
    }

    public class ResponseObj
    {
        public List<HealthClaimsObj> HealthClaimsObj { get; set; }
    }

    public class RootObject
    {
        public List<ErrorObj> ErrorObj { get; set; }
        public ResponseObj ResponseObj { get; set; }
    }
}
