using System.ComponentModel.DataAnnotations;

namespace HotelReservation.Core.Dtos
{
    public class TokenDto
    {
        [Required]
        public string AccessToken { get; set; } = string.Empty;

        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
