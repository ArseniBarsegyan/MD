using Microsoft.EntityFrameworkCore;

namespace MyDiary.Migrations.Context
{
    /// <inheritdoc />
    /// <summary>
    /// BaseContext.
    /// </summary>
    /// <seealso cref="DbContext" />
    public class BaseContext : DbContext
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseContext" /> class.
        /// </summary>
        /// <param name="dbContextOptions">The database context options.</param>
        public BaseContext(DbContextOptions<BaseContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }
    }
}