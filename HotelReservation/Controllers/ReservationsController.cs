using AutoMapper;
using HotelReservation.Core.Dtos.Reservation;
using HotelReservation.Core.Entities;
using HotelReservation.Core.IServices;
using HotelReservation.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReservationsController(IReservationService reservation) : ControllerBase
{
    private readonly IReservationService _reservation = reservation;

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _reservation.GetAllAsync();
        if (response == null || !response.Any())
        {
            return NotFound(new { Message = "No reservations found." });
        }
        return Ok(response);
   
    }

    [HttpGet("Get/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var reservation = await _reservation.GetByIdAsync(id);
        if (reservation == null)
            return NotFound();

        return Ok(reservation);
    }

    [HttpPost("create")]
    public async Task<ActionResult<ReservationDto>> CreateReservation([FromBody] CreateDto createDto)
    {
        var result = await _reservation.CreateAsync(createDto);
        return Ok(result);
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateReservation([FromRoute]int id, [FromBody] UpdateDto updateDto)
    {
        var updatedReservation = await _reservation.UpdateAsync(id, updateDto);
        if (updatedReservation == null)
        {
            return NotFound(new { Message = "Reservation not found." });
        }
        return Ok(updatedReservation);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteReservation([FromRoute]int id)
    {
        var isDeleted = await _reservation.DeleteAsync(id);
        if (!isDeleted)
        {
            return NotFound(new { Message = "Reservation not found." });
        }
        return NoContent();
    }
}
