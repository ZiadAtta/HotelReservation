using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Core.DTOs.FacilityDTOs
{
    public class DeleteFacilityDTO
    {
        [Required(ErrorMessage = "Facility ID is required.")]
        public int Id { get; set; }
    }
}
