using Domain.Helpers;
using Domain.Models;
using Domain.Service.Category.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Category.Interfaces
{
    public interface ICategoryRequestService
    {
        Task<bool> AddCategoryAsync(CategoryAddDTO categoryDto);
        byte[] ConvertImageToByteArray(IFormFile? imageFile);
        public Task<PagedList<CategoryDto>> getAllCategories(CategoryParams param);
        Task<List<CategoryDto>> Getcategories();
		Task<CategoryDto> GetCategoryById(Guid catid);
        Task<bool> UpdateCategoryAsync(CategoryUpdateDTo updateCategoryDTO);
    }
}
