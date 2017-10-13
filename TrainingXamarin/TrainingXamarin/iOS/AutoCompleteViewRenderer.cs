using TrainingXamarin.Control;
using TrainingXamarin.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using MBAutoComplete;
using System.Collections.Generic;
using UIKit;
using Foundation;
using System.Linq;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(AutoCompleteView), typeof(AutoCompleteViewRenderer))]
namespace TrainingXamarin.iOS
{
    public class AutoCompleteViewRenderer : ViewRenderer<AutoCompleteView, UITextField>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<AutoCompleteView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
                return;

            var autoComplete = new UITextField();
            SetNativeControl(autoComplete);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Element == null || Control == null)
                return;
        }
    }
}
