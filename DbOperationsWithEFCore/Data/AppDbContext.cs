using Microsoft.EntityFrameworkCore;

namespace DbOperationsWithEFCore.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
    }
}
