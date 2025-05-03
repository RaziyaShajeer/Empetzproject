using AutoMapper;
using Domain.Service.PetsBreed.DTOs;
using Domain.Service.Beed;
using Domain.Service.PetsBreed.Interfaces;
using Domain.Service.Category;
using Domain.Service.Category.Interfaces;
using Empetz_API.API.Breed.RequestObject;
using Empetz_API.API.Breed;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Service.PetsBreed.DTOs;

namespace Empetz_API.Test.Controller
{
    public class BreedTestController
    {
        private readonly IBreedRequestService _breedRequestService;
        private readonly IMapper _mapper;

        public BreedTestController()
        {
            _mapper = A.Fake<IMapper>();
            _breedRequestService = A.Fake<IBreedRequestService>();
        }


        [Fact]
        public async Task GetBreeds_ReturnsOkResult()
        {
            // Arrange
            var breedController = new BreedController(_mapper, _breedRequestService);

            // Mock the behavior of _breedRequestService.GetBreeds to return a list of BreedDto
            var fakeBreedDtoList = new List<BreedDto>
            {
                new BreedDto { Id = Guid.NewGuid(), Name = "Breed1" },
                new BreedDto { Id = Guid.NewGuid(), Name = "Breed2" }
                // Add more BreedDto as needed
            };

            A.CallTo(() => _breedRequestService.GetBreeds()).Returns(Task.FromResult(fakeBreedDtoList));

            // Act
            var result = await breedController.GetBreeds();

            // Assert
            Assert.IsType<OkObjectResult>(result);

            var okResult = (OkObjectResult)result;
            Assert.IsAssignableFrom<List<BreedRequest>>(okResult.Value);
        }

        [Fact]
        public async Task GetBreeds_ReturnsBadRequestResultOnException()
        {
            // Arrange
            var breedController = new BreedController(_mapper, _breedRequestService);

            // Mock the behavior of _breedRequestService.GetBreeds to throw an exception
            A.CallTo(() => _breedRequestService.GetBreeds()).Throws<Exception>();

            // Act
            var result = await breedController.GetBreeds();

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task GetBreedsByCategory_ReturnsOkResult()
        {
            // Arrange
            var breedController = new BreedController(_mapper, _breedRequestService);
            var categoryId = Guid.NewGuid();

            // Mock the behavior of _breedRequestService.GetBreedCategory to return a list of BreedDto
            var fakeBreedDtoList = new List<BreedDto>
            {
                new BreedDto { Id = Guid.NewGuid(), Name = "Breed1" },
                new BreedDto { Id = Guid.NewGuid(), Name = "Breed2" }
                // Add more BreedDto as needed
            };

            A.CallTo(() => _breedRequestService.GetBreedCategory(categoryId)).Returns(Task.FromResult(fakeBreedDtoList));

            // Act
            var result = await breedController.GetBreedsBycategory(categoryId);

            // Assert
            Assert.IsType<OkObjectResult>(result);

            var okResult = (OkObjectResult)result;
            Assert.IsAssignableFrom<List<BreedRequest>>(okResult.Value);
        }
        [Fact]
        public async Task GetBreedsByCategory_ReturnsBadRequestResult()
        {
            // Arrange
            var breedController = new BreedController(_mapper, _breedRequestService);

            // Mock the behavior of _breedRequestService.GetBreedCategory to throw an exception
            A.CallTo(() => _breedRequestService.GetBreedCategory(A<Guid>._)).Throws<Exception>();

            // Act
            var result = await breedController.GetBreedsBycategory(Guid.NewGuid());

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task GetBreedsById_ReturnsBadRequestResult()
        {
            // Arrange
            var breedController = new BreedController(_mapper, _breedRequestService);

            // Mock the behavior of _breedRequestService.GetBreedbyId to throw an exception
            A.CallTo(() => _breedRequestService.GetBreedbyId(A<Guid>._)).Throws<Exception>();

            // Act
            var result = await breedController.GetBreedsById(Guid.NewGuid());

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task GetBreedsById_ReturnsOkResult()
        {
            // Arrange
            var breedController = new BreedController(_mapper, _breedRequestService);

            // Mock the behavior of _breedRequestService.GetBreedbyId to return a list of BreedDto
            var fakeBreedDtoList = new List<BreedDto>
            {
                new BreedDto { Id = Guid.NewGuid(), Name = "Breed1" },
                new BreedDto { Id = Guid.NewGuid(), Name = "Breed2" }
                // Add more BreedDto as needed
            };

            A.CallTo(() => _breedRequestService.GetBreedbyId(A<Guid>._)).Returns(Task.FromResult(fakeBreedDtoList));

            // Act
            var result = await breedController.GetBreedsById(Guid.NewGuid());

            // Assert
            Assert.IsType<OkObjectResult>(result);

            var okResult = (OkObjectResult)result;
            Assert.IsAssignableFrom<List<BreedRequest>>(okResult.Value);
        }

    }
}
