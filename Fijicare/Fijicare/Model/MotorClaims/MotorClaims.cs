
using System;
using System.Collections.Generic;
using System.Text;

namespace Fijicare.Model.MotorClaims
{
    public class ErrorObj
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class MotorClaimsObj
    {
        public int Claim_Id { get; set; }
        public int ClaimVersion_Id { get; set; }
        public int Policy_Id { get; set; }
        public string Client_Cd { get; set; }
        public string Client_Nm { get; set; }
        public string Provider_Cd { get; set; }
        public string Provider_Nm { get; set; }
        public string Status_Cd { get; set; }
        public string Status_Nm { get; set; }
        public string Insured_Cd { get; set; }
        public string Insured_Nm { get; set; }
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
        public DateTime Loss_Dt { get; set; }
        public string Policy_No { get; set; }
        public string Intermediary_Cd { get; set; }
        public string Intermediary_Nm { get; set; }
        public int Actv_Ind { get; set; }
        public DateTime Lst_Updt_Dt { get; set; }
        public int Lst_Updt_Usr { get; set; }
        public string Registration_No { get; set; }
        public string Engine_No { get; set; }
        public string Chassis_No { get; set; }
        public string Manufacturer_Cd { get; set; }
        public string Manufacturer_Nm { get; set; }
        public string Intimation_Mode { get; set; }
        public DateTime Intimation_Dt { get; set; }
        public string Intimation_Source { get; set; }
        public double Estimated_Amt { get; set; }
        public DateTime? Last_Doc_Dt { get; set; }
        public object DOA { get; set; }
        public object DOD { get; set; }
        public string Loss_Cd { get; set; }
        public string Loss_Nm { get; set; }
        public string Batch_No { get; set; }
        public string ProductSchema_Cd { get; set; }
        public string Lob_Cd { get; set; }
        public string Bill_Type { get; set; }
        public string Status_Grp { get; set; }
        public int Batch_Ind { get; set; }
    }

    public class ResponseObj
    {
        public List<MotorClaimsObj> MotorClaimsObj { get; set; }
    }

    public class RootObject
    {
        public List<ErrorObj> ErrorObj { get; set; }
        public ResponseObj ResponseObj { get; set; }
    }
}
