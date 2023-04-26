
using Fijicare.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fijicare.Model.MotorGarage
{
    public class ErrorObj
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class MotorGarageObj:ModelBase
    {
        public string Id { get; set; }
        public string ProviderType_Cd { get; set; }
        public string ProviderType_Nm { get; set; }
        public string Provider_Cd { get; set; }
        public string Provider_Nm { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string Pin_Zip { get; set; }
        public string City_Nm { get; set; }
        public string District_Nm { get; set; }
        public string State_Nm { get; set; }
        public string Mobile1 { get; set; }
        public string Mobile2 { get; set; }
        public string Mobile3 { get; set; }
        public string Landline_No1 { get; set; }
        public string Landline_No2 { get; set; }
        public string Fax_No1 { get; set; }
        public string Fax_No2 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Website { get; set; }
        public DateTime Lst_Updt_Dt { get; set; }
        private string _FullAddress;
        public string FullAddress
        {
            get
            {
                _FullAddress = Address + "," + City_Nm + " " + Pin_Zip;
                return _FullAddress;
            }

        }
    }

    public class ResponseObj
    {
        public List<MotorGarageObj> MotorGarageObj { get; set; }
    }

    public class RootObject
    {
        public List<ErrorObj> ErrorObj { get; set; }
        public ResponseObj ResponseObj { get; set; }
    }
}
