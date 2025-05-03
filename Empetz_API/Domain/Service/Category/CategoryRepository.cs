using AutoMapper;
using DAL.Models;
using Domain.Helpers;
using Domain.Models;
using Domain.Service.Category.DTOs;
using Domain.Service.Category.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Category
{
    public class CategoryRepository : ICategoryRequestRepository
    {
        protected readonly EmpetzContext _context;
        IMapper _mapper;
        public CategoryRepository(EmpetzContext dbContext,IMapper mapper) {
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task AddAsync(PetsCategory category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));
            category.Id = Guid.NewGuid();
            _context.PetsCategories.Add(category);
            await _context.SaveChangesAsync();
        }

        public byte[] ConvertImageToByteArray(IFormFile? imageFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                imageFile.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _context.PetsCategories.ToListAsync();
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<PagedList<CategoryDto>> GetAllCategoriesAsync(CategoryParams param)
        {
            var query = _context.PetsCategories.ToList().AsQueryable();
            var categoryDtos = _mapper.Map<List<CategoryDto>>(query);

            

            return new PagedList<CategoryDto>(categoryDtos, param.PageNumber, param.PageSize);

        }

        public async Task<CategoryDto> GetCategoryById(Guid cmpId)
		{
			var categories = await _context.PetsCategories.Where(e=>e.Id==cmpId).FirstOrDefaultAsync();
			return _mapper.Map<CategoryDto>(categories);
		}

        public async Task<bool> UpdateCategoryAsync(PetsCategory userEntity)
        {
            //try
            //{
               _context.PetsCategories.Update(userEntity);
                await _context.SaveChangesAsync();
                return true;
            //}
            //catch(Exception ex) {
            //    return false;
            //}
            //_context.Entry(userEntity).State = EntityState.Modified;
            // await _context.SaveChangesAsync();
            
        }
    }
}
