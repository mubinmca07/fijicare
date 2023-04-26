using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Fijicare.Utility
{
    public static class EventsList
    {
        public static event Action<string> OnMessageRecived;
        public static void MessageRecived(string _msg)
        {
            if (OnMessageRecived != null)
                OnMessageRecived(_msg);
        }


        public static event Action<string> OnDialogeMessage;
        public static void DialogeMessage(string _msg)
        {
            if (OnDialogeMessage != null)
                OnDialogeMessage(_msg);
        }
        public static event Action<string> OnCreatPassword;
        public static void CreatPassword(string _msg)
        {
            if (OnCreatPassword != null)
                OnCreatPassword(_msg);
        }

        public static event Action<string> OnLoginFieldFocusChanged;
        public static void LoginFieldFocusChanged(string _msg)
        {
            if (OnLoginFieldFocusChanged != null)
                OnLoginFieldFocusChanged(_msg);
        }


        public static event Action<string> OnImageChange;
        public static void ImageChange(string _msg)
        {
            if (OnImageChange != null)
                OnImageChange(_msg);
        }

    }
}
