using Domain.Enums;

namespace Empetz_API.API.Pets.RequestObject
{
	public class PetList
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public string Name { get; set; }
		public string BreedName { get; set; }

		public int Age { get; set; }
		public string Gender { get; set; }
		public string Discription { get; set; }

        public string? ImagePath { get; set; }
        //public byte[] Image { get; set; }
		public string CategoryName { get; set; }
        public DateTime? PetPosted { get; set; }

        public string UserFirstName { get; set; }

		public string LocationName { get; set; }
		public bool? Vaccinated { get; set; }
        public DateTime? VaccinationDate { get; set; }
        public bool? Certified { get; set; }
		public long? Price { get; set; }
        public string height { get; set; }
        public string weight { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
    }
}
