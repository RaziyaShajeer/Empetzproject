using AutoMapper;
using Domain.Models;
using Domain.Service.Category.DTOs;
using Domain.Service.Category.Interfaces;
using Domain.Service.User.DTO;
using Domain.Service.User;
using Empetz.Controllers;
using Empetz_API.API.Category.RequestObject;
using Empetz_API.API.Public;
using Empetz_API.API.Public.RequestObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Service.User.Interfaces;
using Domain.Helpers;
using Domain.Service.MyPets;
using Empetz_API.API.Pets.RequestObject;
using HireMeNow_WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Empetz_API.Domain.Helper;
using Azure;

namespace Empetz_API.API.Category
{

    [ApiController]
    [Authorize(Roles = "PublicUser")]
    public class CategoryController : BaseApiController<CategoryController>
    {
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment environment;
        public ICategoryRequestService CategoryRequestService { get; set; }
        public CategoryController(IWebHostEnvironment environment, IMapper mapper, ICategoryRequestService _CategoryRequestService)
        {
            _mapper = mapper;
            CategoryRequestService = _CategoryRequestService;
            this.environment = environment;

        }
        [HttpGet]
        [Route("category")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                List<CategoryDto> Category = await CategoryRequestService.Getcategories();
                return Ok(_mapper.Map<List<CategoryRequest>>(Category));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
        
       

        [HttpGet]
        [Route("category/{categoryId}")]
        public async Task<IActionResult> GetCategorybyId(Guid categoryId)
        {
            try
            {
                CategoryDto Category = await CategoryRequestService.GetCategoryById(categoryId);
                return Ok(_mapper.Map<CategoryRequest>(Category));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }


        //[HttpPost("category")]
        //public async Task<IActionResult> AddCategory([FromForm] CategoryAddRequest categoryRequest)
        //{
        //    // Map the request to DTO

        //    var categoryDto = _mapper.Map<CategoryAddDTO>(categoryRequest);
        //    byte[] byteArray = CategoryRequestService.ConvertImageToByteArray(categoryRequest.ImageFile);
        //    categoryDto.Image = byteArray;

        //    // Call the service
        //    var result = await CategoryRequestService.AddCategoryAsync(categoryDto);

        //    if (result)
        //        return Ok("Category Added successfully");

        //    return BadRequest("Failed to Add Category");
        //}

        [HttpPatch("category/{categoryid}")]
        public async Task<IActionResult> UpdateUser([FromForm] CategoryUpdateRequest categoryRequest, Guid categoryid)
        {
            string Imageurl = string.Empty;
            if (categoryid == null)
            {
                return BadRequest("Id is required ");
            }
            var updateCategoryDTO = _mapper.Map<CategoryUpdateDTo>(categoryRequest);
            string uniqueFileName1 = Guid.NewGuid().ToString();
            try
            {
                string Filepath = GetFilepath(categoryRequest.Name);
                if (!System.IO.Directory.Exists(Filepath))
                {
                    System.IO.Directory.CreateDirectory(Filepath);
                }

                string imagepath = Filepath + "\\" + uniqueFileName1 + ".png";
                if (System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                }
                using (FileStream stream = System.IO.File.Create(imagepath))
                {
                    await categoryRequest.ImageFile.CopyToAsync(stream);

                }

                string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


                if (System.IO.File.Exists(imagepath))
                {
                    Imageurl = hosturl + "/Upload/Category/" + categoryRequest.Name + "/" + uniqueFileName1 + ".png";
                }

                updateCategoryDTO.ImagePath = Imageurl;

            }
            catch (Exception ex)
            {

            }

            updateCategoryDTO.Id = categoryid;
          
            var result = await CategoryRequestService.UpdateCategoryAsync(updateCategoryDTO);

            if (result)
                return Ok("Category updated successfully");

            return BadRequest("Failed to update Category");
        }

        [NonAction]
        private string GetFilepath(string itemname)
        {
            return this.environment.WebRootPath + "\\Upload\\Category\\" + itemname;
        }

        [HttpPost("category")]
        public async Task<IActionResult> AddCategoryasync([FromForm] CategoryAddRequest categoryRequest)
        {
            string Imageurl = string.Empty;

            // Map the request to DTO
            var categoryDto = _mapper.Map<CategoryAddDTO>(categoryRequest);
            string uniqueFileName1 = Guid.NewGuid().ToString();
            try
            {
                string Filepath = GetFilepath(categoryRequest.Name);
                if (!System.IO.Directory.Exists(Filepath))
                {
                    System.IO.Directory.CreateDirectory(Filepath);
                }

                string imagepath = Filepath + "\\" + uniqueFileName1 + ".png";
                if (System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                }
                using (FileStream stream = System.IO.File.Create(imagepath))
                {
                    await categoryRequest.ImageFile.CopyToAsync(stream);
             
                }
                
                string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
               
                   
                    if (System.IO.File.Exists(imagepath))
                    {
                        Imageurl = hosturl + "/Upload/Category/" + categoryRequest.Name + "/" + uniqueFileName1 + ".png";
                    }

                categoryDto.ImagePath = Imageurl;
                
            }
            catch (Exception ex)
            {
              
            }
        
            var result = await CategoryRequestService.AddCategoryAsync(categoryDto);

            if (result)
                return Ok("Category Added successfully");   

            return BadRequest("Failed to Add Category");
        }
      
        
    }

}






