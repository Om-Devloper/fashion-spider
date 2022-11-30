using bulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace bulkyBookWeb.Data
{
    public class crawlerDbContext : DbContext
    {
        public crawlerDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<AllData_Fields> productInfo { get; set; }
    }
}
