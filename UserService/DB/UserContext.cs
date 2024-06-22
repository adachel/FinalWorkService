using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;
using UserService.Models;

namespace UserService.DB
{
    // dotnet ef migrations add InitialCreate
    // dotnet ef database update
    public partial class UserContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        private string _connectionString; 

        public UserContext()
        {
        }

        public UserContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseNpgsql(_connectionString);    //
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(p => p.Id).HasName("users_pkey");
                entity.HasIndex(e => e.Email).IsUnique();
                entity.ToTable("users");
                entity.Property(p => p.Id).HasColumnName("id");
                entity.Property(p => p.Email)
                      .HasMaxLength(255)
                      .HasColumnName("email");
                entity.Property(p => p.Password).HasColumnName("password");
                entity.Property(d => d.Salt).HasColumnName("salt");
                entity.Property(e => e.RoleId).HasConversion<int>();
            });
            modelBuilder.Entity<Role>().Property(e => e.RoleId).HasConversion<int>();
            modelBuilder.Entity<Role>().HasData(
                Enum.GetValues(typeof(RoleId))
                .Cast<RoleId>()
                .Select(e => new Role()
                {
                    RoleId = e,
                    Name = e.ToString()
                }));

            base.OnModelCreating(modelBuilder);

            //OnModelCreatingPartial(modelBuilder);
        }
        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
