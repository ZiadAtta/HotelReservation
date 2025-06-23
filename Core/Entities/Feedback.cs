namespace HotelReservation.Core.Entities
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; } // 1-5 scale
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsApproved { get; set; } = false;

        // Foreign keys
        public string CustomerId { get; set; }
        public int? ReservationId { get; set; }

        // Navigation properties
        public ApplicationUser Customer { get; set; }
        public Reservation Reservation { get; set; }
    }
}
