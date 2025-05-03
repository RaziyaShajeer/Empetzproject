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
    public interface ICategoryRequestRepository
    {
        Task AddAsync(PetsCategory category);
        byte[] ConvertImageToByteArray(IFormFile? imageFile);
        Task<List<CategoryDto>> GetAllCategoriesAsync();
        Task<PagedList<CategoryDto>> GetAllCategoriesAsync(CategoryParams param);
        Task<CategoryDto> GetCategoryById(Guid catid);
        Task<bool> UpdateCategoryAsync(PetsCategory userEntity);
    }
}
