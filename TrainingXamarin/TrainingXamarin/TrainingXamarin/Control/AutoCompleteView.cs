using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace TrainingXamarin.Control
{
    public class AutoCompleteView : View
    {
        public static readonly BindableProperty TextChangedProperty =
            BindableProperty.Create<AutoCompleteView, string>(p => p.TextChanged, "", BindingMode.TwoWay, propertyChanged: TextChangedPropertyChanged);

        public string TextChanged
        {
            get { return (string)GetValue(TextChangedProperty); }
            set { SetValue(TextChangedProperty, value); }
        }

        private static void TextChangedPropertyChanged(BindableObject bindable, string oldValue, string newValue)
        {
            var control = (AutoCompleteView)bindable;
            if (control == null || control.TextChangedCommand == null) return;
            control.TextChangedCommand.Execute(newValue);
        }

        public static readonly BindableProperty TextChangedCommandProperty =
            BindableProperty.Create<AutoCompleteView, ICommand>(p => p.TextChangedCommand, null);

        public ICommand TextChangedCommand
        {
            get { return (ICommand)GetValue(TextChangedCommandProperty); }
            set { SetValue(TextChangedCommandProperty, value); }
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create<AutoCompleteView, ObservableCollection<string>>(p => p.ItemsSource, null, BindingMode.TwoWay);

        public ObservableCollection<string> ItemsSource
        {
            get { return (ObservableCollection<string>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty ItemChangedProperty =
            BindableProperty.Create<AutoCompleteView, string>(p => p.ItemChanged, "", BindingMode.TwoWay, propertyChanged: ItemPropertyChanged);
        

        public string ItemChanged
        {
            get { return (string)GetValue(ItemChangedProperty); }
            set { SetValue(ItemChangedProperty, value); }
        }

        private static void ItemPropertyChanged(BindableObject bindable, string oldValue, string newValue)
        {
            var control = (AutoCompleteView)bindable;
            if (control == null || control.ItemChangedCommand == null) return;
            control.ItemChangedCommand.Execute(newValue);
        }

        public static readonly BindableProperty ItemChangedCommandProperty =
            BindableProperty.Create<AutoCompleteView, ICommand>(p => p.ItemChangedCommand, null);

        public ICommand ItemChangedCommand
        {
            get { return (ICommand)GetValue(ItemChangedCommandProperty); }
            set { SetValue(ItemChangedCommandProperty, value); }
        }
    }
}
