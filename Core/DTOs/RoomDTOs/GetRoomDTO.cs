using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Core.DTOs.RoomDTOs
{
    public class GetRoomDTO
    {
        public int Id { get; set; }
        public string? RoomNumber { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        /// <summary>
        /// the capacity of the room
        /// </summary>
        public int Tag { get; set; }
        public List<string>? Facilities { get; set; }

    }
}
