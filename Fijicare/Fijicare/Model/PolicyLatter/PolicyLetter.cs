using System;
using System.Collections.Generic;
using System.Text;

namespace Fijicare.Model.PolicyLatter
{
    public class ErrorObj
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class PolicyLetter
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string byteStrem { get; set; }
    }

    public class ResponseObj
    {
        public List<PolicyLetter> PolicyLetter { get; set; }
    }

    public class RootObject
    {
        public List<ErrorObj> ErrorObj { get; set; }
        public ResponseObj ResponseObj { get; set; }
    }
}
