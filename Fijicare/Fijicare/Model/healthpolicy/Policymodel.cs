using System;
using System.Collections.Generic;


namespace Fijicare.Model.policy
{
    
    public class HealthPoliciesObj : Fijicare.Model.ModelBase
    {
        public string Policy_Id { get; set; }
        public string PolicyVersion_Id { get; set; }
        public string Parent_Id { get; set; }
        public string Client_Cd { get; set; }
        // public string Dcn_No { get; set; }

        private string _Dcn_No;
        public string Dcn_No
        {
            get { return _Dcn_No; }
            set
            {
                Imageurln = "View_Eye.png";
                if (Fijicare.Utility.Session.PageParameter == "Download")
                {
                    string path = Fijicare.App.path + "/" + value + ".pdf";

                   
                    if (!System.IO.File.Exists(path))
                    {
                        Imageurln = "download_btn.png";
                    }
                }
                SetProperty(ref _Dcn_No, value);
            }
        }

        private string _Imageurl = "View_Eye.png";
        public string Imageurln
        {
            get { return _Imageurl; }
            set
            {
                SetProperty(ref _Imageurl, value);

            }

        }
        public string Full_Nm { get; set; }
        public string Branch_Cd { get; set; }
        public string Branch_Nm { get; set; }
        public string Lob_Cd { get; set; }
        public string Lob_Nm { get; set; }
        public string Product_Cd { get; set; }
        public string Product_Nm { get; set; }
        public string ProductVarient_Cd { get; set; }
        public string Scheme_Cd { get; set; }
        public string Scheme_Nm { get; set; }
        public string Proposal_Type { get; set; }
        public string UID { get; set; }
        public string Business_Type { get; set; }
        public string Quotation_No { get; set; }
        public string Proposal_No { get; set; }
        public DateTime Proposal_Dt { get; set; }
        public DateTime Policy_Start_Dt { get; set; }
        public DateTime Policy_End_Dt { get; set; }
        public string Policy_No { get; set; }
        public string PolicyVersion_No { get; set; }
        public string Partner_Reference_No { get; set; }
        public string Past_Insurance_Flag { get; set; }
        public string stringermediary_Cd { get; set; }
        public string stringermediary_Nm { get; set; }
        public string Family_Count { get; set; }
        public string Sales_Manager_Cd { get; set; }
        public string Industry_Cd { get; set; }
        public string Sales_Manager_Nm { get; set; }
        public string Version { get; set; }
        public string Premium_Calc_Ind { get; set; }
        public string Member_Data_Ind { get; set; }
        public string Proposal_Basis { get; set; }
        public string Insured_Relationship { get; set; }
        public string Insured_Relationship_Desc { get; set; }
        public string Past_Policy_Expiry_Dt { get; set; }
        public string Past_Policy_No { get; set; }
        public string MasterPolicy_No { get; set; }
        private string _Premium;
        public string Premium
        {
            get
            {
                return _Premium;
            }
            set
            {
                _Premium = Fijicare.Utility.Helper.GetCurrencyFormatting(value);
            }
        }
        public string Crtd_Usr { get; set; }
        public DateTime Crtd_Dt { get; set; }
        public string Crtd_Ip_Addr { get; set; }
        public string Actv_Ind { get; set; }
        public DateTime Lst_Updt_Dt { get; set; }
        public string Lst_Updt_Usr { get; set; }
        public string Lst_Updt_Ip_Addr { get; set; }
        public string Master_Policy_Ind { get; set; }
        public string ProductType_Cd { get; set; }
        public string Address { get; set; }
        public string District_Nm { get; set; }
        public string City_Nm { get; set; }
        public string State_Nm { get; set; }
        public string Pin_Zip { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }

        public string is_motor_type { get; set; } = "01";
        public string Period
        {

            get
            {
                return string.Format("{0} - {1} / ({2})", String.Format("{0:M/d/yyyy}", Policy_Start_Dt) , String.Format("{0:M/d/yyyy}", Policy_End_Dt), Scheme_Nm);
            }
        }


    }

    public class HealthResponseObj
    {
        public List<HealthPoliciesObj> HealthPoliciesObj { get; set; }
    }

    public class HealthRootObject
    {
        public List<ErrorObj> ErrorObj { get; set; }
        public HealthResponseObj ResponseObj { get; set; }
    }

}
