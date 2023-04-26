using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Fijicare.Interfaces;

namespace Fijicare.Helpers
{
    public static class XDRateAndReview
    {
        public static void RateViewOpen()
        {
            IRateAndReview OpenReview = DependencyService.Get<IRateAndReview>();
            OpenReview?.RateAndReviewClick();

        }
    }
}
