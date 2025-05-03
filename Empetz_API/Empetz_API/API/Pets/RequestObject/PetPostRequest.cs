using Domain.Enums;

namespace Empetz_API.API.Pets.RequestObject
{
	public class PetPostRequest
	{

		public string? Name { get; set; } = null!;
		public Guid? BreedId { get; set; }
		public int? Age { get; set; }
		public string ?Gender { get; set; } = null!;
		public string ?Discription { get; set; } = null!;
		public IFormFile ?ImageFile { get; set; } = null;
		public Guid CategoryId { get; set; }

        public string Address { get; set; }
        public Guid LocationId { get; set; }
	
		public Guid? UserId { get; set; }
		public bool Vaccinated { get; set; }
        public DateTime? VaccinationDate { get; set; }
        public bool? Certified { get;set; }
		public long? Price { get; set; }
		public string height { get; set; }
		public string weight { get; set; }


    }
}
