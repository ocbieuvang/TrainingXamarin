using System.Collections.ObjectModel;
using System.Windows.Input;
using TrainingXamarin.Model;
using Xamarin.Forms;

namespace TrainingXamarin.Introduce
{
    public class IntroduceViewModel : BaseViewModel
    {
        private int _postition = 0;
        private string _btnName = AppResources.action_next;

        public IntroduceViewModel(ContentPage contentPage)
        {

            Items = new ObservableCollection<Intro>
            {
                new Intro(){ Image = "icon_calender.png",
                    Content = AppResources.title_intro_first},
                new Intro(){ Image = "icon_calender.png",
                    Content = AppResources.title_intro_second}
            };

            OnNextClick = new Command((nothing) =>
            {

                if (_postition == Items.Count - 1)
                {
                    contentPage.Navigation.PushAsync(new MainPage());
                }

                if (_postition < Items.Count - 1)
                {
                    Position++;
                }
            });
        }

        public void PositionSelected(object sender, int position)
        {
            if (position == Items.Count - 1)
            {
                BtnName = AppResources.action_finish;
            }
            else
            {
                BtnName = AppResources.action_next;
            }
        }

        public ObservableCollection<Intro> Items
        {
            get; set;
        }

        public int Position
        {
            set
            {
                if (_postition != value)
                {
                    _postition = value;
                    pushPropertyChanged("Position");
                }
            }
            get
            {
                return _postition;
            }
        }

        public string BtnName
        {
            set
            {
                if (_btnName != value)
                {
                    _btnName = value;
                    pushPropertyChanged("BtnName");
                }
            }
            get
            {
                return _btnName;
            }
        }

        public ICommand OnNextClick
        {
            protected set; get;
        }
    }
}
