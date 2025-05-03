using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Login.DTOs
{
    public class PublicUserLoginDto
    {
        public Guid Id { get; set; }

        public string ?FirstName { get; set; } = null!;

        public string? LastName { get; set; }
        [EmailAddress]
        public string? Email { get; set; } = null!;
        [Phone]
        public string? Phone { get; set; } = null!;
        public string? Token { get; set; }
        public string? Role { get; set; }
        public PublicUserLoginDto()
        {
            
        }
    }
}
