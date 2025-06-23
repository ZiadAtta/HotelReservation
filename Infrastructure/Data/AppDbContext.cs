using HotelReservation.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<RoomFacility> RoomFacilities { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }

        public DbSet<TokenInfo> TokenInfos { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RoomFacility>()
             .HasKey(rf => new { rf.RoomId, rf.FacilityId });

            builder.Entity<Room>()
                .HasMany(r => r.Offers)
                .WithMany(o => o.Rooms)
                .UsingEntity(j => j.ToTable("RoomOffers"));
        }
    }
}
