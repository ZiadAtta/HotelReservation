using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Core.DTOs.RoomDTOs
{
    public class CreateRoomDTO
    {
        /// <summary>
        /// Act like a name in room table 
        /// </summary>
        [Required(ErrorMessage = "Room name is required. ")]
        public string? RoomNumber { get; set; }
        [Required(ErrorMessage = "Price is required. ")]
        [Range(0, int.MaxValue,ErrorMessage = "The price must be positive. ")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Room type is required. ")]
        [Range(1,10, ErrorMessage = "Room type must be between 1 and 10. ")]
        public int Capacity { get; set; }   
        public decimal Discount { get; set; }
        public List<int>? Facilities { get; set; }
        [Required(ErrorMessage = "Image is required. ")]
        public string? Image { get; set; }
    }
}
