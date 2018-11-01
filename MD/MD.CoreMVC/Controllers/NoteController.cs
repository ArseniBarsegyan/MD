using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MD.CoreMVC.Models;
using MD.Data;
using MD.Identity;

namespace MD.CoreMVC.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<Note> _repository;
        private IMapper _config;

        public NoteController(UserManager<AppUser> userManager, IRepository<Note> repository)
        {
            _userManager = userManager;
            _repository = repository;

            _config = new MapperConfiguration(m =>
            {
                m.CreateMap<PhotoViewModel, Photo>();
                m.CreateMap<Photo, PhotoViewModel>();

                m.CreateMap<CreateNoteViewModel, Note>()
                    .ForMember(x => x.Date, opt => opt.MapFrom(src => DateTime.Now))
                    .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(x => x.UserId, opt => opt.MapFrom(src => _userManager.GetUserId(User)))
                    .ForMember(x => x.Photos, opt => opt.Ignore());
                m.CreateMap<Note, NoteViewModel>()
                    .ForMember(x => x.Photos, opt => opt.MapFrom(src => src.Photos.ToList()));

                m.CreateMap<List<Photo>, List<PhotoViewModel>>();
                m.CreateMap<List<PhotoViewModel>, List<Photo>>();
                m.CreateMap<List<Note>, List<NoteViewModel>>();
                
            }).CreateMapper();
        }

        [Route("notes")]
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var notes = _repository.GetAll(userId).OrderByDescending(x => x.Id).ToList();
            
            var noteViewModels = new List<NoteViewModel>();

            foreach (var note in notes)
            {
                var noteViewModel = _config.Map<Note, NoteViewModel>(note);

                if (note.Photos.Any())
                {
                    var notePhotos = note.Photos.ToList();
                    var notePhotosViewModels = notePhotos.Select(notePhoto => _config.Map<Photo, PhotoViewModel>(notePhoto)).ToList();

                    noteViewModel.Photos = notePhotosViewModels;
                }                
                noteViewModels.Add(noteViewModel);
            }

            return View(noteViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateNoteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var note = _config.Map<CreateNoteViewModel, Note>(model);
            note = MapViewModelToNote(model, note);

            await _repository.CreateAsync(note);
            await _repository.SaveAsync();
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _repository.GetByIdAsync(id);
            var viewModel = _config.Map<Note, NoteViewModel>(model);

            if (model.Photos != null)
            {
                foreach (var photo in model.Photos)
                {
                    viewModel.Photos.Add(_config.Map<Photo, PhotoViewModel>(photo));
                }
            }            
            return PartialView(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
            return RedirectToAction("Index");
        }

        #region mapping
        private Note MapViewModelToNote(CreateNoteViewModel viewModel, Note note)
        {
            if (viewModel.Photos != null)
            {
                var photosContent = new List<string>();

                foreach (var formFile in viewModel.Photos)
                {
                    using (var ms = new MemoryStream())
                    {
                        formFile.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        photosContent.Add(Convert.ToBase64String(fileBytes));
                    }
                }

                var photos = new List<Photo>();

                for (int i = 0; i < viewModel.Photos.Count(); i++)
                {
                    var photo = new Photo { Name = viewModel.Photos.ElementAt(i).FileName, Image = photosContent.ElementAt(i), Note = note };
                    photos.Add(photo);
                }
                note.Photos = photos;
            }
            return note;
        }
        #endregion
    }
}