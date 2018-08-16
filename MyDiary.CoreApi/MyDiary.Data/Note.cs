using System;
using System.Collections.Generic;

namespace MyDiary.Data
{
    public class Note : Entity
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
    }
}