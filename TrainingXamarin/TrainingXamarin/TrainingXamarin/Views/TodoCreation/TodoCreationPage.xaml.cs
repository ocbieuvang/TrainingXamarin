using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TrainingXamarin.TodoCreation
{
    public partial class TodoCreationPage : ContentPage
    {
        private TodoCreationViewModel mViewModel;

        public TodoCreationPage(object value)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            if (value != null)
            {
                mViewModel = new TodoCreationViewModel(this, value);
            }
            else
            {
                mViewModel = new TodoCreationViewModel(this);
            }

            BindingContext = mViewModel;
        }
    }
}
