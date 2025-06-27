using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Core.DTOs.FacilityDTOs
{
    public class UpdateFacilityDTO
    {
        [Required(ErrorMessage = "Facility ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Facility ID must be a positive integer.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Facility name is required.")]
        public string Name { get; set; }
    }
}
