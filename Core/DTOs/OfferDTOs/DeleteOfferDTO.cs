using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Core.DTOs.OfferDTOs
{
    public class DeleteOfferDTO
    {
        [Required(ErrorMessage = "Offer Id is required")]
        public int OfferId { get; set; }
    }
}
