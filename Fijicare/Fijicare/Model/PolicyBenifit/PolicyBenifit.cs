using System;
using System.Collections.Generic;
using System.Text;

namespace Fijicare.Model.PolicyBenifit
{

    public class ErrorObj
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class PolicyBenefitsObj
    {
        public int Id { get; set; }
        public int PolicyRisk_Id { get; set; }
        public string Cover_Cd { get; set; }
        public string Cover_Nm { get; set; }
        public string Cover_Type { get; set; }
        public object Unit { get; set; }
        public DateTime Effective_From { get; set; }
        public string Limit_Type { get; set; }
        public int Min_Limit { get; set; }
        public int Max_Limit { get; set; }
        public string Limit_Desc { get; set; }
        public string Rate { get; set; }
        public int Net_Amt { get; set; }
        public object Cal_Amt { get; set; }
        public object Intermediary_Commission_Amt { get; set; }
        public object Intermediary_Commission_Rate { get; set; }
        public object Ri_Rate { get; set; }
        public object Ri_Amt { get; set; }
        public object Deductable { get; set; }
        public object Deductable_Type { get; set; }
        public int Actv_Ind { get; set; }
        public DateTime Lst_Updt_Dt { get; set; }
        public int Lst_Updt_Usr { get; set; }
        public string Lst_Updt_Ip_Addr { get; set; }
    }

    public class ResponseObj
    {
        public List<PolicyBenefitsObj> PolicyBenefitsObj { get; set; }
    }

    public class RootObject
    {
        public List<ErrorObj> ErrorObj { get; set; }
        public ResponseObj ResponseObj { get; set; }
    }
}
