using HotelReservation.Core.Enum;

namespace HotelReservation.Core.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public string TransactionId { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        // Navigation properties
        public Reservation Reservation { get; set; }
    }
}
