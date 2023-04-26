using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Database;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Widget;
using Fijicare.Interfaces;
using Fijicare.MyConstants;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;


namespace Fijicare.Droid
{
    [Activity(Label = "FijiCare",
        Icon = "@mipmap/icon",
        HardwareAccelerated =true,
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait,
        Theme = "@style/MainTheme",
        MainLauncher = false,
        Exported = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation
        )
        ]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            await Plugin.Media.CrossMedia.Current.Initialize();

           // DependencyService.Get<IAndroidService>().StartService();
            //Xamarin.Forms.DependencyService.Register<StepCounter>();

            //var simpleWorkerRequest = new OneTimeWorkRequest.Builder(typeof(SimpleWorker))
            //                            .AddTag(SimpleWorker.TAG).Build();

            //WorkManager.GetInstance(this).BeginUniqueWork(
            //SimpleWorker.TAG, ExistingWorkPolicy.Keep, simpleWorkerRequest).Enqueue();



            LoadApplication(new App());
           
        }

        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        //{
        //    Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnResume()
        {
            base.OnResume();

            Xamarin.Essentials.Platform.OnResume();
        }
        private async void   CreateAndShowDialog(String message, String title)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }

        public int OPENGALLERYCODE = 201;

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == OPENGALLERYCODE && resultCode == Result.Ok)
            {
                ObservableCollection<string> images = new ObservableCollection<string>();

                if (data != null)
                {
                    ClipData clipData = data.ClipData;
                    if (clipData != null)
                    {
                        for (int i = 0; i < clipData.ItemCount; i++)
                        {
                            ClipData.Item item = clipData.GetItemAt(i);
                            Android.Net.Uri uri = item.Uri;
                            var path = GetRealPathFromURI(uri);

                            if (path != null)
                            {
                                images.Add(path);
                                ////Rotate Image
                                //var imageRotated = ImageHelpers.RotateImage(path);
                                //var newPath = ImageHelpers.SaveFile("TmpPictures", imageRotated, System.DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                                //images.Add(newPath);
                            }
                        }
                    }
                    else
                    {
                        Android.Net.Uri uri = data.Data;
                        var path = GetRealPathFromURI(uri);

                        if (path != null)
                        {
                            //Rotate Image
                            var imageRotated = ImageHelpers.RotateImage(path);
                            var newPath = ImageHelpers.SaveFile("TmpPictures", imageRotated, System.DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                            // images.Add(newPath);
                            images.Add(path);
                        }
                    }

                    MessagingCenter.Send<App, ObservableCollection<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelected", images);
                }
            }
        }

        protected override void AttachBaseContext(Context @base)
        {
            var configuration = new Android.Content.Res.Configuration(@base.Resources.Configuration);

            configuration.FontScale = 1f;
            var config = @base.CreateConfigurationContext(configuration);

            base.AttachBaseContext(config);
        }

        public String GetRealPathFromURI(Android.Net.Uri contentURI)
        {
            try
            {
                ICursor imageCursor = null;
                string fullPathToImage = "";

                imageCursor = ContentResolver.Query(contentURI, null, null, null, null);
                imageCursor.MoveToFirst();
                int idx = imageCursor.GetColumnIndex(MediaStore.Images.ImageColumns.Data);

                if (idx != -1)
                {
                    fullPathToImage = imageCursor.GetString(idx);
                }
                else
                {
                    ICursor cursor = null;
                    var docID = DocumentsContract.GetDocumentId(contentURI);
                    var id = docID.Split(':')[1];
                    var whereSelect = MediaStore.Images.ImageColumns.Id + "=?";
                    var projections = new string[] { MediaStore.Images.ImageColumns.Data };

                    cursor = ContentResolver.Query(MediaStore.Images.Media.InternalContentUri, projections, whereSelect, new string[] { id }, null);
                    if (cursor.Count == 0)
                    {
                        cursor = ContentResolver.Query(MediaStore.Images.Media.ExternalContentUri, projections, whereSelect, new string[] { id }, null);
                    }
                    var colData = cursor.GetColumnIndexOrThrow(MediaStore.Images.ImageColumns.Data);
                    cursor.MoveToFirst();
                    fullPathToImage = cursor.GetString(colData);
                }
                return fullPathToImage;
            }
            catch (Exception ex)
            {
                Toast.MakeText(Xamarin.Forms.Forms.Context, "Unable to get path", ToastLength.Long).Show();

            }

            return null;

        }

    }



    public static class ImageHelpers
    {
        public static string SaveFile(string collectionName, byte[] imageByte, string fileName)
        {
            var fileDir = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), collectionName);
            if (!fileDir.Exists())
            {
                fileDir.Mkdirs();
            }

            var file = new Java.IO.File(fileDir, fileName);
            System.IO.File.WriteAllBytes(file.Path, imageByte);

            return file.Path;

        }

        public static byte[] RotateImage(string path)
        {
            byte[] imageBytes;

            var originalImage = BitmapFactory.DecodeFile(path);
            var rotation = GetRotation(path);
            var width = (originalImage.Width * 0.25);
            var height = (originalImage.Height * 0.25);
            var scaledImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, true);

            Bitmap rotatedImage = scaledImage;
            if (rotation != 0)
            {
                var matrix = new Matrix();
                matrix.PostRotate(rotation);
                rotatedImage = Bitmap.CreateBitmap(scaledImage, 0, 0, scaledImage.Width, scaledImage.Height, matrix, true);
                scaledImage.Recycle();
                scaledImage.Dispose();
            }

            using (var ms = new MemoryStream())
            {
                rotatedImage.Compress(Bitmap.CompressFormat.Jpeg, 90, ms);
                imageBytes = ms.ToArray();
            }

            originalImage.Recycle();
            rotatedImage.Recycle();
            originalImage.Dispose();
            rotatedImage.Dispose();
            GC.Collect();

            return imageBytes;
        }

        private static int GetRotation(string filePath)
        {
            using (var ei = new ExifInterface(filePath))
            {
                var orientation = (Android.Media.Orientation)ei.GetAttributeInt(ExifInterface.TagOrientation, (int)Android.Media.Orientation.Normal);

                switch (orientation)
                {
                    case Android.Media.Orientation.Rotate90:
                        return 90;
                    case Android.Media.Orientation.Rotate180:
                        return 180;
                    case Android.Media.Orientation.Rotate270:
                        return 270;
                    default:
                        return 0;
                }
            }
        }
    }

    [Activity(NoHistory = true, Exported =true, LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(new[] { Android.Content.Intent.ActionView },
       Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable },
       DataScheme = XAppConstants.CALLBACK_SCHEME),]
    public class WebAuthenticationCallbackActivity : Xamarin.Essentials.WebAuthenticatorCallbackActivity
    {


    }

}