using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Login.DTOs
{
    public class UserLoginDto
    {
        public string? UserName { get; set; } = null!;
        [Phone]
        public string? Phone { get; set; } = null!;
        public string? Token { get; set; }
        public string? Role { get; set; }
    }
}
