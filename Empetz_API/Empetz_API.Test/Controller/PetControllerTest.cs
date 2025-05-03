using AutoMapper;
using Domain.Enums;
using Domain.Service.Category;
using Domain.Service.Category.Interfaces;
using Domain.Service.MyPets.DTOs;
using Domain.Service.MyPets.Interfaces;
using Domain.Service.User.Interfaces;
using Empetz_API.API.Pets.RequestObject;
using Empetz_API.API.Pets;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Helpers;
using Domain.Models;
using HireMeNow_WebApi.Extensions;
using Domain.Service.MyPets;
using Microsoft.AspNetCore.Hosting;

namespace Empetz_API.Test.Controller
{
    public class PetControllerTest
    {
        private readonly IUserService userService;
        private readonly IMapper _mapper;
        private readonly IPetservice petservice;
             private readonly IWebHostEnvironment environment;
        public PetControllerTest()
        {
            _mapper = A.Fake<IMapper>();
            petservice = A.Fake<IPetservice>();
            userService = A.Fake<IUserService>();
            environment = A.Fake<IWebHostEnvironment>();
        }


        [Fact]
        public async Task AddPet_ReturnsOkResult()
        {
            // Arrange
            var petController = new PetsController(environment,_mapper, petservice, userService);

            // Populate the properties of PetPostRequest
            var petPostRequest = new PetPostRequest
            {
                Name = "Fluffy",
                BreedId = Guid.NewGuid(),
                Age = 2,
                Gender = "Male",
                Discription = "A cute pet",
                ImageFile = new FormFile(new MemoryStream(), 0, 0, "image", "image.jpg"),
                CategoryId = Guid.NewGuid(),
                LocationId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Vaccinated = true, // Replace with the appropriate value
                Certified = true, // Replace with the appropriate value
                Price = 100
            };

            // Mock the necessary dependencies
            A.CallTo(() => _mapper.Map<PetPostDto>(A<PetPostRequest>._))
                .Returns(new PetPostDto()); // Adjust as needed
            A.CallTo(() => petservice.AddPetAsync(A<PetPostDto>._))
                .Returns(Task.FromResult(true));

            // Act
            var result = await petController.AddPet(petPostRequest);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal("Pets Added Successfully", okResult.Value);
        }

        [Fact]
        public async Task AddPet_ReturnsBadRequestResult()
        {
            // Arrange
            var petController = new PetsController(environment, _mapper, petservice, userService);

            // Populate the properties of PetPostRequest
            var petPostRequest = new PetPostRequest
            {
                Name = "Fluffy",
                BreedId = Guid.NewGuid(),
                Age = 2,
                Gender = "Male",
                Discription = "A cute pet",
                ImageFile = new FormFile(new MemoryStream(), 0, 0, "image", "image.jpg"),
                CategoryId = Guid.NewGuid(),
                LocationId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Vaccinated = true, // Replace with the appropriate value
                Certified = true, // Replace with the appropriate value
                Price = 100
            };

            // Mock the necessary dependencies
            A.CallTo(() => _mapper.Map<PetPostDto>(A<PetPostRequest>._))
                .Returns(new PetPostDto()); // Adjust as needed
            A.CallTo(() => petservice.AddPetAsync(A<PetPostDto>._))
                .Returns(Task.FromResult(false));

            // Act
            var result = await petController.AddPet(petPostRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.Equal("Try again", badRequestResult.Value);
        }
        [Fact]
        public async Task GetUserPostedPets_ReturnsOkResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var petService = A.Fake<IPetservice>();
            var petController = new PetsController(environment, A.Fake<IMapper>(), petService, A.Fake<IUserService>());

            // Create a list of PostedPetsDTO for testing
            var postedPets = new List<PostedPetsDTO>
            {
                new PostedPetsDTO
                {
                    Name = "Fluffy",
        Age = 2,
        Gender = "Male",
        Discription = "A cute pet",
        Image = new byte[] {  },
        CategoryName = "Category1",
        BreedName = "Breed1",
        UserFirstName = "John",
        LocationName = "Location1",
        TimeAgo = "2 hours ago",
        PetPosted = DateTime.UtcNow.AddHours(-2)
                },
                // Add more PostedPetsDTO as needed
            };

            // Mock the behavior of petservice.GetUserPostedPetsAsync
            A.CallTo(() => petService.GetUserPostedPetsAsync(userId)).Returns(postedPets);

            // Act
            var result = await petController.GetUserPostedPets(userId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.IsAssignableFrom<List<PostedPetsDTO>>(okResult.Value);
        }

        [Fact]
        public async Task GetUserPostedPets_ReturnsBadRequestResultOnException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var petService = A.Fake<IPetservice>();
            var petController = new PetsController(environment, A.Fake<IMapper>(), petService, A.Fake<IUserService>());

            // Mock the behavior of petservice.GetUserPostedPetsAsync to throw an exception
            A.CallTo(() => petService.GetUserPostedPetsAsync(userId)).Throws(new Exception("Simulated exception"));

            // Act
            var result = await petController.GetUserPostedPets(userId);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async Task GetPetById_ReturnsOkResult()
        {
            // Arrange
            var petId = Guid.NewGuid();
            var petService = A.Fake<IPetservice>();
            var mapper = A.Fake<IMapper>();
            var petController = new PetsController(environment, mapper, petService, A.Fake<IUserService>());

            // Create a sample Pet object for testing
            var samplePet = new Pet
            {
                Id = petId,
                Name = "Fluffy",
                // ... Populate other properties as needed
            };

            // Mock the behavior of petservice.getPet
            A.CallTo(() => petService.getPet(petId)).Returns(samplePet);

            // Mock the behavior of mapper.Map
            A.CallTo(() => mapper.Map<PetList>(A<Pet>._)).Returns(new PetList
            {
                Id = petId,
                Name = "Fluffy",
                // ... Populate other properties as needed
            });

            // Act
            var result = await petController.getPetsById(petId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.IsAssignableFrom<PetList>(okResult.Value);
            var selectedPet = (PetList)okResult.Value;
            Assert.Equal(petId, selectedPet.Id);
            // Assert other properties as needed
        }

        [Fact]
        public async Task GetPetById_ReturnsNotFoundResult()
        {
            // Arrange
            var petId = Guid.NewGuid();
            var petService = A.Fake<IPetservice>();
            var mapper = A.Fake<IMapper>();
            var petController = new PetsController(environment, mapper, petService, A.Fake<IUserService>());

            // Mock the behavior of petservice.getPet to return null (not found)
            A.CallTo(() => petService.getPet(petId)).Returns(Task.FromResult<Pet>(null));

            // Act
            var result = await petController.getPetsById(petId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void SearchPets_ReturnsOkResult()
        {
            // Arrange
            var searchTerm = "Fluffy";
            var petService = A.Fake<IPetservice>();
            var mapper = A.Fake<IMapper>();
            var petController = new PetsController(environment, mapper, petService, A.Fake<IUserService>());

            // Create a list of Pet objects for testing
            var matchingPets = new List<Pet>
            {
                new Pet
                {
                    Id = Guid.NewGuid(),
    Name = "Fluffy",
    Breed = new Breed { Id = Guid.NewGuid(), Name = "Breed1" },
    Age = 2,
    Gender = "Male",
    Image = new byte[] { 0x01, 0x02, 0x03 }, // Example byte array for an image
    Category = new PetsCategory { Id = Guid.NewGuid(), Name = "Category1" },
    UserId = Guid.NewGuid(),
    Location = new Location { Id = Guid.NewGuid(), Name = "Location1" },
    Vaccinated = true,
    PetPosted = DateTime.UtcNow.AddHours(-2),
    Certified = true,
    Price = 100,
                    
                },
                // Add more Pet objects as needed
            };

            // Mock the behavior of petservice.SearchPets
            A.CallTo(() => petService.SearchPets(searchTerm)).Returns(matchingPets);

            // Act
            var result = petController.SearchPets(searchTerm);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.IsAssignableFrom<List<Pet>>(okResult.Value);
        }
        [Fact]
        public async Task SearchPetsByBreedName_ReturnsOkResult()
        {
            // Arrange
            var breedName = "Breed1"; // Replace with the desired breed name
            var petService = A.Fake<IPetservice>();
            var petController = new PetsController(environment, A.Fake<IMapper>(), petService, A.Fake<IUserService>());

            // Create a list of pets for testing
            var pets = new List<Pet>
    {
        new Pet
        {
            Id = Guid.NewGuid(),
            Name = "Fluffy",
            Breed = new Breed { Id = Guid.NewGuid(), Name = "Breed1" },
            Age = 2,
            Gender = "Male",
            Image = Encoding.UTF8.GetBytes("SampleImage"), // You can replace this with your actual image data
            Category = new PetsCategory { Id = Guid.NewGuid(), Name = "Category1" },
            Location = new Location { Id = Guid.NewGuid(), Name = "Location1" },
            Vaccinated = true,
            PetPosted = DateTime.UtcNow.AddHours(-2),
            Certified = true,
            Price = 100,
            // ... Add other properties as needed
        },
        // Add more pets as needed
    };

            // Mock the behavior of petservice.PetsByBreedName
            A.CallTo(() => petService.PetsByBreedName(breedName)).Returns(pets);

            // Act
            var result = petController.SearchPetsByBreedName(breedName);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.IsAssignableFrom<List<Pet>>(okResult.Value);
        }

        [Fact]
        public async Task UpdatePet_WithImage_ReturnsOkResult()
        {
            // Arrange
            var petId = Guid.NewGuid();
            var petUpdateRequest = new PetUpdateRequest
            {
                Name = "UpdatedFluffy",
                BreedId = Guid.NewGuid(),
                Age = 3,
                Gender = "Female",
             
                ImageFile = new FormFile(new MemoryStream(), 0, 0, "image", "updated_image.jpg"),
                CategoryId = Guid.NewGuid(),
                LocationId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
              // Replace with the appropriate value
                Certified = Certified.False, // Replace with the appropriate value
                Price = 150
            };

            var userService = A.Fake<IUserService>();
            var mapper = A.Fake<IMapper>();
            var petService = A.Fake<IPetservice>();
            var petController = new PetsController(environment, mapper, petService, userService);

            A.CallTo(() => userService.ConvertImageToByteArray(A<IFormFile>._))
                .Returns(Encoding.UTF8.GetBytes("SampleImage")); // Replace with your actual image data

            A.CallTo(() => mapper.Map<Pet>(A<PetUpdateRequest>._))
                .Returns(new Pet { /* Map properties accordingly */ });

            A.CallTo(() => petService.updatePet(A<Pet>._, petId))
                .Returns(Task.FromResult(true));

            // Act
            var result = await petController.updatePet(petUpdateRequest, petId);

            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal("Pets Updated SuccessFully", okResult.Value.GetType().GetProperty("Message")?.GetValue(okResult.Value));
        }

        [Fact]
        public async Task UpdatePet_WithoutImage_ReturnsOkResult()
        {
            // Arrange
            var petId = Guid.NewGuid();
            var petUpdateRequest = new PetUpdateRequest
            {
                // Populate the properties of PetUpdateRequest as needed
                Name = "UpdatedFluffy",
                BreedId = Guid.NewGuid(),
                Age = 3,
                Gender = "Female",
                Discription = "An updated cute pet",
                ImageFile = new FormFile(new MemoryStream(), 0, 0, "image", "updated_image.jpg"),
                CategoryId = Guid.NewGuid(),
                LocationId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
            //    Vaccinated = Vaccinated.False, // Replace with the appropriate value
                Certified = Certified.False, // Replace with the appropriate value
                Price = 150
            };

            var userService = A.Fake<IUserService>();
            var mapper = A.Fake<IMapper>();
            var petService = A.Fake<IPetservice>();
            var petController = new PetsController(environment, mapper, petService, userService);

            A.CallTo(() => mapper.Map<Pet>(A<PetUpdateRequest>._))
                .Returns(new Pet { /* Map properties accordingly */ });

            A.CallTo(() => petService.updatePet(A<Pet>._, petId))
                .Returns(Task.FromResult(true));

            // Act
            var result = await petController.updatePet(petUpdateRequest, petId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.Equal("Pets Updated SuccessFully", okResult.Value.GetType().GetProperty("Message")?.GetValue(okResult.Value));
        }

        [Fact]
        public async Task UpdatePet_Fails_ReturnsBadRequestResult()
        {
            // Arrange
            var petId = Guid.NewGuid();
            var petUpdateRequest = new PetUpdateRequest
            {
                Name = "UpdatedFluffy",
                BreedId = Guid.NewGuid(),
                Age = 3,
                Gender = "Female",
                Discription = "An updated cute pet",
                ImageFile = new FormFile(new MemoryStream(), 0, 0, "image", "updated_image.jpg"),
                CategoryId = Guid.NewGuid(),
                LocationId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
               // Replace with the appropriate value
                Certified = Certified.False, // Replace with the appropriate value
                Price = 150,
            };

            var userService = A.Fake<IUserService>();
            var mapper = A.Fake<IMapper>();
            var petService = A.Fake<IPetservice>();
            var petController = new PetsController(environment, mapper, petService, userService);

            A.CallTo(() => userService.ConvertImageToByteArray(A<IFormFile>._))
                .Returns(Encoding.UTF8.GetBytes("SampleImage")); // Replace with your actual image data

            A.CallTo(() => mapper.Map<Pet>(A<PetUpdateRequest>._))
                .Returns(new Pet { /* Map properties accordingly */ });

            A.CallTo(() => petService.updatePet(A<Pet>._, petId))
                .Returns(Task.FromResult(false));

            // Act
            var result = await petController.updatePet(petUpdateRequest, petId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.Equal(false, badRequestResult.Value);
        }


    }
}

