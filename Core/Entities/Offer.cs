namespace HotelReservation.Core.Entities
{
    public class Offer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Discount { get; set; } // Percentage or fixed amount
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        // Navigation properties
        public ICollection<Room> Rooms { get; set; }
    }
}
