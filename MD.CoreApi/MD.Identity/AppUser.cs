using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MD.Identity
{
    /// <inheritdoc />
    /// <summary>
    /// User account
    /// </summary>
    public class AppUser : IdentityUser
    {
        public IEnumerable<Note> Notes { get; set; }
    }
}