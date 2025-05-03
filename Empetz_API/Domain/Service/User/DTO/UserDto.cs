using Domain.Models;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Service.User.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string? LastName { get; set; }
        [EmailAddress]
        public string? Email { get; set; } = null!;
        [Phone]
        public string Phone { get; set; } = null!;

        public byte[] Image { get; set; } = null!;
        public int Role { get; set; }
        public string? ConnectionId { get; set; }
        public DateTime Accountcreated { get; set; }
        
    }
}
