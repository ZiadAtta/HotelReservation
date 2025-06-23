using HotelReservation.Core.Enum;

namespace HotelReservation.Core.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal TotalPrice { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign keys
        public string CustomerId { get; set; }
        public int RoomId { get; set; }
        public int? OfferId { get; set; }
        public int? PaymentId { get; set; }

        // Navigation properties
        public ApplicationUser Customer { get; set; }
        public Room Room { get; set; }
        public Offer Offer { get; set; }
        public Payment Payment { get; set; }
    }
}
