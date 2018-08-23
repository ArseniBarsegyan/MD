using Microsoft.EntityFrameworkCore;
using MyDiary.Identity;

namespace MyDiary.Migrations.Context
{
    public class IdentityContext : AppIdentityDbContext
    {
        public IdentityContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions, Configuration.IdentitySchemaName)
        {
        }
    }
}