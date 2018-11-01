using System;
using System.Collections.Generic;

namespace MD.Xamarin.EF.Models
{
    /// <inheritdoc />
    /// <summary>
    /// Note entity.
    /// </summary>
    public class Note : Entity
    {
        /// <summary>
        /// Note text.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Creation date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Collection of photos.
        /// </summary>
        public IEnumerable<Photo> Photos { get; set; }

        /// <summary>
        /// Id of the user to which note is belong.
        /// </summary>
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}