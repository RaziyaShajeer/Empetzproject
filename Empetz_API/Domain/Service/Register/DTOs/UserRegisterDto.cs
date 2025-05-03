using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Register.DTOs
{
	public  class UserRegisterDto
	{
		public string FirstName { get; set; } = null!;
		public string Phone { get; set; } = null!;
        public DateTime Accountcreated { get; set; }
		public string? LastName { get; set; }
		public string? Email { get; set; } = null!;
		public byte[]? Image { get; set; } = null!;
        public string Address { get; set; }
    }
}
