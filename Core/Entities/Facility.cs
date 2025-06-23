namespace HotelReservation.Core.Entities
{
    public class Facility
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RoomFacility> RoomFacilities { get; set; }
    }
}
