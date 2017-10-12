using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TrainingXamarin.CustomView
{
    public class CustomMap : Map
    {
        public static readonly BindableProperty PositionChangedProperty =
            BindableProperty.Create<CustomMap, Position>(p => p.PositionChanged, new Position(), BindingMode.TwoWay, propertyChanged: PositionPropertyChanged);

        public Position PositionChanged
        {
            get { return (Position)GetValue(PositionChangedProperty); }
            set { SetValue(PositionChangedProperty, value); }
        }

        private static void PositionPropertyChanged(BindableObject bindable, Position oldValue, Position newValue)
        {
            var control = (CustomMap)bindable;
            if (control == null) return;
            control.MoveToRegion(MapSpan.FromCenterAndRadius(newValue, Distance.FromMiles(1)));
        }
    }
}
