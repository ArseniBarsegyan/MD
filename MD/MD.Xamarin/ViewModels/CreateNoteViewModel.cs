using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MD.Xamarin.EF.Models;
using MD.Xamarin.Extensions;
using MD.Xamarin.Helpers;
using MD.Xamarin.Interfaces;
using MD.Xamarin.Interfaces.FilePickerService;
using Plugin.Multilingual;
using Xamarin.Forms;

namespace MD.Xamarin.ViewModels
{
    public class CreateNoteViewModel : BaseViewModel
    {
        private readonly IAlertService _alertService;

        public CreateNoteViewModel()
        {
            Photos = new ObservableCollection<PhotoViewModel>();
            _alertService = DependencyService.Get<IAlertService>();
            CreateNoteCommand = new Command<Task>(async task => await CreateNoteCommandExecute());
            PickPhotoCommand = new Command<PlatformDocument>(async document => await PickPhotoCommandExecute(document));
        }

        public bool IsLoading { get; set; }
        public string Description { get; set; }
        public ObservableCollection<PhotoViewModel> Photos { get; set; }
        public ICommand CreateNoteCommand { get; set; }
        public ICommand PickPhotoCommand { get; set; }

        /// <summary>
        /// Invokes when Photos collection changing.
        /// </summary>
        public event EventHandler PhotosCollectionChanged;

        private async Task CreateNoteCommandExecute()
        {
            var ci = CrossMultilingual.Current.CurrentCultureInfo;

            if (string.IsNullOrWhiteSpace(Description))
            {
                var descriptionEmptyMessageLocalized =
                    Resmgr.Value.GetString(ConstantsHelper.DescriptionEmptyMessage, ci);
                _alertService.ShowOkAlert(descriptionEmptyMessageLocalized, ConstantsHelper.Ok);
                return;
            }
            var noteModel = new Note
            {
                UserId = Settings.CurrentUserId,
                Description = Description,
                Date = DateTime.Now
            };
            var photosModels = Photos.ToPhotoModels();
            foreach (var photoModel in photosModels)
            {
                photoModel.Note = noteModel;
            }
            noteModel.Photos = photosModels;
            
            await App.NoteRepository.CreateAsync(noteModel);
            await App.NoteRepository.SaveAsync();
            MessagingCenter.Send(this, ConstantsHelper.ShouldUpdateUI);
        }

        private async Task PickPhotoCommandExecute(PlatformDocument document)
        {
            IsLoading = true;
            var ci = CrossMultilingual.Current.CurrentCultureInfo;

            if (document.Name.EndsWith(".png") || document.Name.EndsWith(".jpg"))
            {
                var mediaService = DependencyService.Get<IMediaService>();
                var fileSystem = DependencyService.Get<IFileSystem>();
                var imageContent = fileSystem.ReadAllBytes(document.Path);

                var resizedImage = mediaService.ResizeImage(imageContent, ConstantsHelper.ResizedImageWidth,
                    ConstantsHelper.ResizedImageHeight);
                var photoModel = new Photo
                {
                    Name = document.Name,
                    Image = Convert.ToBase64String(resizedImage)
                };

                Photos.Add(photoModel.ToPhotoViewModel());
                PhotosCollectionChanged?.Invoke(this, EventArgs.Empty);
                IsLoading = false;
            }
            else
            {
                IsLoading = false;
                var imagePickErrorMesssageLocalized =
                    Resmgr.Value.GetString(ConstantsHelper.ImagePickErrorMesssage, ci);
                var okLocalized = Resmgr.Value.GetString(ConstantsHelper.Ok, ci);

                _alertService.ShowOkAlert(imagePickErrorMesssageLocalized, okLocalized);
            }
        }
    }
}