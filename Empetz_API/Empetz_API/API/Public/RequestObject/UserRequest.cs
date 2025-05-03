using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Empetz_API.API.Public.RequestObject
{
    public class UserRequest
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string? LastName { get; set; }
      
        public string? Email { get; set; } = null!;

        public byte[] Image { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public Role Role { get; set; }

        public string ? Address { get; set; }
        public DateTime Accountcreated { get; set; }
    }
}
