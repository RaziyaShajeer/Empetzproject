using AutoMapper;
using Domain.Service.Category.Interfaces;
using Empetz_API.API.Category;
using Empetz_API.API.Category.RequestObject;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Domain.Service.Category;
using Domain.Service.Category.DTOs;
using Microsoft.AspNetCore.Http;

namespace Empetz_API.Test.Controller
{
    public class CategoryControllerTest
    {
        private readonly ICategoryRequestService _CategoryRequestService;
        private readonly IMapper _mapper;

        //public CategoryControllerTest()
        //{
        //    _mapper = A.Fake<IMapper>();
        //    _CategoryRequestService = A.Fake<ICategoryRequestService>();
            
        //}

        //[Fact]
        //public void Get_All_Categories_Return_OK()
        //{
        //    // Arrange
        //    var category = A.Fake<ICollection<CategoryRequest>>();
        //    var categoryList = A.Fake<List<CategoryRequest>>();
        //    A.CallTo(() => _mapper.Map<List<CategoryRequest>>(category)).Returns(categoryList);
        //    var controller = new CategoryController(_mapper, _CategoryRequestService);

        //    // Act
        //    var result = controller.GetCategories().Result; // Use Result to wait for the Task

        //    // Assert
        //    result.Should().NotBeNull();
        //    result.Should().BeOfType<OkObjectResult>();
        //}
        //[Fact]
        //public async Task CategoryController_CreateCategory_ReturnOK()
        //{
        //    // Arrange
        //    var categoryController = new CategoryController(_mapper, _CategoryRequestService);
        //    var categoryAddRequest = new CategoryAddRequest
        //    {
        //        // Set properties of the categoryAddRequest as needed for your test
        //    };

        //    // Mock the behavior of your service
        //    A.CallTo(() => _CategoryRequestService.AddCategoryAsync(A<CategoryAddDTO>._))
        //        .Returns(true); // Set to false to test the BadRequest case

        //    // Act
        //    var result = await categoryController.AddCategory(categoryAddRequest);

        //    // Assert
        //    result.Should().BeOfType<OkObjectResult>()
        //        .Which.Value.Should().Be("Category Added successfully");
        //}
        //[Fact]
        //public async Task CategoryController_GetCategoryById_ReturnsOk()
        //{
        //    // Arrange
        //    var categoryId = Guid.NewGuid();
        //    var categoryDto = new CategoryDto
        //    {
        //        Id = categoryId,
        //        Name = "SampleCategory",
        //        Image = new byte[] { /* Sample image data */ }
        //    };

        //    A.CallTo(() => _CategoryRequestService.GetCategoryById(categoryId))
        //        .Returns(Task.FromResult(categoryDto));

        //    var categoryController = new CategoryController(_mapper, _CategoryRequestService);

        //    // Act
        //    var result = await categoryController.GetCategorybyId(categoryId);

        //    // Assert
        //    result.Should().BeOfType<OkObjectResult>()
        //        .Which.Value.Should().BeEquivalentTo(_mapper.Map<CategoryRequest>(categoryDto));
        //}
        //[Fact]
        //public async Task CategoryController_GetCategoryById_ReturnsBadRequest()
        //{
        //    // Arrange
        //    var categoryId = Guid.NewGuid();

        //    A.CallTo(() => _CategoryRequestService.GetCategoryById(categoryId))
        //        .Throws(new Exception("Simulated error"));

        //    var categoryController = new CategoryController(_mapper, _CategoryRequestService);

        //    // Act
        //    var result = await categoryController.GetCategorybyId(categoryId);

        //    // Assert
        //    result.Should().BeOfType<BadRequestResult>();
        //}

        //[Fact]
        //public async Task CategoryController_UpdateUser_ReturnsOk()
        //{
        //    // Arrange
        //    var categoryId = Guid.NewGuid();
        //    var categoryUpdateRequest = new CategoryUpdateRequest
        //    {
        //        Id = categoryId,
        //        Name = "UpdatedCategoryName",
              
        //    };

        //    A.CallTo(() => _CategoryRequestService.UpdateCategoryAsync(A<CategoryUpdateDTo>._))
        //        .Returns(true);

        //    var categoryController = new CategoryController(_mapper, _CategoryRequestService);

        //    // Act
        //    var result = await categoryController.UpdateUser(categoryUpdateRequest);

        //    // Assert
        //    result.Should().BeOfType<OkObjectResult>()
        //        .Which.Value.Should().Be("Category updated successfully");
        //}

        //[Fact]
        //public async Task CategoryController_UpdateCategory_ReturnsBadRequestWhenIdIsNull()
        //{
        //    // Arrange
        //    var categoryController = new CategoryController(_mapper, _CategoryRequestService);
        //    var categoryUpdateRequest = new CategoryUpdateRequest
        //    {
        //        // Set other properties as needed for your test, but leave Id as null
        //        Id = Guid.Empty,
        //        Name = "SampleCategory",
        //        ImageFile = A.Fake<IFormFile>()
        //    };

        //    // Act
        //    var result = await categoryController.UpdateUser(categoryUpdateRequest);

        //    // Assert
        //    result.Should().BeOfType<BadRequestObjectResult>()
        //        .Which.Value.Should().Be("Failed to update Category");
        //}


    }
}

