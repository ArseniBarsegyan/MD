using Microsoft.EntityFrameworkCore;
using MD.Identity;

namespace MD.Migrations.Context
{
    public class IdentityContext : AppIdentityDbContext
    {
        public IdentityContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions, Configuration.IdentitySchemaName)
        {
        }
    }
}