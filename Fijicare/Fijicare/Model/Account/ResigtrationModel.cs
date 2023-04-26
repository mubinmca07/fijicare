using System;
using System.Collections.Generic;

namespace Fijicare.Model.Account
{
    public class ErrorObj
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class EntityCredentialObj
    {
        public string Entity_Nm { get; set; }
    }

    public class ResponseObj
    {
        public List<EntityCredentialObj> EntityCredentialObj { get; set; }
    }

    public class RootObject
    {
        public List<ErrorObj> ErrorObj { get; set; }
        public ResponseObj ResponseObj { get; set; }
    }
}
