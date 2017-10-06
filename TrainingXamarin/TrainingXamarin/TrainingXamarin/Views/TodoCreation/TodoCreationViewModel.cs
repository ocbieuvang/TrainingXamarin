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
        private ContentPage mContentPage;

        public TodoCreationViewModel(ContentPage contentPage)
        {
            Todo = new Todo();
            mContentPage = contentPage;
            DateFrom = DateTime.Now;
            DateTo = DateTime.Now;
            TimeFrom = DateTime.Now.TimeOfDay;
            TimeTo = DateTime.Now.TimeOfDay;

            OnSaveClick = new Command(onSaveClick);
        }

        public TodoCreationViewModel(ContentPage contentPage, object value)
		{
			Todo = (Todo)value;
            DateFrom = Todo.From;
            DateTo = Todo.To;
            TimeFrom = DateFrom.TimeOfDay;
			TimeTo = DateTo.TimeOfDay;
            mContentPage = contentPage;
			OnSaveClick = new Command(onSaveClick);
		}

        public void onSaveClick() {
			if (Todo == null || mContentPage == null) return;
			Todo.From = DateFrom.Add(TimeFrom);
			Todo.To = DateTo.Add(TimeTo);

			if (DateTime.Now.CompareTo(Todo.To) > 0 ||
				Todo.From.CompareTo(Todo.To) > 0 ||
				String.IsNullOrEmpty(Todo.Title))
			{
				mContentPage.DisplayAlert("Warning",
										 "1.Please enter \"Title\"\n" +
										 "2.Time To > Date From \n" +
										 "3.Time To > Time Now", "OK");
				return;
			}

			App.Database.SaveItemAsync(Todo);
			mContentPage.Navigation.PopAsync();
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
