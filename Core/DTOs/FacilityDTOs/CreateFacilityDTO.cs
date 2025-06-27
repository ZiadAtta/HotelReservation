using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Core.DTOs.FacilityDTOs
{
    public class CreateFacilityDTO
    {
        [Required(ErrorMessage = "Facility name is required.")]
        public string? Name { get; set; }
    }
}
