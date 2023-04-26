using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fijicare.Utility
{
   public class Helper
    {
        public static string GetCurrencyFormatting(string amt)
        {
            string temp = string.Empty;
            if (!string.IsNullOrEmpty(amt))
            {
                if (amt.Contains("$"))
                    temp = amt.Replace("$", "");
                double cvramt = 0;

                double.TryParse(amt, out cvramt);
                temp = cvramt.ToString("C", new System.Globalization.CultureInfo("en-US"));

            }
            return temp;
        }

        public static string GetDeviceMacAddress()
        {
            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                    netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    PhysicalAddress address = netInterface.GetPhysicalAddress();
                    return BitConverter.ToString(address.GetAddressBytes());

                }
            }

            return "NoMac";
        }


        private static Dictionary<char, int> RomanMap = new Dictionary<char, int>()
    {
        {'I', 1},
         {'G', 0},
        {'V', 5},
        {'X', 10},
        {'L', 50},
        {'C', 100},
        {'D', 500},
        {'M', 1000}
    };
        public static int RomanToInteger(string roman)
        {
            int number = 0;
            for (int i = 0; i < roman.Length; i++)
            {
                if (i + 1 < roman.Length && RomanMap[roman[i]] < RomanMap[roman[i + 1]])
                {
                    number -= RomanMap[roman[i]];
                }
                else
                {
                    number += RomanMap[roman[i]];
                }
            }
            return number;
        }

        public static bool ValidateEmail(string email)
        {            
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            else
                return false;
        }

        
    }
}
