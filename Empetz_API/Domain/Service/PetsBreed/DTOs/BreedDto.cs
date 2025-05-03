using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.PetsBreed.DTOs
{
    public class BreedDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string CategoryNavigationName { get; set; }
    }
}
