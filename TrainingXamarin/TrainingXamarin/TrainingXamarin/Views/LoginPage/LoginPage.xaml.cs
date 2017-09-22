using System;
using TrainingXamarin.Introduce;
using Xamarin.Forms;

namespace TrainingXamarin
{
    public partial class LoginPage : ContentPage
    {
        int i = 0;
        public LoginPage()
        {
            InitializeComponent();
        }

        async void login_Clicked(object sender, EventArgs e)
        {
            var userName = username.Text;
            var passWord = password.Text;

            if (userName == "admin" && passWord == "admin")
            {
                if (i == 0)
                {
                    await Navigation.PushAsync(new IntroducePage());
                }
                else
                {
                    await Navigation.PushAsync(new MainPage());
                }
                i++;
            }
            else
            {
                await DisplayAlert("Alert", "Wrong username or password!", "OK");
            }
        }
    }
}
