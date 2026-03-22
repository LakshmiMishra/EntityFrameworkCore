using DbOperationsWithEFCore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DbOperationsWithEFCore.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
    }
}
