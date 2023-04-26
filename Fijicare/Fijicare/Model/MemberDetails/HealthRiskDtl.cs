using System;
using System.Collections.Generic;
using System.Text;

namespace Fijicare.Model.MemberDetails
{
    public class ErrorObj
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class HealthRiskDtl
    {
        public string Policy_id { get; set; }
        public string Dcn_No { get; set; }
        public string Branch_Cd { get; set; }
        public string Branch_Nm { get; set; }
        public string Product_Cd { get; set; }
        public string Product_Nm { get; set; }
        public string Client_Cd { get; set; }
        public string Client_Nm { get; set; }
        public string Address_Line1 { get; set; }
        public string Address_Line2 { get; set; }
        public string Address_Line3 { get; set; }
        public string District_Nm { get; set; }
        public string City_Nm { get; set; }
        public string State_Nm { get; set; }
        public string Pin_Zip { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Landline_No1 { get; set; }
        public string Landline_No2 { get; set; }
        public string Location_Nm { get; set; }
        public string Proposal_No { get; set; }
        public string Proposer_Nm { get; set; }
        public string Policy_No { get; set; }
        public string Certificate_No { get; set; }
        public DateTime Policy_Start_Dt { get; set; }
        public DateTime Policy_End_Dt { get; set; }
        public string Status { get; set; }
        public string Insured_Cd { get; set; }
        public string Insured_Nm { get; set; }
        public string Family_Id { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string Relation_Nm { get; set; }
        public string Relation_Cd { get; set; }
        public string Scheme_Cd { get; set; }
        public string Scheme_Nm { get; set; }
        public string Employee_Cd { get; set; }
        public string Pan_No { get; set; }
        public DateTime Risk_Inception_Dt { get; set; }
        public DateTime Risk_Start_Dt { get; set; }
        public DateTime Risk_End_Dt { get; set; }
        public string Portability_Ind { get; set; }
        private string _Sum_Insured;
        public string Sum_Insured
        {
            get
            {
                return _Sum_Insured;
            }
            set 
            { 
                _Sum_Insured = Fijicare.Utility.Helper.GetCurrencyFormatting(value);
            }
        }
        public string Deductible_Sum_Insured { get; set; }
        public string Cumm_Bonus { get; set; }
        public string Prev_Claim_Ind { get; set; }
        public string Prev_Sum_Insured { get; set; }
        public string Ped_Ind { get; set; }
        public string Special_Conditions { get; set; }
        public string stringermediary_Cd { get; set; }
        public string stringermediary_Nm { get; set; }
        public string Age_Yr { get; set; }
        public string Policy_Tenure { get; set; }
        public string Policy_Tenure_Unit { get; set; }
    }

    public class ResponseObj
    {
        public List<HealthRiskDtl> HealthRiskDtls { get; set; }
    }

    public class RootObject
    {
        public List<ErrorObj> ErrorObj { get; set; }
        public ResponseObj ResponseObj { get; set; }
    }
}
