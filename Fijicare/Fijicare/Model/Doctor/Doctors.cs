using System;
using System.Collections.Generic;
using System.Text;

namespace Fijicare.Model.Doctor
{
    public class ErrorObj
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class BulkCapitionProviderObj
    {
        public string id { get; set; }
        public string __invalid_name__Status { get; set; }
        public string SubCat { get; set; } = "-";
        public string Name { get; set; } = "-";
        public string Business_Name { get; set; } = "-";
        public string Manager_Nm { get; set; } = "-";// for pharmacy
        public string LiaisingOfficer_Nm { get; set; } = "-";    // for pharmacy
        public string Area { get; set; } = "-";
        public string Email1 { get; set; } = "-";
        public string Email2 { get; set; }
        public string Email3 { get; set; } = "-";
        public string Address1 { get; set; } = "-";
        public string Address2 { get; set; }

        public string Address {
            get
            {
                return Address1 + "\n" + Address2;
            }
          }
        public string PhoneNo1 { get; set; } = "-";
        public string PhoneNo2 { get; set; } = "-";
        public string Mobile1 { get; set; } = "-";
        public object Mobile2 { get; set; } = "-";
        public string Fax_No { get; set; } = "-";
        public string OperatingHours { get; set; }
        public string Provider_Cd { get; set; }
        
    }

    public class ResponseObj
    {
        public List<BulkCapitionProviderObj> BulkCapitionProviderObj { get; set; }
    }

    public class RootDoctors
    {
        public List<ErrorObj> ErrorObj { get; set; }
        public ResponseObj ResponseObj { get; set; }
    }
}
