using Domain.Models;
using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.MyPets.DTOs
{
    public class PetDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
		public string Phone { get; set; } = null!;
		public string Gender { get; set; } = null!;
        public string Discription { get; set; } = null!;
        public string ImagePath { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string BreedName { get; set; }
		public Guid UserId { get; set; }
		public string UserFirstName { get; set; }
        
        public string LocationName { get; set; } = null!;
        public string height { get; set; }
        public string weight { get; set; }

        public string ?Status { get; set; }
        public DateTime? VaccinationDate { get; set; }
        public bool? Certified { get; set; }
        public bool? Vaccinated { get; set; }

        public string Address { get; set; }

        public DateTime? PetPosted { get; set; }



        public long Price { get; set; }
        

       

    }
}
