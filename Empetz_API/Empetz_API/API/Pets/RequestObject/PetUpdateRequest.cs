using Domain.Enums;

namespace Empetz_API.API.Pets.RequestObject
{
	public class PetUpdateRequest
	{
	
		public string? Name { get; set; } = null!;
		public Guid? BreedId { get; set; }
		public int? Age { get; set; }
		public string? Gender { get; set; } = null!;
		public string? Discription { get; set; } = null!;
		public IFormFile? ImageFile { get; set; } = null!;
		public Guid? CategoryId { get; set; }


		public Guid? LocationId { get; set; }

		public Guid? UserId { get; set; }
		public Vaccinated? Vaccibbnated { get; set; }

		public Certified? Certified { get; set; }
		public long? Price { get; set; }
        public string Status { get; set; }
    }
}
