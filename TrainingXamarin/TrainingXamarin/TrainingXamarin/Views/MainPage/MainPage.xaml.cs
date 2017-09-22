using System;
using System.Collections.Generic;
using System.Globalization;
using CarouselView.FormsPlugin.Abstractions;
using TrainingXamarin.Models;
using TrainingXamarin.TodoCreation;
using TrainingXamarin.Views.MainPage;
using Xamarin.Forms;

namespace TrainingXamarin
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel();
        }

        public void Add_Work_Button(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TodoCreationPage());
        }
    }
}