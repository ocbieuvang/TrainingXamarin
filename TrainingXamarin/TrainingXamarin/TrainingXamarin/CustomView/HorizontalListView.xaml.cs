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
        private static Label previousButton;
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
            Label labelScrollTo = new Label();
            Label labelFirst = new Label();
            control.wrapper.Children.Clear();

            if (newValue != null)
            {
                control.ItemsSource = (ObservableCollection<DateTime>)newValue;

                foreach (var item in control.ItemsSource)
                {
                    var stack = new StackLayout()
                    {
                        WidthRequest = 45,
                        Margin = new Thickness(0, 5, 0, 0)
                    };

                    var itemView = new Label()
                    {
                        WidthRequest = 45,
                        Text = item.DayOfWeek.ToString().Substring(0, 3),
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                        TextColor = Color.Gray
                    };

                    var button = new Label()
                    {
                        Text = item.Day.ToString("D"),
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalTextAlignment = TextAlignment.Center,
                    };

                    if (DateTime.Now.Day == item.Day && DateTime.Now.Month == item.Month)
                    {
                        labelScrollTo = button;
                        previousButton = button;
                        button.TextColor = Color.FromHex("ff3366");
                    }
                    else
                    {
                        if (Convert.ToInt16(item.Day.ToString("D")) == 1)
                        {
                            labelFirst = button;
                        }
                    }

                    stack.Children.Add(itemView);
                    stack.Children.Add(button);

                    var command = new Command(() =>
                    {
                        var args = new ItemTappedEventArgs(control.ItemsSource, item);
                        if (control.SelectedCommand != null)
                        {
                            control.SelectedCommand.Execute(item);
                        }

                        if (previousButton != null)
                        {
                            previousButton.TextColor = Color.Black;
                        }
                        button.TextColor = Color.FromHex("ff3366");
                        previousButton = button;
                    });

                    button.GestureRecognizers.Add(new TapGestureRecognizer
                    {
                        Command = command,
                        NumberOfTapsRequired = 1,
                    });
                    control.wrapper.Children.Add(stack);
                }
                if (DateTime.Now.Day.ToString() == labelScrollTo.Text)
                {
                    control.ScrollToAsync(labelScrollTo, ScrollToPosition.Center, true);
                }
                else
                {
                    control.ScrollToAsync(labelFirst, ScrollToPosition.Start, true);
                }
            }
        }
    }
}
