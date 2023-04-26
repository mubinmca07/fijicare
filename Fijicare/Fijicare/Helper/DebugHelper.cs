using System;
using System.Diagnostics;

namespace Fijicare.Helper
{
    public class DebugHelper
    {
        public static void ResponseData(string json,string content)
        {
#if DEBUG
            Debug.WriteLine("-------------APIResponce------------");
            Debug.WriteLine(json);
            Debug.WriteLine(content);
            Debug.WriteLine("-------------APIResponce------------");
#endif
        }

        public static void WriteLine(string message, bool isToasted = false)
        {
            Debug.WriteLine(message);
        }

        public static void WriteLineError(Exception ex, bool isToasted = false)
        {
            WriteLine("Error: " + ex.Message + "\n" + ex.StackTrace, isToasted);
        }

        public static void WriteLineError(string functionName, Exception ex)
        {
            WriteLine("Error: " + functionName + "=>" + ex.Message + "\n" + ex.StackTrace);
        }

        public static void ShowConsole(Exception exception, bool trackAnalytic = false)
        {
            if (trackAnalytic)
            {

            }
            Debug.WriteLine(
                $"Source : {exception.Source}\n " +
                $"Message : {exception.Message}\n" +
                $"StackTrace : {exception.StackTrace}"
            );
        }

        public static void ShowConsole(string message, bool trackAnalytic = false)
        {
            if (trackAnalytic)
            {

            }
            Debug.WriteLine(message);
        }
    }
}
