using MessageService.Models;
using Microsoft.EntityFrameworkCore;

namespace MessageService.DB
{
    // dotnet ef migrations add InitialCreate
    // dotnet ef database update
    public partial class MessageContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        //public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }

        private string _connectionString;
        public MessageContext()
        {
        }

        public MessageContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                //entity.HasKey(p => p.Id).HasName("users_pkey");
                //entity.HasIndex(e => e.Email).IsUnique();
                entity.ToTable("users");
                entity.Property(p => p.Id).HasColumnName("id");
                //entity.Property(p => p.Email)
                //      .HasMaxLength(255)
                //      .HasColumnName("email");
                //entity.Property(p => p.Password).HasColumnName("password");
                //entity.Property(d => d.Salt).HasColumnName("salt");
                //entity.Property(e => e.RoleId).HasConversion<int>();
            });
            //modelBuilder.Entity<Role>().Property(e => e.RoleId).HasConversion<int>();
            //modelBuilder.Entity<Role>().HasData(
            //    Enum.GetValues(typeof(RoleId))
            //    .Cast<RoleId>()
            //    .Select(e => new Role()
            //    {
            //        RoleId = e,
            //        Name = e.ToString()
            //    }));


            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(p => p.Id).HasName("message_pkey");
                entity.HasIndex(e => e.Id).IsUnique();
                entity.ToTable("messages");

                entity.Property(p => p.Id).HasColumnName("id");
                entity.Property(p => p.Text).HasColumnName("text");
                entity.Property(d => d.FromUser).HasColumnName("fromuser");
                entity.Property(e => e.ToUser).HasColumnName("touser");
                entity.Property(e => e.StatusId).HasConversion<int>(); 
            });

            modelBuilder.Entity<Status>().Property(e => e.StatusId).HasConversion<int>();
            modelBuilder.Entity<Status>().HasData(
                Enum.GetValues(typeof(StatusId))
                .Cast<StatusId>()
                .Select(e => new Status()
                {
                    StatusId = e,
                    Message = e.ToString()
                }));

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
