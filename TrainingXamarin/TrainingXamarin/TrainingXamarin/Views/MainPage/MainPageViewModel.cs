using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;
using TrainingXamarin.Model;
using TrainingXamarin.Models;
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

        private DateTime SelectedDate = DateTime.Now;

        public MainPageViewModel()
        {
            Items = new ObservableCollection<string>(list_month);

            var days = AllDatesInMonth(DateTime.Now.Year, DateTime.Now.Month);

            DaysInMonth = new ObservableCollection<DateTime>(days);

            Position = list_month.IndexOf(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month));

            GetLstToDo(DateTime.Now);

            ClickCommand = new Command(DateClickedCommand);

            Prev_Button_Clicked = new Command(HandleAction_PrevButton);
            Next_Button_Clicked = new Command(HandleAction_NextButton);
        }

        void DateClickedCommand(object value)
        {
			GetLstToDo((DateTime)value);
			SelectedDate = (DateTime)value;
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

		public bool IsRefreshing
		{
			get { return _isRefreshing; }
			set
			{
				_isRefreshing = value;
				pushPropertyChanged(nameof(IsRefreshing));
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
            List<Todo> todos = await App.Database.GetItemInDateAsync(date);
            ListToDo = new ObservableCollection<Todo>(todos);
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

