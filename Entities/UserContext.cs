using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BackendApi.Entities
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }

        public UserContext(DbContextOptions<UserContext> option) : base(option) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .Property(u => u.Created)
            .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Task>()
                .HasOne<User>(u => u.User)
                .WithMany(t => t.Tasks)
                .HasForeignKey(u => u.UserID)
                .IsRequired(false);

            modelBuilder.Entity<UserProject>()
            .HasKey(up => new { up.ProjectId, up.UserId });
            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.Project)
                .WithMany(p => p.ProjectsAndUsers)
                .HasForeignKey(up => up.ProjectId);
            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.User)
                .WithMany(u => u.ProjectsAndUsers)
                .HasForeignKey(up => up.UserId);

            // this will singularize all table names
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.Relational().TableName = entityType.DisplayName();
            }
        }
    }
}

