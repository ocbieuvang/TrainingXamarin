using System;
using System.Windows.Input;
using TrainingXamarin.Model;
using Xamarin.Forms;

namespace TrainingXamarin.TodoCreation
{
    public class TodoCreationViewModel : BaseViewModel
    {
        private Todo mTodo;
        private TimeSpan mTimeFrom;
        private DateTime mDateFrom;
        private TimeSpan mTimeTo;
        private DateTime mDateTo;

        public TodoCreationViewModel(ContentPage contentPage)
        {
            Todo = new Todo();

            DateFrom = DateTime.Now;
            DateTo = DateTime.Now;
            TimeFrom = DateTime.Now.TimeOfDay;
            TimeTo = DateTime.Now.TimeOfDay;

            OnSaveClick = new Command((nothing) =>
            {
                if (Todo == null || contentPage == null) return;
                Todo.From = DateFrom.Add(TimeFrom);
                Todo.To = DateTo.Add(TimeTo);

                if (Todo.From.Second < DateTime.Now.Second ||
                    Todo.To.Second < DateTime.Now.Second ||
                    Todo.From.Second > Todo.To.Second ||
                    String.IsNullOrEmpty(Todo.Title))
                {
                    contentPage.DisplayAlert("Warning", 
                                             "1.Please enter \"Title\"\n" +
                                             "2.Time To > Date From \n" +
                                             "3.Time > Time Now", "OK");
                    return;
                }

                App.Database.SaveItemAsync(Todo);
                contentPage.Navigation.PopAsync();
            });
        }

        public Todo Todo
        {
            set
            {
                mTodo = value;
                pushPropertyChanged("Todo");
            }
            get
            {
                return mTodo;
            }
        }

        public ICommand OnSaveClick
        {
            protected set; get;
        }

        public TimeSpan TimeFrom
        {
            set
            {
                mTimeFrom = value;
                pushPropertyChanged("TimeFrom");
            }
            get
            {
                return mTimeFrom;
            }
        }

        public TimeSpan TimeTo
        {
            set
            {
                mTimeTo = value;
                pushPropertyChanged("TimeTo");
            }
            get
            {
                return mTimeTo;
            }
        }

        public DateTime DateFrom
        {
            set
            {
                mDateFrom = value;
                pushPropertyChanged("DateFrom");
            }
            get
            {
                return mDateFrom;
            }
        }

        public DateTime DateTo
        {
            set
            {
                mDateTo = value;
                pushPropertyChanged("DateTo");
            }
            get
            {
                return mDateTo;
            }
        }
    }
}
