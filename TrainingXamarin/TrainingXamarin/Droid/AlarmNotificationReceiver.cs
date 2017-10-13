using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.V4.Content;

namespace TrainingXamarin.Droid
{
    [BroadcastReceiver(Enabled = true)]
    public class AlarmNotificationReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            const string NotificationPermission = Manifest.Permission.AccessNotificationPolicy;
            const string SetAlarmPermission = Manifest.Permission.SetAlarm;

            if (ContextCompat.CheckSelfPermission(context, NotificationPermission) == (int)Permission.Granted && ContextCompat.CheckSelfPermission(context, SetAlarmPermission) == (int)Permission.Granted)
            {
                NotificationCompat.Builder builder = new NotificationCompat.Builder(context);
                builder.SetAutoCancel(true)
                    .SetDefaults((int)NotificationDefaults.All)
                    .SetSmallIcon(Resource.Drawable.xamarin_icon)
                    .SetContentTitle("To do reminder")
                       .SetContentText(intent.GetStringExtra("work") + " need to be done!")
                    .SetContentInfo("Info");

                NotificationManager manager = (NotificationManager)context.GetSystemService(Context.NotificationService);
                manager.Notify(0, builder.Build());
                return;
            }
        }
        readonly string[] PermissionsNotification =
            {
                Manifest.Permission.AccessNotificationPolicy,
                Manifest.Permission.SetAlarm
            };
    }
}