using System;
namespace Fijicare.Helper
{
    public class Utility
    {
        public static DateTime FirstDateInWeek(DateTime dt)
        {
            while (dt.DayOfWeek != System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                dt = dt.AddDays(-1);
            return dt;
        }


        public static bool TokenExpired()
        {
            try
            {
                DateTime dt = Convert.ToDateTime(Settings.token_time);

                dt = dt.AddSeconds((long)Settings.Expired_in);

                if (DateTime.Now > dt)
                    return true;
                else
                    return false;
            }
            catch (Exception )
            {
                return false;
            }
           
        }
    }
}
