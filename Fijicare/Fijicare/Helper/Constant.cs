using System;
using System.Runtime.Serialization;

namespace Fijicare.Helper
{
    public static class Constant
    {
        public static string BASE_API_AUTH
        {
            get
            {
                // return "http://localhost:38604/mobileauth/";
                // return "http://127.0.0.1:38604/mobileauth/";
                return "https://www.Fijicare.com.fj/FijiAuthMobile/mobileauth/";
            }
        }

        public static string GoogleDataSetApi
        {
            get
            {
                return "https://www.googleapis.com/fitness/v1/users/me/";
            }
        }

        public static string PASSPHRASE_CIPHER
        {
            get
            {
                return "bSPhFQ5ydpxsB4g";
            }
        }

        public static string BASE_API_USER
        {
            get
            {
                return "https://www.googleapis.com";
            }
        }

        public static string APP_NAME
        {
            get
            {
                return "GoogleFit";
            }
        }


        public const Int32 Caloreis = 2500;

        public const Int32 Steps = 10000;

        public const Int32 Distance = 8;// 8 km

        public const Int32 HeartPoint = 21;

    }
}
