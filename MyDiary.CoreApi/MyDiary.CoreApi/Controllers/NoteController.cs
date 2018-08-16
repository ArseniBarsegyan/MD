using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyDiary.Data;

namespace MyDiary.CoreApi.Controllers
{
    [Produces("application/json")]
    [Route("api/notes")]
    public class NoteController : Controller
    {
        private readonly IRepository<Note> _repository;

        public NoteController(IRepository<Note> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<Note> Get()
        {
            var allNotes = _repository.GetAll().ToList();
            foreach (var note in allNotes)
            {
                foreach (var photo in note.Photos)
                {
                    photo.Note = null;
                }
            }
            return allNotes;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var note = await _repository.GetByIdAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            foreach (var photo in note.Photos)
            {
                photo.Note = null;
            }
            return Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Note note)
        {
            if (ModelState.IsValid)
            {
                foreach (var photo in note.Photos)
                {
                    photo.Note = note;
                }

                await _repository.CreateAsync(note);
                await _repository.SaveAsync();

                foreach (var photo in note.Photos)
                {
                    photo.Note = null;
                }
                return Ok(note);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Note note)
        {
            if (note == null)
            {
                return BadRequest();
            }
            _repository.Update(note);
            await _repository.SaveAsync();
            return Ok(note);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            await _repository.SaveAsync();
            return Ok(result);
        }
    }
}