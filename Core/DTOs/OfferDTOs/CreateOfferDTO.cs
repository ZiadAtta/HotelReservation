using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Core.DTOs.OfferDTOs
{
    public class CreateOfferDTO
    {

        [Required(ErrorMessage = "At least one room Id is required")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Discount is required")]
        [Range(50, int.MaxValue, ErrorMessage = "Must be more than or equal 50")]
        public decimal Discount { get; set; }

        [Required(ErrorMessage = "Active status is required")]
        public bool IsActive { get; set; }
    }
}
