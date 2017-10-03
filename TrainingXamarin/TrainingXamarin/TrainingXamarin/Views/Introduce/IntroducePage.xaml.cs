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

        public void PositionSelected(object sender, int position)
        {
            mViewModel.PositionSelected(sender, position);
        }

    }
}
