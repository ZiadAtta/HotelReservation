using HotelReservation.Core.DTOs.FacilityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Core.IServices
{
    public interface IFacilityService
    {
        public Task<bool> CreateFacility(CreateFacilityDTO createFacilityDTO);
        public Task<bool> UpdateFacility(UpdateFacilityDTO updateFacilityDTO);
        public Task<bool> DeleteFacility(DeleteFacilityDTO id);
    }
}
