using System.ComponentModel.DataAnnotations.Schema;

namespace HotelReservation.Core.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public int RoomId { get; set; }
        [ForeignKey(nameof(RoomId))]
        public virtual Room Room { get; set; }
    }
}
