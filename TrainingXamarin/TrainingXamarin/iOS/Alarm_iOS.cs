﻿using System;
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

            UIApplication.SharedApplication.CancelLocalNotification(notification); 
        }

        public void SetAlarm(Todo work)
        {
			var notification = new UILocalNotification();

            DateTime newDate = new DateTime(2001, 1, 1, 0, 0, 0);
            var secondToDate = (work.From - DateTime.Now).TotalSeconds;

            notification.FireDate = NSDate.FromTimeIntervalSinceNow(secondToDate);

			notification.AlertAction = "To do reminder";
            notification.AlertBody = work.Title + " need to be done!";

			notification.ApplicationIconBadgeNumber = 1;

            notification.SoundName = UILocalNotification.DefaultSoundName;

			UIApplication.SharedApplication.ScheduleLocalNotification(notification);
        }
    }
}
