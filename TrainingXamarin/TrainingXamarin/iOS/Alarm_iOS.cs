using System;
using Foundation;
using TrainingXamarin.iOS;
using TrainingXamarin.Model;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Alarm_iOS))]
namespace TrainingXamarin.iOS
{
    public class Alarm_iOS:IAlarm
    {
        public Alarm_iOS()
        {
        }

        public void CancelAlarm(Todo work)
        {
            var notification = new UILocalNotification();

            foreach(UILocalNotification noti in UIApplication.SharedApplication.ScheduledLocalNotifications)
            {
                if (noti.FireDate == ConvertDateTimeToNSDate(work.From))
                {
                    UIApplication.SharedApplication.CancelLocalNotification(noti);        
                }
            }
        }

        public void SetAlarm(Todo work)
        {
			var notification = new UILocalNotification();

            DateTime newDate = new DateTime(2001, 1, 1, 0, 0, 0);
            var secondToDate = (work.From - DateTime.Now).TotalSeconds;

            notification.FireDate = ConvertDateTimeToNSDate(work.From);

			notification.AlertAction = "To do reminder";
            notification.AlertBody = work.Title + " need to be done!";

			notification.ApplicationIconBadgeNumber = 1;

            notification.SoundName = UILocalNotification.DefaultSoundName;

			UIApplication.SharedApplication.ScheduleLocalNotification(notification);
        }

        public NSDate ConvertDateTimeToNSDate(DateTime date)
        {
            DateTime newDate = TimeZone.CurrentTimeZone.ToLocalTime(
                new DateTime(2001, 1, 1, 0, 0, 0));
            return NSDate.FromTimeIntervalSinceReferenceDate(
                (date - newDate).TotalSeconds);
        }
    }
}
