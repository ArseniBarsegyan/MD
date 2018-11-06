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
    }
}
