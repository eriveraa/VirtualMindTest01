using Microsoft.EntityFrameworkCore;
using EMT.Common.Entities;

namespace EMT.DAL.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Purchase> Purchase { get; set; }
    }
}
