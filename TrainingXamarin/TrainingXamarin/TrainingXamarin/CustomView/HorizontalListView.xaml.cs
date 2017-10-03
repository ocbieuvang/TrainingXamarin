using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TrainingXamarin.CustomView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HorizontalListView : Xamarin.Forms.ScrollView
    {
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
                                                         propertyName: "ItemsSource",
                                                         returnType: typeof(ObservableCollection<DateTime>),
                                                         declaringType: typeof(HorizontalListView),
                                                         defaultValue: null,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: ItemSourcePropertyChanged);

        public HorizontalListView()
        {
            InitializeComponent();
        }

        public ObservableCollection<DateTime> ItemsSource { get; set; }

        public ICommand SelectedCommand
        {
            get { return (ICommand)GetValue(SelectedCommandProperty); }
            set { SetValue(SelectedCommandProperty, value); }
        }

        public static readonly BindableProperty SelectedCommandProperty =
			BindableProperty.Create("SelectedCommand", typeof(ICommand), typeof(HorizontalListView), null);


        public static void ItemSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (HorizontalListView)bindable;

            control.wrapper.Children.Clear();

            if (newValue != null)
            {
                control.ItemsSource = (ObservableCollection<DateTime>)newValue;

                foreach (var item in control.ItemsSource)
                {
                    var itemView = new Label()
                    {
                        WidthRequest = 48,
                        HeightRequest = 100,
                        Text = item.DayOfWeek.ToString().Substring(0, 3),
                        VerticalTextAlignment= TextAlignment.Center
                    };

                    var command = new Command((obj) =>
                    {
                        var args = new ItemTappedEventArgs(control.ItemsSource, item);
                        if(control.SelectedCommand != null)
                        {
                            control.SelectedCommand.Execute(item);
						}
                    });

                    itemView.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = command,
                        NumberOfTapsRequired = 1
                    });
                    control.wrapper.Children.Add(itemView);
                }
            }
        }
    }
}
