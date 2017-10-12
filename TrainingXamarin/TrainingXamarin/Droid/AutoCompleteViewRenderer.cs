using TrainingXamarin.Control;
using Xamarin.Forms;
using TrainingXamarin.Droid;
using Android.Widget;
using System.ComponentModel;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AutoCompleteView), typeof(AutoCompleteViewRenderer))]
namespace TrainingXamarin.Droid
{
    public class AutoCompleteViewRenderer : ViewRenderer<AutoCompleteView, AutoCompleteTextView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<AutoCompleteView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
                return;

            var autoComplete = new AutoCompleteTextView(Forms.Context);
            SetNativeControl(autoComplete);
            Control.TextChanged += OnTextChanged;
            Control.ItemClick += OnItemClick;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Element == null || Control == null)
                return;
            Control.Adapter = new ArrayAdapter(Forms.Context, Android.Resource.Layout.SimpleDropDownItem1Line, Element.ItemsSource);
        }

        async void OnTextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (Control.Text != null) Element.TextChanged = Control.Text;
        }

        async void OnItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (Control.Text != null) Element.ItemChanged = Control.Text;
        }
    }
}
