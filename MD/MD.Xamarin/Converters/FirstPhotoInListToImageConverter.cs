using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using MD.Xamarin.ViewModels;
using Xamarin.Forms;
using System.Linq;

namespace MD.Xamarin.Converters
{
    public class FirstPhotoInListToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ImageSource retSource = null;
            if (value is IEnumerable<PhotoViewModel> photoViewModels)
            {
                if (photoViewModels.Any())
                {
                    byte[] imageAsBytes = System.Convert.FromBase64String(photoViewModels.ElementAt(0).Image);
                    retSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
                }
                return retSource;
            }
            return retSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}