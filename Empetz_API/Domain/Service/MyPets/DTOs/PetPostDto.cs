using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.MyPets.DTOs
{
	public class PetPostDto
	{
		public string Name { get; set; } = null!;
		public Guid BreedId { get; set; }
		public int Age { get; set; }
		public string Gender { get; set; } = null!;
		public string Discription { get; set; } = null!;
		public byte[] Image { get; set; } = null!;

        public string ImagePath { get; set; }
        public Guid CategoryId { get; set; }
		public Guid UserId { get; set; }
		public bool Vaccinated { get; set; }
        public DateTime? VaccinationDate { get; set; }
        public bool Certified { get; set; }
		public Guid LocationId { get; set; }
		public long? Price { get; set; }
        public string height { get; set; }
        public string weight { get; set; }

		     public string Address { get; set; }
        //public string Status { get; set; }
    }
}
