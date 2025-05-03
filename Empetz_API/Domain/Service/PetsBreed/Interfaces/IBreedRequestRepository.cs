using Domain.Service.PetsBreed.DTOs;
using Domain.Service.Category.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Service.PetsBreed.Interfaces
{
    public interface IBreedRequestRepository
    {
        Task<List<BreedDto>> GetAllBreedsAsync();
        Task<List<BreedDto>> GetAllBreedsAsync(Guid category);
        Task<List<BreedDto>> GetAllBreedsIdAsync(Guid id);
        Task<bool> AddBreed(Breed breed);
    }
}
