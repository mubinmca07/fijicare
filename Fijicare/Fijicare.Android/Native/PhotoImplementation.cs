using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Fijicare.Interfaces;
using Plugin.Media;

[assembly: Xamarin.Forms.Dependency(typeof(Fijicare.Droid.Native.PhotoImplementation))]
namespace Fijicare.Droid.Native
{
    public class PhotoImplementation : Java.Lang.Object, IPhoto
    {
        public async Task<Stream> GetPhoto()
        {
            // Open the photo and put it in a Stream to return       
            MemoryStream memoryStream = new MemoryStream();
            Plugin.Media.Abstractions.MediaFile file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
                return null;
            using (FileStream source = System.IO.File.OpenRead(file.Path))
            {
                await source.CopyToAsync(memoryStream);
            }

            return memoryStream;
        }
    }
}
