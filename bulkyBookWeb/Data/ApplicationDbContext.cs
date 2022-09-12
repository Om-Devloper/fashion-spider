using Microsoft.EntityFrameworkCore;
using bulkyBookWeb.Models;

namespace bulkyBookWeb.Data;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }
        public DbSet<Catagory> Catagories { get; set; }
    }

