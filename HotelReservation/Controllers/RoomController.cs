using HotelReservation.Core.DTOs.RoomDTOs;
using HotelReservation.Core.Interfaces;
using HotelReservation.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

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
                // return BadRequest(ModelState);
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            var success = await _roomService.CreateRoomAsync(model);
            if (!success)
                return BadRequest("Failed to create room");

            return Ok("Room created successfully");
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            if (rooms == null || !rooms.Any())
                return NotFound("No rooms found");
            return Ok(rooms);
        }
    }
}
