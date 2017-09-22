using System.ComponentModel;

namespace TrainingXamarin.Model
{
    public class Intro : INotifyPropertyChanged
    {
        public string Image
        {
            get; set;
        }

        public string Content
        {
            get; set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
