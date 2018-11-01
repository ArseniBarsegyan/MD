using System.Collections.Generic;

namespace MD.Xamarin.EF.Models
{
    /// <summary>
    /// Application user.
    /// </summary>
    public class AppUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }

        /// <summary>
        /// Collection of notes.
        /// </summary>
        public IEnumerable<Note> Notes { get; set; }
    }
}