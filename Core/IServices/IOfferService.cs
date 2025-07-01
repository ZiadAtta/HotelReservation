using HotelReservation.Core.DTOs.OfferDTOs;
using HotelReservation.Core.DTOs.RoomDTOs;
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
        public Task<List<GetOfferDTO>> GetOffersByRoomId(RoomPaginationDTO model);
        public Task<List<GetOfferDTO>> SearchByName(SearchRoomDTO model);
    }
}
