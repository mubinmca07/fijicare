using System;
using System.Collections.Generic;
using System.Text;

namespace Fijicare.Model.MotorRiskDtl
{
    public class ErrorObj
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class MotorRiskDtl
    {
        public string Product_Cd { get; set; }
        public string Product_Nm { get; set; }
        public string Client_Cd { get; set; }
        public string Client_Nm { get; set; }
        public string Branch_Cd { get; set; }
        public string Branch_Nm { get; set; }
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
        public string stringermediary_Cd { get; set; }
        public string stringermediary_Nm { get; set; }
        public string Insured_Cd { get; set; }
        public string Insured_Nm { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; }
        public string Owner_Nm { get; set; }
        public string Registration_No { get; set; }
        public string Registration_Location { get; set; }
        public string RegistrationOffice_Cd { get; set; }
        public string Registration_Dt { get; set; }
        public string Manufacture_Yr { get; set; }
        public string Engine_No { get; set; }
        public string Chassis_No { get; set; }
        public string Manufacturer_Cd { get; set; }
        public string Manufacturer_Nm { get; set; }
        public string Model_Cd { get; set; }
        public string Model_Nm { get; set; }
        public string ModelVarient_Cd { get; set; }
        public string ModelVarient_Nm { get; set; }
        public string __invalid_name__BodyType_Nm { get; set; }
        public string CubicCapacity { get; set; }
        public string HorsePower { get; set; }
        public string SeatingCapacity { get; set; }
        public string Gross_Wt { get; set; }
        public string Color { get; set; }
        public string FuelType { get; set; }
        public string Transmission_Type { get; set; }
        public string Vehical_IDV { get; set; }
        public string Accessories_IDV { get; set; }
        public string Total_IDV { get; set; }
        public string Driver_Accident_Ind { get; set; }
        public string Scheme_Cd { get; set; }
        public string Scheme_Nm { get; set; }
        public DateTime Risk_Inception_Dt { get; set; }
        public DateTime Risk_Start_Dt { get; set; }
        public DateTime Risk_End_Dt { get; set; }
        public string Portability_Ind { get; set; }
        public string Sum_Insured { get; set; }
        public string Deductible_Sum_Insured { get; set; }
        public string Cumm_Bonus { get; set; }
        public string Prev_Claim_Ind { get; set; }
        public string Prev_Sum_Insured { get; set; }
        public string Special_Conditions { get; set; }
        public string Actv_Ind { get; set; }
        public string Version { get; set; }
        public string Crtd_Usr_Nm { get; set; }
        public DateTime Crtd_Dt { get; set; }
        public string Vehicle_Type { get; set; }
    }

    public class ResponseObj
    {
        public List<MotorRiskDtl> MotorRiskDtls { get; set; }
    }

    public class RootObject
    {
        public List<ErrorObj> ErrorObj { get; set; }
        public ResponseObj ResponseObj { get; set; }
    }
}
