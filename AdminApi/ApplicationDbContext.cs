using AdminApi.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AdminApi.Attributes;

namespace AdminApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            try
            {
                Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database migration failed: {ex.Message}");
            }
        }

        public DbSet<AdminModel> admins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(GlobalAttributes.mySQLConfig.connectionString, ServerVersion.AutoDetect(GlobalAttributes.mySQLConfig.connectionString));
            }
        }
    }
}
