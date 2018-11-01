using System;
using System.Linq;
using System.Threading.Tasks;
using MD.Xamarin.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace MD.Xamarin.EF
{
    /// <summary>
    /// Implementation of <see cref="IRepository{TEntity}" />.
    /// </summary>
    public class NoteRepository : IDisposable, IRepository<Note>
    {
        private readonly DbContext _dbContext;
        private bool _disposed;

        public NoteRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected DbSet<Note> DbSet => _dbContext.Set<Note>();

        /// <summary>
        /// Return all notes by user id.
        /// </summary>
        /// <param name="userId">Id of the user.</param>
        /// <returns></returns>
        public IQueryable<Note> GetAll(string userId)
        {
            return DbSet.Where(x => x.UserId == userId).Include(x => x.Photos);
        }

        /// <summary>
        /// Return note by its id.
        /// </summary>
        /// <param name="id">Id of the note.</param>
        /// <returns></returns>
        public async Task<Note> GetByIdAsync(int? id)
        {
            var note = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (note != null)
            {
                var photos = await _dbContext.Entry(note).Collection(x => x.Photos).Query().ToListAsync();
                note.Photos = photos;
            }
            return note;
        }

        /// <summary>
        /// Create new note in database.
        /// </summary>
        /// <param name="item">note to saved to database.</param>
        /// <returns></returns>
        public async Task CreateAsync(Note item)
        {
            await DbSet.AddAsync(item);
        }

        /// <summary>
        /// Delete note from database by id.
        /// </summary>
        /// <param name="id">id of the note to be deleted.</param>
        /// <returns></returns>
        public async Task<Note> DeleteAsync(int? id)
        {
            var item = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (item != null)
            {
                DbSet.Remove(item);
            }
            return item;
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Save changes.
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Update note in database.
        /// </summary>
        /// <param name="item">note to be updated.</param>
        public async Task Update(Note item)
        {
            await DeleteAsync(item.Id);
            _dbContext.Add(item);
            // _dbContext.Entry(item).State = EntityState.Modified;
        }
    }
}