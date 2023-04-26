
using Android.App;
using Android.OS;
using Fijicare.Droid;

namespace Fijicare.Droid
{
    [Activity(Label = "FijiCare",
        LaunchMode = Android.Content.PM.LaunchMode.SingleTop,
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait,

        Theme = "@style/Theme.Splash",
        Icon = "@drawable/icon",
         MainLauncher = true,
         NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var mainIntent = new Android.Content.Intent(Application.Context, typeof(MainActivity));

            mainIntent.SetFlags(Android.Content.ActivityFlags.SingleTop);

            StartActivity(mainIntent);
        }
    }
}