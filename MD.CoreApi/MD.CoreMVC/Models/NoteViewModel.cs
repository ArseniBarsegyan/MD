using System;
using System.Collections.Generic;

namespace MD.CoreMVC.Models
{
    public class NoteViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public List<PhotoViewModel> Photos { get; set; }
    }
}
