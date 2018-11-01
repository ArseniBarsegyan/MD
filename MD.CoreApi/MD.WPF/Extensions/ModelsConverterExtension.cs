using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MD.WPF.Models;
using MD.WPF.ViewModels;

namespace MD.WPF.Extensions
{
    public static class ModelsConverterExtension
    {
        public static PhotoViewModel ToPhotoViewModel(this Photo model)
        {
            return new PhotoViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Image = model.Image
            };
        }

        public static Photo ToPhotoModel(this PhotoViewModel viewModel)
        {
            return new Photo
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Image = viewModel.Image
            };
        }

        public static NoteViewModel ToNoteViewModel(this Note model)
        {
            return new NoteViewModel
            {
                Id = model.Id,
                Date = model.Date,
                Description = model.Description,
                Photos = new ObservableCollection<PhotoViewModel>(model.Photos.ToPhotoViewModels())
            };
        }

        public static Note ToNoteModel(this NoteViewModel viewModel)
        {
            return new Note
            {
                Id = viewModel.Id,
                Date = viewModel.Date,
                Description = viewModel.Description,
                Photos = new List<Photo>(viewModel.Photos.ToPhotoModels())
            };
        }

        public static IEnumerable<Photo> ToPhotoModels(this IEnumerable<PhotoViewModel> viewModels)
        {
            return viewModels.Select(viewModel => viewModel.ToPhotoModel()).ToList();
        }

        public static IEnumerable<PhotoViewModel> ToPhotoViewModels(this IEnumerable<Photo> models)
        {
            return models.Select(model => model.ToPhotoViewModel()).ToList();
        }

        public static IEnumerable<Note> ToNoteModels(this IEnumerable<NoteViewModel> viewModels)
        {
            return viewModels.Select(viewModel => viewModel.ToNoteModel()).ToList();
        }

        public static IEnumerable<NoteViewModel> ToNoteViewModels(this IEnumerable<Note> models)
        {
            return models.Select(viewModel => viewModel.ToNoteViewModel()).ToList();
        }
    }
}