
using Android.Content;
using Fijicare.Droid.Native;
using Fijicare.Interfaces;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(ShareIntent))]
namespace Fijicare.Droid.Native
{
    public class ShareIntent : IShareable
    {
        public void OpenShareIntent(string textToShare)
        {
            Intent myIntent = new Intent(Android.Content.Intent.ActionSend);
            myIntent.SetType("text/plain");
            myIntent.PutExtra(Intent.ExtraText, textToShare);
            myIntent.PutExtra(Android.Content.Intent.ExtraOriginatingUri, textToShare);
        
            Forms.Context.StartActivity(Intent.CreateChooser(myIntent, "Choose an App"));
        }
    }

}
