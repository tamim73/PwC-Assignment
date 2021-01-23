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


            modelBuilder.Entity<Story>()
                .HasOne(x => x.Topic)
                .WithOne(x=> x.TopicForStory)
                .HasForeignKey<Story>(x => x.TopicId);

            modelBuilder.Entity<Post>()
               .HasOne<Story>(x => x.PostForStory)
               .WithMany(x => x.Posts)
               .HasForeignKey(x => x.PostForStoryId);

            #region Seeding
            // admin user
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = 1,
                    Name = "System Administrator",
                    Username = "admin",
                    Password = CryptoHelper.HashPassword("p@ssw0rd"),
                    Role = UserRole.Admin
                },
                new ApplicationUser
                {
                    Id = 2,
                    Name = "Mr.Writer",
                    Username = "writer",
                    Password = CryptoHelper.HashPassword("p@ssw0rd"),
                    Role = UserRole.Writer
                }
                );

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
