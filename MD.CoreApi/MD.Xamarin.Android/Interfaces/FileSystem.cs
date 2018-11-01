using System;
using System.IO;
using MD.Xamarin.Droid.Interfaces;
using MD.Xamarin.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileSystem))]
namespace MD.Xamarin.Droid.Interfaces
{
    /// <inheritdoc />
    /// <summary>
    /// Implementation of <see cref="IFileSystem"/> for Android.
    /// </summary>
    public class FileSystem : IFileSystem
    {
        public byte[] ReadAllBytes(string path)
        {
            try
            {
                return File.ReadAllBytes(path);
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                return new byte[0];
            }
        }
    }
}