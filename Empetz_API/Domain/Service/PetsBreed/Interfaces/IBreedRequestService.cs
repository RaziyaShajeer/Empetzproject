using Domain.Service.PetsBreed.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Service.PetsBreed.Interfaces
{
    public interface IBreedRequestService
    {
       
        Task<List<BreedDto>> GetBreedbyId(Guid id);
        Task<List<BreedDto>> GetBreedCategory(Guid category);

        Task<List<BreedDto>> GetBreeds();
        Task<bool> AddBreed(BreedPostDto breed);
    }
}
