using System;
using System.IO;
using TrainingXamarin.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace TrainingXamarin.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}
