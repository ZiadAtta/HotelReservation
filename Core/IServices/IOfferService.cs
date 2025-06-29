using HotelReservation.Core.DTOs.OfferDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Core.IServices
{
    public interface IOfferService  
    {
        public Task<bool> CreateOffer(CreateOfferDTO createFacilityDTO);
        public Task<bool> UpdateFacility(UpdateOfferDTO updateOfferrDTO);
        public Task<bool> DeleteFacility(DeleteOfferDTO updateFacilityDTO);
    }
}
