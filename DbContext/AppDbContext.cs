using Data.Models;
using DataAccess.Helpers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<TaskModel> Tasks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(u => u.CreatedTasks)
                    .WithOne(t => t.Author)
                    .HasForeignKey(t => t.AuthorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(u => u.AssignedTasks)
                    .WithOne(t => t.Performer)
                    .HasForeignKey(t => t.PerformerId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<TaskModel>(entity =>
            {
                entity.HasOne(u => u.ParentTask)
                      .WithMany(u => u.Subtasks)
                      .OnDelete(DeleteBehavior.Cascade);
            });


            var users = new[]
            {
                new User
                {
                    Id = 1,
                    Login = "admin",
                    PasswordHash = CryptographyHelper.HashPassword("admin")
                },
                new User
                {
                    Id = 2,
                    Login = "ivan",
                    PasswordHash = CryptographyHelper.HashPassword("ivan")
                },
                new User
                {
                    Id = 3,
                    Login = "petr",
                    PasswordHash = CryptographyHelper.HashPassword("petr")
                }
            };

            modelBuilder.Entity<User>().HasData(users);
        }
    }
}
