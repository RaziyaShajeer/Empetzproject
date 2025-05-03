using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.PetsBreed.DTOs
{
    public class BreadRequestDTO
    {
     
        public class BreadDTO
        {
            public string Name { get; set; } = null!;

            public Guid Category { get; set; }
        }
    }

}
