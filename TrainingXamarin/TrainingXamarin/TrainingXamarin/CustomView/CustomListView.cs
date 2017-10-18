using System.Windows.Input;
using Xamarin.Forms;

namespace TrainingXamarin.CustomView
{
    public class CustomListView : ListView
    {
        public CustomListView()
        {
            this.ItemTapped += (s, e) =>
            {
                SelectedCommand.Execute(s);
            };
        }

        public ICommand SelectedCommand
        {
            get { return (ICommand)GetValue(SelectedCommandProperty); }
            set { SetValue(SelectedCommandProperty, value); }
        }

        public static readonly BindableProperty SelectedCommandProperty =
            BindableProperty.Create("SelectedCommand", typeof(ICommand), typeof(CustomListView), null);

    }
}
