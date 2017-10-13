using System;
using TrainingXamarin.TodoCreation;
using TrainingXamarin.Views.MainPage;
using Xamarin.Forms;

namespace TrainingXamarin
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel viewModel;
        public MainPage()
        {
            InitializeComponent();

            viewModel = new MainPageViewModel(this);
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }

        public void EditToDoCommand(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TodoCreationPage(((ListView)sender).SelectedItem));
        }

        public void Add_Work_Button(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TodoCreationPage(null));
        }
    }
}