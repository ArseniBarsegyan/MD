using System;
using System.Collections.Generic;
using MyDiary.Data;

namespace MyDiary.Identity
{
    public class Note : Entity
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<Photo> Photos { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}