using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TrainingXamarin.TodoCreation
{
    public partial class TodoCreationPage : ContentPage
    {
        private TodoCreationViewModel mViewModel;

        public TodoCreationPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            mViewModel = new TodoCreationViewModel(this);
            BindingContext = mViewModel;
        }
    }
}
