using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Register.DTOs
{
    public class UserSignUpDto
    {
        public string FirstName { get; set; } = null!;
        public string UserName { get; set; } = null!;
       
        public string Phone { get; set; } = null!;
        public string? Email { get; set; }
        public string password { get; set; } = null!;
    }
}
