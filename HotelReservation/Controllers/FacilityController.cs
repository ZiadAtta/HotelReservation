using HotelReservation.Core.DTOs.FacilityDTOs;
using HotelReservation.Core.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        private readonly IFacilityService _facilityService;

        public FacilityController(IFacilityService facilityService)
        {
            _facilityService = facilityService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFacility([FromBody] CreateFacilityDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            var result = await _facilityService.CreateFacility(dto);
            if (!result)
                return BadRequest("Facility already exists or failed to create.");

            return Ok("Facility created successfully.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFacility([FromBody] UpdateFacilityDTO dto)
        {
            var result = await _facilityService.UpdateFacility(dto);
            if (!result)
                return NotFound("Facility not found or update failed.");

            return Ok("Facility updated successfully.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFacility([FromBody] DeleteFacilityDTO dto)
        {
            var result = await _facilityService.DeleteFacility(dto);
            if (!result)
                return NotFound("Facility not found or delete failed.");

            return Ok("Facility deleted successfully.");
        }
    }
}
