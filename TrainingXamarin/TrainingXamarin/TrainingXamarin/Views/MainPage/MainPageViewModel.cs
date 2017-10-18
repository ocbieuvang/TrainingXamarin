using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;
using TrainingXamarin.Model;
using TrainingXamarin.Models;
using TrainingXamarin.TodoCreation;
using Xamarin.Forms;

namespace TrainingXamarin.Views.MainPage
{
    public class MainPageViewModel : BaseViewModel
    {
        List<string> list_month = new List<string>(DateTimeFormatInfo.CurrentInfo.MonthNames);
        public ObservableCollection<string> Items { get; set; }

        private ObservableCollection<Todo> listToDo;

        private ObservableCollection<DateTime> dayInMonth;

        private bool _isRefreshing = false;
        private bool _isLoading = false;

        private ContentPage mainPage;

        internal void OnAppearing()
        {
            GetLstToDo(SelectedDate);
        }

        private DateTime SelectedDate = DateTime.Now;

        public MainPageViewModel(ContentPage page)
        {
            mainPage = page;
            Items = new ObservableCollection<string>(list_month);

            var days = AllDatesInMonth(DateTime.Now.Year, DateTime.Now.Month);

            DaysInMonth = new ObservableCollection<DateTime>(days);

            Position = list_month.IndexOf(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month));

            GetLstToDo(DateTime.Now);
            SelectedDate = DateTime.Now;

            ClickCommand = new Command(DateClickedCommand);

            Prev_Button_Clicked = new Command(HandleAction_PrevButton);
            Next_Button_Clicked = new Command(HandleAction_NextButton);
            ShowMenuClick = new Command(HandleShowMenu);
            AddWorkButton = new Command(HandleAddWorkButton);
            EditToDoCommand = new Command(HandleEditTodoCommand);
        }

        private async void HandleAddWorkButton()
        {
            if (SelectedDate >= DateTime.Now.Date)
            {
                Todo todo = new Todo
                {
                    From = SelectedDate,
                    To = SelectedDate
                };
                await mainPage.Navigation.PushAsync(new TodoCreationPage(todo));
            }
            else
            {
                await mainPage.DisplayAlert("Alart", "Date create must greater than now!", "OK");
            }
        }

        private void HandleShowMenu(object value)
        {
            (App.Current.MainPage as MasterDetailPage).IsPresented = true;
        }

        public async void HandleEditTodoCommand(object value)
        {
            var item = ((ListView)value).SelectedItem;
            Todo todo = ((Todo)item);
            var action = await mainPage.DisplayActionSheet(todo.Title, "Cancel", null, "Edit", "Delete");
            switch (action)
            {
                case "Edit":
                    await mainPage.Navigation.PushAsync(new TodoCreationPage(item));
                    break;
                case "Delete":
                    DeleteWorkToDoCommand(item);
                    break;
            }
        }

        public async void DeleteWorkToDoCommand(object value)
        {
            var action = await mainPage.DisplayAlert("Delete confirm", "Do you really want to delete this todo work?", "Yes", "No");
            if (action)
            {
                await App.Database.DeleteAsync((Todo)value);
                GetLstToDo(SelectedDate);
            }
        }

        void DateClickedCommand(object value)
        {
            var dateClick = (DateTime)value;
            GetLstToDo(dateClick);
            SelectedDate = dateClick;
        }

        public ICommand ClickCommand
        {
            get;
            private set;
        }

        public ICommand Prev_Button_Clicked
        {
            get; private set;
        }

        public ICommand Next_Button_Clicked
        {
            get; private set;
        }

        public ICommand ShowMenuClick
        {
            get;
            private set;
        }

        public ICommand AddWorkButton
        {
            get;
            private set;
        }

        public ICommand EditToDoCommand
        {
            get;
            private set;
        }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                pushPropertyChanged(nameof(IsRefreshing));
            }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                pushPropertyChanged(nameof(IsLoading));
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(() =>
                {
                    IsRefreshing = true;

                    GetLstToDo(SelectedDate);

                    IsRefreshing = false;
                });
            }
        }
        public ObservableCollection<DateTime> DaysInMonth
        {
            get
            {
                return dayInMonth;
            }
            set
            {
                if (dayInMonth != value)
                {
                    dayInMonth = value;
                    pushPropertyChanged("DaysInMonth");
                }
            }
        }

        private int _position;

        public int Position
        {
            get { return _position; }
            set
            {
                if (_position != value)
                {
                    _position = value;
                    pushPropertyChanged("Position");
                }
            }
        }

        public async void GetLstToDo(DateTime date)
        {
            IsLoading = true;
            List<Todo> todos = await App.Database.GetItemInDateAsync(date);
            ListToDo = new ObservableCollection<Todo>(todos);
            IsLoading = false;
        }

        public ObservableCollection<Todo> ListToDo
        {
            get
            {
                return listToDo;
            }
            set
            {
                listToDo = value;
                pushPropertyChanged("ListToDo");
            }
        }

        void HandleAction_PrevButton()
        {
            if (Position > 0)
                Position--;
            else
                Position = 11;
            DaysInMonth = new ObservableCollection<DateTime>(ListAllDayMonth());
        }

        void HandleAction_NextButton()
        {
            if (Position < 11)
                Position++;
            else
                Position = 0;
            DaysInMonth = new ObservableCollection<DateTime>(ListAllDayMonth());
        }

        IEnumerable<DateTime> ListAllDayMonth()
        {
            var month_pos = DateTime.ParseExact(list_month[Position].Substring(0, 3), "MMM", CultureInfo.InvariantCulture).Month;
            return AllDatesInMonth(DateTime.Now.Year, month_pos);
        }

        public static IEnumerable<DateTime> AllDatesInMonth(int year, int month)
        {
            int days = DateTime.DaysInMonth(year, month);
            for (int day = 1; day <= days; day++)
            {
                yield return new DateTime(year, month, day);
            }
        }
    }
}

