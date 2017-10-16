using System;
using System.Windows.Input;
using Plugin.CrossPlacePicker;
using TrainingXamarin.Model;
using TrainingXamarin.Views.MapPage;
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

            DateFrom = DateTime.Now;
            DateTo = DateTime.Now;
            TimeFrom = DateTime.Now.TimeOfDay;
            TimeTo = DateTime.Now.TimeOfDay;

            InitCommon(contentPage);
        }

        public TodoCreationViewModel(ContentPage contentPage, object value)
        {
            Todo = (Todo)value;
            DateFrom = Todo.From;
            DateTo = Todo.To;
            TimeFrom = DateFrom.TimeOfDay;
            TimeTo = DateTo.TimeOfDay;

            InitCommon(contentPage);
        }


        public void InitCommon(ContentPage contentPage)
        {
            mContentPage = contentPage;
            OnSaveClick = new Command(onSaveClick);
            OnPickLocationClick = new Command(onPickLocationClick);

            MessagingCenter.Subscribe<string>(this, "TODO", (location) =>
            {
                if (location != null) Todo.Location = location;
            });
        }


        public async void onPickLocationClick()
        {
            try
            {
                var result = await CrossPlacePicker.Current.Display();
                if (result != null)
                {
                    Todo.Location = result.Name;
                }
            }
            catch (Exception ex)
            {
                await mContentPage.DisplayAlert("Error", ex.ToString(), "Oops");
            }
        }

        public void onSaveClick()
        {
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
            if (Todo.IsDone == true)
            {
                DependencyService.Get<IAlarm>().CancelAlarm(Todo);
            }
            else
            {
                DependencyService.Get<IAlarm>().SetAlarm(Todo);
            }
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

        public ICommand OnPickLocationClick
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
