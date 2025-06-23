using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        //[AllowAnonymous]
        //[Authorize(Roles = "HotelStaff")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetPeople()
        {
            return Ok("People data");
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreatePerson()
        {
            return Ok("Person is created");
        }
    }
}
