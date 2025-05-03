using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.WishLIst.DTOs
{
    public class GetWishListDTO
    {

        public Guid id { get; set; }
        public Guid userid { get; set; }
        public Guid petId { get; set; }
        public string Name { get; set; }
        public string BreedName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Discription { get; set; }
        public string height { get; set; }
		public string weight { get; set; }
        public Guid location { get; set; }
		public byte[] Image { get; set; }

        public string CategoryName { get; set; }
		public string Status { get; set; }
	}
}
