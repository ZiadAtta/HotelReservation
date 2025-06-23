using Core.Enum;
using Microsoft.AspNetCore.Identity;

namespace HotelReservation.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        /* public string FirstName { get; set; }
         public string LastName { get; set; }*/
        public string Name { get; set; } = string.Empty;
        public UserType UserType { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
    }
}
