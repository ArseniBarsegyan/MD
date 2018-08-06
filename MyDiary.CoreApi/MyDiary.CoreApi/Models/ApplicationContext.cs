using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyDiary.CoreApi.Models
{
    /// <summary>
    /// ApplicationContext class.
    /// </summary>
    public class ApplicationContext : IdentityDbContext
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationContext"/> class.
        /// </summary>
        /// <param name="dbContextOptions">The database context options.</param>
        public ApplicationContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}