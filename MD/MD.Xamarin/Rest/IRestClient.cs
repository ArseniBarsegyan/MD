using System.Collections.Generic;
using System.Threading.Tasks;
using MD.Xamarin.Models;

namespace MD.Xamarin.Rest
{
    /// <summary>
    /// Implementation of the REST client for MD.Notes
    /// </summary>
    public interface IRestClient
    {
        /// <summary>
        /// Get all notes from API.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<NoteModel>> GetNotes();
    }
}