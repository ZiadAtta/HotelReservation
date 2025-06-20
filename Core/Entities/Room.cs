using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Room : BaseEntity
    {
        [ForeignKey(nameof(HotelStaff))]
        public int HotelStaffId { get; set; }
        public HotelStaff? HotelStaff { get; set; }
        public string? Name { get; set; }
        public Decimal Price { get; set; }
        public int Capacity { get; set; }
        public string? Image { get; set; }
        public ICollection<RoomFacility>? RoomFacilities { get; set; }
        public Add? Add { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Favorite>? Favorites { get; set; }
        public ICollection<Rate>? Rates { get; set; }
    }
}
