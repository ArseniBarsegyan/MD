using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MD.Data;
using MD.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MD.XamarinTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IRepository<Note> _repository;

        public ValuesController(IRepository<Note> repository)
        {
            _repository = repository;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Note>> Get()
        {
            var allNotes = _repository.GetAll("05f9947f-6e27-44fc-a7d7-16c2f9189331").OrderByDescending(x => x.Id).ToList();
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

                note.UserId = "05f9947f-6e27-44fc-a7d7-16c2f9189331";
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
