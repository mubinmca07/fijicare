using Android.Content;
using Android.Hardware;
using Android.Runtime;
using Fijicare.Helper;
using Fijicare.Droid;
using Fijicare.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(StepCounter))]
namespace Fijicare.Droid
{

    public class StepCounter : Java.Lang.Object, IStepCounter, ISensorEventListener
    {

        private int counterSteps = 0;
        private int stepDetector = 0;
        private long StepsCounter = 0;


        private SensorManager sManager;

        public long StepNotStored
        {
            get { return StepsCounter; }
            set { StepsCounter = value; }
        }

        public new void Dispose()
        {
            sManager.UnregisterListener(this);
            sManager.Dispose();
        }

        public void InitSensorService()
        {

            sManager = Android.App.Application.Context.GetSystemService(Context.SensorService) as SensorManager;

            sManager.RegisterListener(this, sManager.GetDefaultSensor(SensorType.StepCounter), SensorDelay.Normal);
        }

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
        }
        private long milestoneStep;
        public void OnSensorChanged(SensorEvent sensorEvent)
        {
            switch (sensorEvent.Sensor.Type)
            {
                case SensorType.StepDetector:
                    stepDetector++;
                    break;
                case SensorType.StepCounter:
                    {


                        Settings.StepSinceReboot = (long)sensorEvent.Values[0];
                        MessagingCenter.Send<string>("1", "Stepcount");
                      //  Android.Widget.Toast.MakeText(Android.App.Application.Context, "Step Counts 1="+ Settings.StepSinceReboot, Android.Widget.ToastLength.Short).Show();
                        if (Settings.StepSinceReboot == 0)
                        {
                            if (Settings.LastStep != 0)
                            {
                                Settings.LastStep = 0;
                                Settings.NotSavedSteps = Settings.todaySteps;
                            }

                        }
                        else
                        {

                            Settings.todaySteps = Settings.StepSinceReboot - Settings.LastStep;
                        }

                       // Android.Widget.Toast.MakeText(Android.App.Application.Context,
                        //     "Sensor++" , Android.Widget.ToastLength.Short).Show();
                        break;
                    }
            }
        }


        public void StopSensorService()
        {
            sManager.UnregisterListener(this);
        }

        public bool IsAvailable()
        {
            return Android.App.Application.Context.PackageManager.HasSystemFeature(Android.Content.PM.PackageManager.FeatureSensorStepCounter)
                && Android.App.Application.Context.PackageManager.HasSystemFeature(Android.Content.PM.PackageManager.FeatureSensorStepDetector);
        }


    }
}