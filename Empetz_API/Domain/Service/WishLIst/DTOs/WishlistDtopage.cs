using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.WishLIst.DTOs
{
    public class WishlistDtopage
    {
		//public Guid id { get; set; }
		//public string UserNavigationFirstName { get; set; }
		//public string PetNavigationName { get; set; }
		////public string Name { get; set; }
		//public string PetNavigationBreedName { get; set; }
		//public int PetNavigationAge { get; set; }
		//public string PetNavigationGender { get; set; }
		//public string PetNavigationDiscription { get; set; }
		//public byte[] PetNavigationImage { get; set; }

		//public string PetNavigationCategoryName { get; set; }
		//public string status { get; set; }
		public Guid Id { get; set; }
		public string Name { get; set; } = null!;
		public int Age { get; set; }
		public string Phone { get; set; } = null!;
		public string Gender { get; set; } = null!;
		public string ?Discription { get; set; } = null!;
		public byte[] ?Image { get; set; } = null!;
		public string ?CategoryName { get; set; } = null!;
		public string ?BreedName { get; set; }
		public Guid ?UserId { get; set; }
		public string ?UserFirstName { get; set; }

		public string ?LocationName { get; set; } = null!;
		public string ?height { get; set; }
		public string ?weight { get; set; }
		public string ?Status { get; set; }
		public DateTime? VaccinationDate { get; set; }
		public bool? Certified { get; set; }
		public bool? Vaccinated { get; set; }
	}
}
