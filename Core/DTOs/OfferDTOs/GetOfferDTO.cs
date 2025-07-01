using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Core.DTOs.OfferDTOs
{
    public class GetOfferDTO
    {
        public string? Name { get; set; }
        public decimal Discount { get; set; }
        public decimal Price { get; set; }
        public int capacity { get; set; }
        public bool IsActive { get; set; }
        public int OfferId { get; set; }
        public int RoomId { get; set; }
    }
}
