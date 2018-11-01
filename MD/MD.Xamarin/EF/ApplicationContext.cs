using MD.Xamarin.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace MD.Xamarin.EF
{
    /// <inheritdoc />
    /// <summary>
    /// EF application context class.
    /// </summary>
    public class ApplicationContext : DbContext
    {
        // path to database.
        private readonly string _databasePath;

        public ApplicationContext(string databasePath)
        {
            _databasePath = databasePath;
            Database.EnsureCreated();
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<AppUser> Users { get; set; }

        // Set file name to injected database full name. Also create database if not exists.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
    }
}