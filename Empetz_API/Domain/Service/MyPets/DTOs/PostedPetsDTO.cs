using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.MyPets.DTOs
{
    public class PostedPetsDTO
    {
		public Guid Id { get; set; }

		public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string Gender { get; set; } = null!;
        public string Discription { get; set; } = null!;
        public byte[] Image { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string BreedName { get; set; }
		public Guid UserId { get; set; }
		public string UserFirstName { get; set; }
        public string LocationName { get; set; } = null!;
        public string TimeAgo { get; set; }
        public DateTime? PetPosted { get; set; }
        public string Status { get; set; }
    }
}
