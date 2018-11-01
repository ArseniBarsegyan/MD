using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MD.Xamarin.EF.Models;
using MD.Xamarin.ViewModels;

namespace MD.Xamarin.Extensions
{
    public static class ModelsConverterExtension
    {
        // ----------------------------------------
        // -------- Photo models converters --------
        // ----------------------------------------
        public static Photo ToPhotoModel(this PhotoViewModel viewModel)
        {
            var model = new Photo
            {
                Id = viewModel.Id,
                Image = viewModel.Image,
                Name = viewModel.Name,
                NoteId = viewModel.NoteId
            };
            return model;
        }

        public static PhotoViewModel ToPhotoViewModel(this Photo model)
        {
            var viewModel = new PhotoViewModel
            {
                Id = model.Id,
                Image = model.Image,
                Name = model.Name,
                NoteId = model.NoteId
            };
            return viewModel;
        }

        public static IEnumerable<Photo> ToPhotoModels(this IEnumerable<PhotoViewModel> viewModels)
        {
            return viewModels.Select(viewModel => viewModel.ToPhotoModel()).ToList();
        }

        public static IEnumerable<PhotoViewModel> ToPhotoViewModels(this IEnumerable<Photo> models)
        {
            return models.Select(model => model.ToPhotoViewModel()).ToList();
        }

        // ----------------------------------------
        // -------- Note models converters --------
        // ----------------------------------------
        public static Note ToNoteModel(this NoteViewModel viewModel)
        {
            var model = new Note
            {
                Id = viewModel.Id,
                Date = viewModel.Date,
                Description = viewModel.Description,
                UserId = viewModel.UserId,
                Photos = viewModel.Photos.ToPhotoModels()
            };
            return model;
        }

        public static NoteViewModel ToNoteViewModel(this Note model)
        {
            var viewModel = new NoteViewModel
            {
                Id = model.Id,
                Date = model.Date,
                Description = model.Description,
                UserId = model.UserId,
                Photos = new ObservableCollection<PhotoViewModel>(model.Photos.ToPhotoViewModels())
            };
            return viewModel;
        }

        public static IEnumerable<Note> ToNoteModels(this IEnumerable<NoteViewModel> viewModels)
        {
            return viewModels.Select(viewModel => viewModel.ToNoteModel()).ToList();
        }

        public static IEnumerable<NoteViewModel> ToNoteViewModels(this IEnumerable<Note> models)
        {
            return models.Select(model => model.ToNoteViewModel()).ToList();
        }
    }
}