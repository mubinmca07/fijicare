
using Fijicare.Interfaces;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(Fijicare.Droid.Native.ContentPath))]

namespace Fijicare.Droid.Native
{

    public class ContentPath : IContentPath
    {
        public string GetContentPath()
        {
            string directory = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);
           
            return directory;
        }

        public string GetContentPathIOsRythms()
        {
            return "";
        }
    }
   
}



