using System;
using System.Collections.Generic;

namespace MyDiary.CoreApi.Models
{
    /// <summary>
    /// Notes table in database.
    /// </summary>
    public class Note : Entity
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
    }
}