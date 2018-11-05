namespace MD.Xamarin.EF.Models
{
    /// <inheritdoc />
    /// <summary>
    /// Photo entity.
    /// </summary>
    public class Photo : Entity
    {
        /// <summary>
        /// Photo file name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id of the note to which photo is belong.
        /// </summary>
        public int NoteId { get; set; }

        public Note Note { get; set; }

        /// <summary>
        /// Photo content as base64 string.
        /// </summary>
        public string Image { get; set; }
    }
}