using AutoMapper;
using Domain.Models;

using Domain.Service.Category.DTOs;
using Domain.Service.Category.Interfaces;
using Domain.Service.PetsBreed.DTOs;
using Domain.Service.PetsBreed.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.PetsBreed
{
    public class BreedRequestService : IBreedRequestService
    {
        IBreedRequestRepository _repository;
        IMapper _mapper;
        public BreedRequestService(IBreedRequestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

       

        public async Task<List<BreedDto>> GetBreedbyId(Guid id)
        {
            return await _repository.GetAllBreedsIdAsync(id);
        }

        public async Task<List<BreedDto>> GetBreedCategory(Guid category)
        {
            return await _repository.GetAllBreedsAsync(category);
        }

        public async Task<List<BreedDto>> GetBreeds()
        {
            return await _repository.GetAllBreedsAsync();
        }
        public async Task<bool> AddBreed(BreedPostDto breedtoadd)
        {
            Breed breed =_mapper.Map<Breed>(breedtoadd);  
            return await _repository.AddBreed(breed);
        }
    }
}
