using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.User.DTO
{
    public class UpdateUserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;

        public string? LastName { get; set; }

        public string? Email { get; set; } = null!;

        public IFormFile ImageFile { get; set; }// Added for image upload
        public byte[] Image { get; set; } = null!;

        public string Phone { get; set; } = null!;
        public string Role { get; set; }
    }
}
