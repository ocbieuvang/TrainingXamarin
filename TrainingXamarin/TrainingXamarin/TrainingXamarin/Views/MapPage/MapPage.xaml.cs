
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TrainingXamarin.Views.MapPage
{
    public partial class MapPage : ContentPage
    {
        private MapViewModel mMapViewModel;
        public MapPage()
        {
            InitializeComponent();
            mMapViewModel = new MapViewModel(this);
            BindingContext = mMapViewModel;
        }
    }
}
