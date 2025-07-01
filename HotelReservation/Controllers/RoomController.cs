using HotelReservation.Core.DTOs.RoomDTOs;
using HotelReservation.Core.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomDTO model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            var success = await _roomService.CreateRoomAsync(model);
            if (!success)
                return BadRequest("Failed to create room");

            return Ok("Room created successfully");
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllRooms([FromQuery] RoomPaginationDTO model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors)
                                              .Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }

            var rooms = await _roomService.GetAllRoomsAsync(model);
            if (rooms == null || !rooms.Any())
                return NotFound("No rooms found");

            return Ok(rooms);
        }

        [HttpGet("SearchByName")]
        public async Task<IActionResult> SearchByName([FromQuery] SearchRoomDTO model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors)
                                              .Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }

            var rooms = await _roomService.SearchByNameAsync(model);
            if (rooms == null || !rooms.Any())
                return NotFound("No rooms found");

            return Ok(rooms);
        }

        [HttpGet("FilterByTag")]
        public async Task<IActionResult> FilterByTag([FromQuery] FilterByTageDTO model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors)
                                              .Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }

            var rooms = await _roomService.FilterByTageAsync(model);
            if (rooms == null || !rooms.Any())
                return NotFound("No rooms found");

            return Ok(rooms);
        }

        [HttpGet("FilterByFacility")]
        public async Task<IActionResult> FilterByFacility([FromQuery] FilterByFacilityDTO model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors)
                                              .Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }

            var rooms = await _roomService.FilterByFacilityAsync(model);
            if (rooms == null || !rooms.Any())
                return NotFound("No rooms found");

            return Ok(rooms);
        }
    }
}
