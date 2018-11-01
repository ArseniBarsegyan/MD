using System;
using System.Linq;
using System.Threading.Tasks;
using MD.Xamarin.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace MD.Xamarin.Authentication
{
    /// <summary>
    /// Provide CRUD operations with users in database with help of EF.
    /// </summary>
    public class UserManager : IDisposable
    {
        private readonly DbContext _dbContext;
        private bool _disposed;

        public UserManager(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected DbSet<AppUser> DbSet => _dbContext.Set<AppUser>();

        /// <summary>
        /// Get all users from database.
        /// </summary>
        /// <returns></returns>
        public IQueryable<AppUser> GetAll()
        {
            return DbSet;
        }

        /// <summary>
        /// Get user by id from database.
        /// </summary>
        /// <param name="id">id of the user.</param>
        /// <returns></returns>
        public async Task<AppUser> GetByIdAsync(string id)
        {
            var user = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        /// <summary>
        /// Create user in database.
        /// </summary>
        /// <param name="user">user to be created.</param>
        /// <returns></returns>
        public async Task CreateAsync(AppUser user)
        {
            await DbSet.AddAsync(user);
        }

        /// <summary>
        /// Delete user from database by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AppUser> DeleteAsync(string id)
        {
            var user = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                DbSet.Remove(user);
            }
            return user;
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
        /// Update user in database.
        /// </summary>
        /// <param name="user">user to be updated.</param>
        public void Update(AppUser user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
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
    }
}