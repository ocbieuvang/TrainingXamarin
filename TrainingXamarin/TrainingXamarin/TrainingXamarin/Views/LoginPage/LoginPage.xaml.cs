using System;
using TrainingXamarin.Introduce;
using Xamarin.Forms;

namespace TrainingXamarin
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void login_Clicked(object sender, EventArgs e)
        {
            var userName = username.Text;
            var passWord = password.Text;

            if (Application.Current.Properties.ContainsKey("firstLogin"))
            {
                var login = (Boolean)Application.Current.Properties["firstLogin"];
                if (login)
                {
                    await Navigation.PushAsync(new IntroducePage());
                    Application.Current.Properties["firstLogin"] = false;
                }
                else
                {
                    await Navigation.PushAsync(new MainPage());
                }
            }
            else if (userName != "admin" || passWord != "admin")
            {
                await DisplayAlert("Alert", "Wrong username or password!", "OK");
            }

            else
            {
                Application.Current.Properties["firstLogin"] = true;
            }
        }
    }
}
