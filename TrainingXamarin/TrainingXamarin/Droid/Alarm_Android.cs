using System;
using Android.App;
using Android.Content;
using TrainingXamarin.Droid;
using TrainingXamarin.Model;
using Xamarin.Forms;

[assembly: Dependency(typeof(Alarm_Android))]
namespace TrainingXamarin.Droid
{
    public class Alarm_Android : IAlarm
    {
        PendingIntent pendingIntent;
        Intent myIntent = new Intent(Android.App.Application.Context, typeof(AlarmNotificationReceiver));
        AlarmManager manager = (AlarmManager)Android.App.Application.Context.GetSystemService(Context.AlarmService);

        public Alarm_Android()
        {
        }

        public void CancelAlarm(Todo work)
        {
            var service = PendingIntent.GetBroadcast(Android.App.Application.Context, work.ID, myIntent, PendingIntentFlags.UpdateCurrent);
            manager.Cancel(service);
        }

        public void SetAlarm(Todo work)
        {
            myIntent.PutExtra("work", work.Title);
            pendingIntent = PendingIntent.GetBroadcast(Android.App.Application.Context, work.ID, myIntent, PendingIntentFlags.UpdateCurrent);
            TimeSpan span = work.From - DateTime.Now;
            long schedule = (long)(Java.Lang.JavaSystem.CurrentTimeMillis() + span.TotalMilliseconds);
            manager.Set(AlarmType.RtcWakeup, schedule, pendingIntent);
        }
    }
}
