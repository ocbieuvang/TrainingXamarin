using System;
using Android.App;
using Android.Content;

namespace TrainingXamarin.Droid
{
    public class BootReceiver : BroadcastReceiver
    {
        private const String BOOT_COMPLETED = "android.intent.action.BOOT_COMPLETED";
        private const String QUICKBOOT_POWERON = "android.intent.action.QUICKBOOT_POWERON";

        public BootReceiver()
        {
        }

        public override void OnReceive(Context context, Intent intent)
        {
            String action = intent.Action;
            if (action.Equals(BOOT_COMPLETED) || action.Equals(QUICKBOOT_POWERON))
            {
                Intent service = new Intent(context, typeof(BootService));
                context.StartService(service);
            }
        }
    }
}
