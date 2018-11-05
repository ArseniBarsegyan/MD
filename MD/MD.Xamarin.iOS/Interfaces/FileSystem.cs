
using System.IO;
using MD.Xamarin.iOS.Interfaces;
using MD.Xamarin.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileSystem))]
namespace MD.Xamarin.iOS.Interfaces
{
    /// <summary>
    /// Implementation of <see cref="IFileSystem"/> for iOS.
    /// </summary>
    public class FileSystem : IFileSystem
    {
        public byte[] ReadAllBytes(string path)
        {
            return File.ReadAllBytes(path);
        }
    }
}