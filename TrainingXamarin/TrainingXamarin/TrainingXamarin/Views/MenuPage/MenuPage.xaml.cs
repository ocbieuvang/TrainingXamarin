using System;
using Xamarin.Forms;

namespace TrainingXamarin
{
    public partial class MenuPage : MasterDetailPage
    {
        public MenuPage()
        {
            InitializeComponent();
            Detail = new NavigationPage(new LoginPage());
            IsPresented = false;
        }

        void navIntroPage(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new IntroPage());
            IsPresented = false;
        }

		void navMainPage(object sender, EventArgs e)
		{
            Detail = new NavigationPage(new MainPage());
            IsPresented = false;
		}
    }
}
