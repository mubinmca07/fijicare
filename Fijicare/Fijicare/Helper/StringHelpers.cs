using System;
using System.Text.RegularExpressions;

namespace Fijicare.Helper
{
    public static class StringHelpers
    {
        public static bool IsEmpty(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public static string CheckNullOrNot(this string text)
        {
            return text.IsEmpty() ? "" : text;
        }

        public static bool IsPhoneNumber(this string number)
        {
            return Regex.Match(number, @"^(\+[0-9]{9})$").Success;
        }

        public static string NormalizePhoneNumber(this string phoneNumber)
        {
            try
            {
                if (!phoneNumber.IsEmpty())
                {
                    phoneNumber = phoneNumber.Trim().Replace("-", "").Replace("+", "");
                    phoneNumber = Regex.Replace(phoneNumber, @"\s+", string.Empty);

                    string countrycode = phoneNumber.Substring(0, 2);

                    if (phoneNumber[0] != '0')
                    {
                        phoneNumber = "0" + phoneNumber;
                    }
                }

                return phoneNumber;
            }
            catch
            {
                return phoneNumber;
            }
        }
        public static bool EmailValidator(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var emailPattern = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
                return Regex.IsMatch(email, emailPattern) ? true : false;
            }
            else return false;
        }

        public static bool IsMail(this string email)
        {
            string expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
