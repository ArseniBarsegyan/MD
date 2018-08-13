using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MyDiary.Identity
{
    public class AppUser : IdentityUser
    {
        public IEnumerable<Note> Notes { get; set; }
    }
}