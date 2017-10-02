using System;
using System.ComponentModel;

namespace TrainingXamarin.Model
{
	public class BaseModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void pushPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this,
				new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
