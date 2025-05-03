using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
	public class PetfilterParams:PaginationParams
	{
		public int? FromAge { get; set; }
		public int? ToAge { get; set; }
		public DateTime? DatePublished { get; set; }
		public long? Pricestarting { get; set; }
		public long? Priceending { get; set; }
		public bool? vaccinated { get; set; }
		public bool? certified { get; set; }
		public Guid? BreedId { get; set; }
	}
}
