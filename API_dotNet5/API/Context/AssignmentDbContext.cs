using Lib.Entites;
using Lib.Enums;
using Lib.Helpers;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.Context
{
    public class AssignmentDbContext : DbContext
    {

        public AssignmentDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Assignment");

            #region Seeding
            // admin user
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = 1,
                    Name = "System Administrator",
                    Username = "sysadmin",
                    Password = CryptoHelper.HashPassword("p@ssw0rd"),
                    Role = UserRole.Admin
                });

            // Demo 
            #endregion

        }

        #region DbSets
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Story> Stories { get; set; }

        #endregion
    }
}
