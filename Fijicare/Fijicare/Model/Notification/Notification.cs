using System;
using System.Collections.Generic;
using System.Text;

namespace Fijicare.Model.Notification
{
    public class ErrorObj
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class NotificationsObj
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Entity_Cd { get; set; }
        public string Mesasage { get; set; }
        public string Actv_Ind { get; set; }
        public DateTime Lst_Updt_Dt { get; set; }
        public string Lst_Updt_Usr { get; set; }
        public string Lst_Updt_Ip_Addr { get; set; }
    }

    public class ResponseObj
    {
        public List<NotificationsObj> NotificationsObj { get; set; }
    }

    public class RootObject
    {
        public List<ErrorObj> ErrorObj { get; set; }
        public ResponseObj ResponseObj { get; set; }
    }
}
