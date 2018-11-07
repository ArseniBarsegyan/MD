using System.Collections.Generic;
using System.Threading.Tasks;
using MD.Xamarin.Models;

namespace MD.Xamarin.Rest
{
    /// <summary>
    /// Implementation of the REST client for MD.Notes
    /// </summary>
    public interface INotesServiceClient
    {
        /// <summary>
        /// Get all notes from API.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<NoteModel>> GetNotes();

        /// <summary>
        /// Get note by id from API.
        /// </summary>
        /// <param name="id">id of the note to be retrieved.</param>
        /// <returns></returns>
        Task<NoteModel> Get(int id);

        /// <summary>
        /// Create new note.
        /// </summary>
        /// <param name="note">note to be created.</param>
        /// <returns></returns>
        Task<bool> Create(NoteModel note);

        /// <summary>
        /// Update old note with new.
        /// </summary>
        /// <param name="id">id of the note to be updated.</param>
        /// <param name="noteModel">new note.</param>
        /// <returns></returns>
        Task<bool> UpdateNote(int id, NoteModel noteModel);

        /// <summary>
        /// Delete note.
        /// </summary>
        /// <param name="id">id of the note to be deleted.</param>
        /// <returns></returns>
        Task<bool> DeleteNote(int id);
    }
}