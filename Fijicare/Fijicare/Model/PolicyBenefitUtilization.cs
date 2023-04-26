using System;
using System.Collections.Generic;
using System.Text;

namespace Fijicare.Model
{
    

    public class PolicyBenefitUtilizationObj
    {
        public string Policy_Id { get; set; }
        public string Insured_Nm { get; set; }
        public string Insured_Cd { get; set; }
        public string Cover_Cd { get; set; }
        public string Cover_Nm { get; set; }
        public string Cover_Type { get; set; }
        public DateTime Effective_From { get; set; }
        public string Limit_Type { get; set; }

        private string _Cover_Limit;
        public string Cover_Limit
        {
            get
            {                
                return _Cover_Limit;
            }
            set
            {
                _Cover_Limit = Fijicare.Utility.Helper.GetCurrencyFormatting(value);
            }
        }
        private string _Utilization_Amt;
        public string Utilization_Amt {
            get
            {

                return _Utilization_Amt;
            }
            set
            {
                _Utilization_Amt = Fijicare.Utility.Helper.GetCurrencyFormatting(value);
            }
        }
        private string _Balance_Amt;
        public string Balance_Amt {
            get
            {

                return _Balance_Amt;
            }
            set
            { 
                _Balance_Amt =  Fijicare.Utility.Helper.GetCurrencyFormatting(value); 
            }
        }
    }

    public class PolicyBenefitUtilizationResponseObj
    {
        public List<PolicyBenefitUtilizationObj> PolicyBenefitUtilizationObj { get; set; }
    }

    public class PolicyBenefitUtilizationRootObject
    {
        public List<ErrorObj> ErrorObj { get; set; }
        public PolicyBenefitUtilizationResponseObj ResponseObj { get; set; }
    }
}
