// File: Data/ApplicationDBContext.cs
using Microsoft.EntityFrameworkCore;
using Models;

namespace api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        // Define your DbSets here
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }

        internal async Task<bool> AnyAsync(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
