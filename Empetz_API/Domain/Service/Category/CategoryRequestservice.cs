using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Domain.Service.Category.DTOs;
using Domain.Service.Category.Interfaces;
using Domain.Service.User.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Category
{
    public class CategoryRequestservice : ICategoryRequestService
    {
        ICategoryRequestRepository _repository;
        IMapper _mapper;
        public CategoryRequestservice(ICategoryRequestRepository repository, IMapper mapper)
        {
                _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> AddCategoryAsync(CategoryAddDTO categoryDto)
        {
            var category = _mapper.Map<PetsCategory>(categoryDto);
            await _repository.AddAsync(category);
            return true; 
        }

        public byte[] ConvertImageToByteArray(IFormFile? imageFile)
        {
            return _repository.ConvertImageToByteArray(imageFile);
        }

        public async Task<PagedList<CategoryDto>> getAllCategories(CategoryParams param)
        {
            return await _repository.GetAllCategoriesAsync(param);
        }

        public async Task<List<CategoryDto>> Getcategories()
        {
            return await _repository.GetAllCategoriesAsync();
        }
		
			 public async Task<CategoryDto>GetCategoryById(Guid catid)
		{
			return await _repository.GetCategoryById(catid);
		}

        public async Task<bool> UpdateCategoryAsync(CategoryUpdateDTo updateCategoryDTO)
        {
            var userEntity = _mapper.Map<PetsCategory>(updateCategoryDTO);

            // Call repository for update
            var result = await _repository.UpdateCategoryAsync(userEntity);

            return result;
        }

		Task<PagedList<CategoryDto>> ICategoryRequestService.getAllCategories(CategoryParams param)
		{
			return _repository.GetAllCategoriesAsync(param);
		}
	}
}
