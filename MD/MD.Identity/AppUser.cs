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
        public byte[] ImageContent { get; set; }
        public IEnumerable<Note> Notes { get; set; }
    }
}