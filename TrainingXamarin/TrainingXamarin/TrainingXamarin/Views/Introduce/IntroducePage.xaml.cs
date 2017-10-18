using System;
using CarouselView.FormsPlugin.Abstractions;
using Xamarin.Forms;

namespace TrainingXamarin.Introduce
{
    public partial class IntroducePage : ContentPage
    {
        private IntroduceViewModel mViewModel;

        public IntroducePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            mViewModel = new IntroduceViewModel(this);
            BindingContext = mViewModel;

            Carousel_View.PositionSelected += PositionSelected;
        }

        public void PositionSelected(object sender, EventArgs e)
        {
            mViewModel.PositionSelected(sender, ((PositionSelectedEventArgs)e).NewValue);
        }

    }
}
