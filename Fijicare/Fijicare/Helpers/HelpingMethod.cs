using System;
namespace Fijicare.Helpers
{
    public static class HelpingMethod
    {
        public static decimal GetDistanceBySteps(long steps)
        {
            var distance = (decimal)(steps * 78) / 100000;
            return distance;
        }

    }
}
