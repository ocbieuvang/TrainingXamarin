using System;
using System.Collections.Generic;
using System.Globalization;
using CarouselView.FormsPlugin.Abstractions;
using TrainingXamarin.Model;
using TrainingXamarin.Models;
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
    }
}