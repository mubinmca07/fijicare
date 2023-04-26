using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Fijicare.Droid.Native;
using Fijicare.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(Audio))]
namespace Fijicare.Droid.Native
{
    public class Audio : IAudio
    {
        private MediaPlayer _mediaPlayer;

        public bool PlayAudio(string path)
        {
            _mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, Android.Net.Uri.Parse(path));
            _mediaPlayer.Start();
            return true;
        }

     

      public  bool StopAudio()
        {
            if (_mediaPlayer != null)
            {
                _mediaPlayer.SetVolume(0, 0);
                _mediaPlayer.Stop();
                return true;
            }
            return false;
        }
    }
}