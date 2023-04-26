using System;
using System.Collections.Generic;
using System.Text;

namespace Fijicare.Model.Login
{

    

    public class UserDetail
    {
        public int Id { get; set; }
        public string Entity_Cd { get; set; }
        public string Entity_Nm { get; set; }
        public string Contact_Nm { get; set; }
        public string User_Nm { get; set; }
        public object Mobile { get; set; }
        public string Email { get; set; }
        public int Actv_Ind { get; set; }
        public DateTime Lst_Updt_Dt { get; set; }
        public int Lst_Updt_Usr { get; set; }
        public string Lst_Updt_Ip_Addr { get; set; }
        
    }

    public class UserDetailResponseObj
    {
        public List<UserDetail> EntityCredentialObj { get; set; }
    }

    public class UserDetailRootObject
    {
        public List<ErrorObj> ErrorObj { get; set; }
        public UserDetailResponseObj ResponseObj { get; set; }
    }

}
