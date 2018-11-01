using Microsoft.Win32;
using MD.WPF.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace MD.WPF.Pages
{
    /// <summary>
    /// Interaction logic for CreateNotePage.xaml
    /// </summary>
    public partial class CreateNotePage : Page
    {
        private string _image;
        private string _fileName;

        public CreateNotePage()
        {
            InitializeComponent();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void PickPhotoButton_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                DefaultExt = ".png",
                Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"
            };

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                string filePath = dlg.FileName;
                _fileName = System.IO.Path.GetFileName(filePath);
                PickPhotoButton.Content = _fileName;
                byte[] array = File.ReadAllBytes(filePath);
                _image = Convert.ToBase64String(array);
            }
        }

        private void CreateButton_OnClick(object sender, RoutedEventArgs e)
        {
            var photoModel = new PhotoViewModel
            {
                Name = _fileName,
                Image = _image
            };

            ViewModel.NoteViewModel = new NoteViewModel
            {
                Date = DateTime.Now,
                Description = DescriptionTextBox.Text,
                Photos = new ObservableCollection<PhotoViewModel> { photoModel }
            };

            ViewModel.CreateNoteCommand.Execute(null);
        }
    }
}
