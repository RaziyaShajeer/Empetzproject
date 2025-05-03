using Domain.Enums;

namespace Empetz_API.API.Pets.RequestObject
{
	public class PetFilter
	{
		public int? FromAge { get; set; }
		public int? ToAge { get; set; }
		public DateTime? DatePublished { get; set; }
		public long? Pricestarting { get; set; }
		public long? Priceending { get; set; }
		public Vaccinated? vaccinated { get; set; }
		public Certified? certified { get; set; }
		public Guid? BreedId { get; set; }
	}

}
