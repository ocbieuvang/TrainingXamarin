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

            OnSaveClick = new Command((nothing) =>
            {
                if (Todo == null || contentPage == null) return;
                Todo.From = DateFrom.Add(TimeFrom);
                Todo.To = DateTo.Add(TimeTo);
                App.Database.SaveItemAsync(Todo);
                //var stack = contentPage.Navigation.NavigationStack;
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
