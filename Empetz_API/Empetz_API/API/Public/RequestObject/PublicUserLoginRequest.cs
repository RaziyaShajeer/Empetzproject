using System.ComponentModel.DataAnnotations;

namespace Empetz_API.API.Public.RequestObject
{
    public class PublicUserLoginRequest
    {
        [Required]
        [Phone]
        public string Phone { get; set; }
    }
}
