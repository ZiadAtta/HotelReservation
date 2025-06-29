using HotelReservation.Core.DTOs.OfferDTOs;
using HotelReservation.Core.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOffer([FromBody] CreateOfferDTO dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            var result = await _offerService.CreateOffer(dto);
            if (!result)
                return BadRequest("Failed to create offer. Discount may be greater than room price or room doesn't exist.");

            return Ok("Offer created successfully.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOffer([FromBody] UpdateOfferDTO dto)
        {
            var result = await _offerService.UpdateFacility(dto);
            if (!result)
                return BadRequest("Failed to update offer. Check if the room or offer exists and discount is valid.");

            return Ok("Offer updated successfully.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOffer([FromBody] DeleteOfferDTO dto)
        {
            var result = await _offerService.DeleteFacility(dto);
            if (!result)
                return NotFound("Offer not found or could not be deleted.");

            return Ok("Offer deleted successfully.");
        }
    }
}
