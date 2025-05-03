using AutoMapper;
using DAL.Models;
using Domain.Models;
using Domain.Models;
using Domain.Service.PetsBreed.DTOs;
using Domain.Service.PetsBreed.Interfaces;
using Domain.Service.Category.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Domain.Service.Beed
{
    public class BreedRequestRepository : IBreedRequestRepository
    {

        protected readonly EmpetzContext _context;
        IMapper _mapper;
        public BreedRequestRepository(EmpetzContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }
      
        public async Task<List<BreedDto>> GetAllBreedsAsync()
        {
            var Breeds = await _context.Breeds.Include(e=>e.CategoryNavigation).ToListAsync();
            return _mapper.Map<List<BreedDto>>(Breeds);
        }

        public async Task<List<BreedDto>> GetAllBreedsAsync(Guid category)
        {
            var Breeds = await _context.Breeds
             .Where(b => b.Category == category)
             .ToListAsync();
            return _mapper.Map<List<BreedDto>>(Breeds);

        }

        public async Task<List<BreedDto>> GetAllBreedsIdAsync(Guid id)
        {
            var Breeds = await _context.Breeds
             .Where(b => b.Id == id).Include(e => e.CategoryNavigation)
             .ToListAsync();
            return _mapper.Map<List<BreedDto>>(Breeds);
        }
        public async Task<bool> AddBreed(Breed breed)
        {
            try
            {
                var breeds = _context.Breeds.Where(e => e.Name == breed.Name&&e.Category==breed.Category).FirstOrDefault();
                if (breeds == null)
                {
                    breed.Id = Guid.NewGuid();
                    await _context.Breeds.AddAsync(breed);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    throw new AlreadyExistException();
                }
            }
           
        
            catch(Exception ex)
            {
                throw ex;
                return false;
            }
            
            
        }
    }
}
