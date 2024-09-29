using DemoCrudApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoCrudApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Demo> Demos { get; set; }


       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }


    }
}
