namespace HotelReservation.Core.Entities
{
    public class RoomType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation properties
        public ICollection<Room> Rooms { get; set; }
    }
}
