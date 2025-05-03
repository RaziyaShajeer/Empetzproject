using System.ComponentModel.DataAnnotations;

namespace Empetz_API.API.Public.RequestObject
{
    public class UserSignUpRequest
    {


        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number format. It should be 10 digits.")]
        public string Phone { get; set; } = null!;
        [EmailAddress]
        public string? Email { get; set; } = null!;
        [Required]
        public string password { get; set; } = null!;
    }
}
