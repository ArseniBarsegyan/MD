using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MD.Helpers;

namespace MD.CoreMVC.Models
{
    public class CreateNoteViewModel
    {
        [Required(ErrorMessage = ConstantsHelper.NoteDescriptionRequired)]
        public string Description { get; set; }
        public IEnumerable<IFormFile> Photos { get; set; }
    }
}