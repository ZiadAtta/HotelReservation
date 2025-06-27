namespace HotelReservation.Core.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public string Description { get; set; }
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; } = true;
        //public string ImageUrl { get; set; }
        public virtual List<Photo> Photos { get; set; }
        // Foreign keys
        public int RoomTypeId { get; set; }

        // Navigation properties
        public RoomType RoomType { get; set; }
        public ICollection<RoomFacility> RoomFacilities { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

        // the updates
       // public ICollection<Offer> Offers { get; set; }
       public Offer? Offer { get; set; }
    }
}
