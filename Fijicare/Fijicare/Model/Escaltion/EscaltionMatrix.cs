using System;
using System.Collections.Generic;
using System.Text;

namespace Fijicare.Model.Escaltion
{
   
    public class EscaltionMatrixObj
    {
        public string Esclation_Type { get; set; }
        public string Esclation_Level { get; set; }
        public string Contact_Person { get; set; }
        public string Contact_Email { get; set; }
        public string Contact_Number { get; set; }
    }

    public class EscaltionResponseObj
    {
        public List<EscaltionMatrixObj> EscaltionMatrixObj { get; set; }
    }

    public class EscaltionRootObject
    {
        public List<ErrorObj> ErrorObj { get; set; }
        public EscaltionResponseObj ResponseObj { get; set; }
    }
}
