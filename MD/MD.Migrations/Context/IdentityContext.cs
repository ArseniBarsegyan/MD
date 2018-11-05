using Microsoft.EntityFrameworkCore;
using MD.Identity;
using MD.Helpers;

namespace MD.Migrations.Context
{
    public class IdentityContext : AppIdentityDbContext
    {
        public IdentityContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions, ConstantsHelper.ContextShemaName)
        {
        }
    }
}