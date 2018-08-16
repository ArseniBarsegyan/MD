using Microsoft.EntityFrameworkCore;
using MyDiary.Data;

namespace MyDiary.Migrations.Context
{
    public class ApplicationDbContext : ApplicationContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
    }
}