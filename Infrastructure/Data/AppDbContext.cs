using Core.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<HotelStaff> HotelStaffs { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomFacility> RoomFacilities { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Add> Adds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // for appaying TPT (Table Per Type) inheritance strategy
            modelBuilder.Entity<HotelStaff>().ToTable("HotelStaffs");

            modelBuilder.Entity<Room>()
                .HasOne(r => r.Add)
                .WithOne(a => a.Room)
                .HasForeignKey<Add>(a => a.RoomId);

            modelBuilder.Entity<HotelStaff>()
                .HasMany(h => h.Rooms)
                .WithOne(r => r.HotelStaff)
                .HasForeignKey(r => r.HotelStaffId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Room)
                .WithMany(r => r.Comments)
                .HasForeignKey(c => c.RoomId);

            modelBuilder.Entity<Rate>()
                .HasOne(r => r.User)
                .WithMany(u => u.Rates)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Rate>()
                .HasOne(r => r.Room)
                .WithMany(rm => rm.Rates)
                .HasForeignKey(r => r.RoomId);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Room)
                .WithMany(r => r.Favorites)
                .HasForeignKey(f => f.RoomId);

            modelBuilder.Entity<Favorite>()
                .HasIndex(f => new { f.UserId, f.RoomId })
                .IsUnique();

            modelBuilder.Entity<Rate>()
                .HasIndex(r => new { r.UserId, r.RoomId })
                .IsUnique();

            modelBuilder.Entity<Comment>()
               .HasIndex(c => new { c.UserId, c.RoomId })
               .IsUnique(false);

            modelBuilder.Entity<Room>()
              .Property(r => r.Price)
              .HasPrecision(18, 2);

            modelBuilder.Entity<Add>()
                .Property(a => a.Discount)
                .HasPrecision(18, 2);
        }
    }
}
