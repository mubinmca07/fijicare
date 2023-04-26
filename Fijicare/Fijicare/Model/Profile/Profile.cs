using System.Collections.Generic;

namespace Fijicare.Model.Profile
{
    
    public class EntityCredentialObj : ModelBase
    {
        public int Id { get; set; }
        public int Client_Id { get; set; }
        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { SetProperty(ref _Title, value); }
        }
        private string _First_Nm;
        public string First_Nm
        {
            get { return _First_Nm; }
            set { SetProperty(ref _First_Nm, value); }
        }
        private string _Last_Nm;
        public string Last_Nm
        {
            get { return _Last_Nm; }
            set { SetProperty(ref _Last_Nm, value); }
        }
        private string _Address_Line1;
        public string Address_Line1
        {
            get { return _Address_Line1; }
            set { SetProperty(ref _Address_Line1, value); }
        }
        private string _Email1;

        public string Email1
        {
            get { return _Email1; }
            set { SetProperty(ref _Email1, value); }
        }
      
        private string _Email2;
        public string Email2
        {
            get { return _Email2; }
            set { SetProperty(ref _Email2, value); }
        }

        private string _Email3;
        public string Email3
        {
            get { return _Email3; }
            set { SetProperty(ref _Email3, value); }
        }

        private string _Mobile1;
        public string Mobile1
        {
            get { return _Mobile1; }
            set { SetProperty(ref _Mobile1, value); }
        }


        private string _Mobile2;
        public string Mobile2
        {
            get { return _Mobile2; }
            set { SetProperty(ref _Mobile2, value); }
        }

        private string _Mobile3;
        public string Mobile3
        {
            get { return _Mobile3; }
            set { SetProperty(ref _Mobile3, value); }
        }
       
        public string Client_Cd { get; set; }
        public string Entity_Cd { get; set; }
        public string User_Nm { get; set; }
        public string Type { get; set; }
    }

    public class ProfileResponseObj
    {
        public List<EntityCredentialObj> EntityCredentialObj { get; set; }
    }

    public class  ProfileRoot
    {
        public List<ErrorObj> ErrorObj { get; set; }
        public ProfileResponseObj ResponseObj { get; set; }
    }



    

    public class EntityCredentialObjUpd
    {
        public int Id { get; set; }
        public string Entity_Nm { get; set; }
    }

    public class ResponseObjUpd
    {
        public List<EntityCredentialObjUpd> EntityCredentialObj { get; set; }
    }

    public class RootObjectUpd
    {
        public List<ErrorObj> ErrorObj { get; set; }
        public ResponseObjUpd ResponseObj { get; set; }
    }

}
