using System.ComponentModel.DataAnnotations;

namespace Empetz_API.ContactUs.RequestObject
{
    public class ContactUsRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }

        public Guid UserId { get; set; }
    }
}
