using System;
using Android.App;
using Android.Content;
using Android.OS;
using Fijicare.Dbhelper;
using Fijicare.Helper;
using Fijicare.Interfaces;
using Xamarin.Forms;

namespace Fijicare.Droid
{
    [Service]
    public class DataSource : Service
    {

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
        public const int ServiceRunningNotifID = 9000;

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Notification notif = DependencyService.Get<INotification>().ReturnNotif();
            StartForeground(ServiceRunningNotifID, notif);
            DoLongRunningOperationThings();//to be implement
            return StartCommandResult.Sticky;
        }

      /*  public override void OnTaskRemoved(Intent rootIntent)
        {
            // ensureServiceStaysRunning();
            Intent intent = new Intent(global::Android.App.Application.Context, typeof(DataSource));
            PendingIntent pendingIntent = PendingIntent.GetService(this, 1, intent, PendingIntentFlags.OneShot);
            AlarmManager alarmManager = (AlarmManager)GetSystemService(Context.AlarmService);
            alarmManager.Set(AlarmType.RtcWakeup, SystemClock.ElapsedRealtime() + 5000, pendingIntent);

            base.OnTaskRemoved(rootIntent);
        }*/

       /* private void ensureServiceStaysRunning()
        {
            DependencyService.Get<Interfaces.IAndroidService>().StartService();
        }
        int stopserviceafter20second = 0;*/
        private void DoLongRunningOperationThings()
        {

            Device.StartTimer(new TimeSpan(0, 0, 60), () =>
            {
                // do something every 60 seconds
                Device.BeginInvokeOnMainThread(async () =>
                // System.Threading.Tasks.Task.Run( async () => 
                {
                    DependencyService.Get<IStepCounter>().InitSensorService();
                    //stopserviceafter20second++;
                    Settings.todaySteps = Settings.StepSinceReboot - Settings.LastStep;

                    StepCounterDatabase stepCounterDatabase = await StepCounterDatabase.Instance;
                 //var items = await stepCounterDatabase.GetTodayDataAsync();
                 if (Settings.todaySteps != 0)
                    {
                     // if (items == null || items.Count == 0)
                     {

                            StepTable stepTable = new StepTable();
                            stepTable.DailySteps = Settings.todaySteps + Settings.NotSavedSteps;
                            stepTable.CreateDate = DateTime.Now.ToString("G");
                            stepTable.Created = DateTime.Now;
                            var insertitem = await stepCounterDatabase.SaveItemAsync(stepTable);
                            Settings.NotSavedSteps = 0;


                        }
                        Settings.LastStep = Settings.StepSinceReboot;
                     /*else
                     {
                         StepTable stepTable = items[0];
                         stepTable.DailySteps = todaySteps;
                         stepTable.CreateDate = DateTime.Now.ToString("dd-MM-yyyy");
                         stepTable.Created = DateTime.Now;
                         await stepCounterDatabase.SaveItemAsync(stepTable);
                     }*/
                    }
                });
               // if (stopserviceafter20second == 3)
               //     return false; // runs again, or false to stop
              //  else
              return true;
            });

        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override bool StopService(Intent name)
        {
            return base.StopService(name);
        }
    }

    public interface INotification
    {
        Notification ReturnNotif();
    }
}
