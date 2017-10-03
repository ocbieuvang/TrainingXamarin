using TrainingXamarin.Data;
using Xamarin.Forms;

namespace TrainingXamarin
{
    public partial class App : Application
    {
        static TodoDataBase mDatabase;

        public App()
        {
            InitializeComponent();

            MainPage = new MenuPage();
        }

        public static TodoDataBase Database
        {
            get
            {
                if (mDatabase == null)
                {
                    mDatabase = new TodoDataBase(DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLite.db3"));
                }
                return mDatabase;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}