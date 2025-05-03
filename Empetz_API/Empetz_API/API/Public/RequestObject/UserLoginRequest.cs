using System.ComponentModel.DataAnnotations;

namespace Empetz_API.API.Public.RequestObject
{
    public class UserLoginRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string password { get; set; }
    }
}
