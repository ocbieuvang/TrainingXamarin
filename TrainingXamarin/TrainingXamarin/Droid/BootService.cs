using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using TrainingXamarin.Model;
using Xamarin.Forms;

namespace TrainingXamarin.Droid
{
    public class BootService : IntentService
    {
        public BootService() : base()
        {
        }

        private async void setAlarmsFromDatabase()
        {
            List<Todo> todos = await App.Database.GetListWorkFromNow();

            foreach (Todo work in todos)
            {
                DependencyService.Get<IAlarm>().SetAlarm(work);
            }
        }

        protected override void OnHandleIntent(Intent intent)
        {
            setAlarmsFromDatabase();
            Intent service = new Intent(this, typeof(BootService));
            StopService(service);
        }
    }
}
