using System.ComponentModel.DataAnnotations;

namespace HotelReservation.Core.Dtos
{
    public class SignupDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(30, ErrorMessage = "Name cannot exceed 30 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(30, ErrorMessage = "Email cannot exceed 30 characters")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [MaxLength(15, ErrorMessage = "Phone number cannot exceed 15 characters")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country is required")]
        [MaxLength(50, ErrorMessage = "Country cannot exceed 50 characters")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        /* [MaxLength(30, ErrorMessage = "Password cannot exceed 30 characters")]
         [DataType(DataType.Password)]
         [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
             ErrorMessage = "Password must contain at least 8 characters with uppercase, lowercase, number, and special character")]*/
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password confirmation is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
