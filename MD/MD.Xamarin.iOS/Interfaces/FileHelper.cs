using System;
using System.IO;
using MD.Xamarin.iOS.Interfaces;
using MD.Xamarin.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace MD.Xamarin.iOS.Interfaces
{
    /// <inheritdoc />
    /// <summary>
    /// Implementation of <see cref="T:MD.Xamarin.Interfaces.IFileHelper" /> for iOS.
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