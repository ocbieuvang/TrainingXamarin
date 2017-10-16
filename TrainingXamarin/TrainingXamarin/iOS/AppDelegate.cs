using Foundation;
using UIKit;
using CarouselView.FormsPlugin.iOS;
using Google.Maps;

namespace TrainingXamarin.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            var apikey = "AIzaSyCXfHhoBNynUtBdSigDfO0lClJjJi2EKvo";
            PlacesClient.ProvideApiKey(apikey);
            MapServices.ProvideAPIKey(apikey);
            CarouselViewRenderer.Init();
            Xamarin.FormsMaps.Init();

            LoadApplication(new App());

            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var notificationSettings = UIUserNotificationSettings.GetSettingsForTypes(
                    UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null
                );
                app.RegisterUserNotificationSettings(notificationSettings);
            }

            if (options != null)
            {
                if (options.ContainsKey(UIApplication.LaunchOptionsLocalNotificationKey))
                {
                    var localNotification = options[UIApplication.LaunchOptionsLocalNotificationKey] as UILocalNotification;
                    if (localNotification != null)
                    {
                        UIAlertController okayAlertController = UIAlertController.Create(localNotification.AlertAction, localNotification.AlertBody, UIAlertControllerStyle.Alert);
                        okayAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

                        app.KeyWindow.RootViewController.PresentViewController(okayAlertController, true, null);

                        UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
                    }
                }
            }

            return base.FinishedLaunching(app, options);
        }

        public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
        {
            UIAlertController okayAlertController = UIAlertController.Create(notification.AlertAction, notification.AlertBody, UIAlertControllerStyle.Alert);
            okayAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

            if (application != null)
            {
                application.KeyWindow.RootViewController.PresentViewController(okayAlertController, true, null);
            }

            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
        }
    }
}
