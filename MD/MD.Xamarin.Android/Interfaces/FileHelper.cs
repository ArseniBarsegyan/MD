using Xamarin.Forms;
using MD.Xamarin.Droid.Interfaces;
using MD.Xamarin.Interfaces;
using System;
using System.IO;

[assembly: Dependency(typeof(FileHelper))]
namespace MD.Xamarin.Droid.Interfaces
{
    /// <inheritdoc />
    /// <summary>
    /// Implementation of <see cref="T:MD.Xamarin.Interfaces.IFileHelper" /> for Android.
    /// </summary>
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}