namespace HotelReservation.Core.Entities
{
    public class RoomFacility
    {
        public int RoomId { get; set; }
        public int FacilityId { get; set; }

        // Navigation properties
        public Room Room { get; set; }
        public Facility Facility { get; set; }
    }
}
