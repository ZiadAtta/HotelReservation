using HotelReservation.Core.DTOs.RoomDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Core.IServices
{
    public interface IRoomService
    {
        Task<bool> CreateRoomAsync(CreateRoomDTO createRoom);
        Task<List<GetRoomDTO>> GetAllRoomsAsync(RoomPaginationDTO model);
        Task<List<GetRoomDTO>> SearchByNameAsync(SearchRoomDTO model);
        Task<List<GetRoomDTO>> FilterByTageAsync(FilterByTageDTO model);
        Task<List<GetRoomDTO>> FilterByFacilityAsync(FilterByFacilityDTO model);
    }
}
