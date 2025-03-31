using Microsoft.EntityFrameworkCore;
using MoheymanProject.Models;

namespace MoheymanProject.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=.;Database=MoheymanProject;Integrated Security=True;Encrypt=False;");
        }
    }
}