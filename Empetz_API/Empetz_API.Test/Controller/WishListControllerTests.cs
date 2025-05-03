using AutoMapper;
using Domain.Enums;
using Domain.Models;
using Domain.Service.Category.DTOs;
using Domain.Service.MyPets;
using Domain.Service.MyPets.DTOs;
using Domain.Service.MyPets.Interfaces;
using Domain.Service.Register;
using Domain.Service.User;
using Domain.Service.User.DTO;
using Domain.Service.User.Interfaces;
using Domain.Service.WishLIst.DTOs;
using Domain.Service.WishLIst.Interfaces;
using Empetz_API.API.Category.RequestObject;
using Empetz_API.API.Pets.RequestObject;
using Empetz_API.API.Public;
using Empetz_API.API.WishList;
using Empetz_API.API.WishList.RequestObject;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Empetz_API.Test.Controller
{
	public class WishListControllerTests
	{
		private readonly IWishListService _wishListService;
		IMapper _mapper;
		public WishListControllerTests()
		{
			_mapper = A.Fake<IMapper>();
			_wishListService = A.Fake<IWishListService>();
		}
		[Fact]
		public async Task WishListController_AddToWishList_ReturnsOk()
		{

			var controller = new WishListController(_wishListService, _mapper);
			var WishListRequestObject = new WishListRequestObject
			{
				Id=Guid.NewGuid(),
				User=Guid.NewGuid(),	
				Pet=Guid.NewGuid(),
				// Set other properties as needed for your test
			};
			A.CallTo(() => _wishListService.AddToWishList(WishListRequestObject.User,WishListRequestObject.Pet))
			   .Returns(Task.FromResult(true));
			var result = await controller.AddToWishList(WishListRequestObject);
			// Assert
			Assert.IsType<OkObjectResult>(result);
			var okResult = (OkObjectResult)result;
			Assert.Equal("Pet added to favorites successfully", okResult.Value);
		}
		[Fact]
		public async Task WishListController_AddToWishListWithInvalidUserId_ReturnsBadRequest()
		{

			var controller = new WishListController(_wishListService, _mapper);
			var WishListRequestObject = new WishListRequestObject
			{
				Id = Guid.NewGuid(),
				User =Guid.Empty,
				Pet = Guid.NewGuid(),
				// Set other properties as needed for your test
			};
			A.CallTo(() => _wishListService.AddToWishList(WishListRequestObject.User, WishListRequestObject.Pet))
			   .Returns(Task.FromResult(false));
			var result = await controller.AddToWishList(WishListRequestObject);
			// Assert
			Assert.IsType<BadRequestObjectResult>(result);
			var okResult = (BadRequestObjectResult)result;
			Assert.Equal("Failed to add pet to favorites", okResult.Value);
		}
		[Fact]
		public async Task WishListController_AddToWishListWithInvalidPetId_ReturnsBadRequest()
		{

			var controller = new WishListController(_wishListService, _mapper);
			var WishListRequestObject = new WishListRequestObject
			{
				Id = Guid.NewGuid(),
				User = Guid.NewGuid(),
				Pet = Guid.Empty
				// Set other properties as needed for your test
			};
			A.CallTo(() => _wishListService.AddToWishList(WishListRequestObject.User, WishListRequestObject.Pet))
			   .Returns(Task.FromResult(false));
			var result = await controller.AddToWishList(WishListRequestObject);
			// Assert
			Assert.IsType<BadRequestObjectResult>(result);
			var okResult = (BadRequestObjectResult)result;
			Assert.Equal("Failed to add pet to favorites", okResult.Value);
		}
		[Fact]
		public async Task WishListController_GetWishListbyuserId_ReturnsOk()
		{
			var wishlist = new List<GetWishListDTO>
			{
				new GetWishListDTO
				{
					id=Guid.NewGuid(),
					userid=Guid.NewGuid(),
					petId=Guid.NewGuid(),
					Name="Tom",
					BreedName="German Shepherd",
					Age=2,
					Gender="Female",
					Discription="Good",
					Image=new byte[] {  },

					CategoryName="Dog"


				},
		              // Add more PostedPetsDTO as needed
		          };
			var controller = new WishListController(_wishListService, _mapper);
			Guid userid = Guid.NewGuid();
			A.CallTo(() => _wishListService.GetWishListByUserIdAsync(userid)).Returns(wishlist);

			var result = await controller.GetWishListByserId(userid);

            // Assert
            Assert.IsType<OkObjectResult>(result);
			var okResult = (OkObjectResult)result;
			Assert.IsAssignableFrom<List<GetWishListDTO>>(okResult.Value);
		}
		[Fact]
		public async Task WishListController_GetWishListbyuserId_ReturnsBadResult()
		{

			var controller = new WishListController(_wishListService, _mapper);
			var userid = Guid.NewGuid();
			A.CallTo(() => _wishListService.GetWishListByUserIdAsync(userid)).Throws(new Exception("Simulated exception"));


			var result = await controller.GetWishListByserId(userid);

			// Assert
			Assert.IsType<BadRequestObjectResult>(result);
			var okResult = (BadRequestObjectResult)result;
			Assert.Equal("Invalid UserId", okResult.Value);
		}
		[Fact]
		public async Task WishListController_GetWishListbyInvaliduserId_ReturnsBadResult()
		{

			var controller = new WishListController(_wishListService, _mapper);
			Guid userid = Guid.Empty;
			A.CallTo(() => _wishListService.GetWishListByUserIdAsync(userid)).Throws(new Exception("Simulated exception"));


			var result = await controller.GetWishListByserId(userid);

			// Assert
			Assert.IsType<BadRequestObjectResult>(result);
			var okResult = (BadRequestObjectResult)result;
			Assert.Equal("Invalid UserId", okResult.Value);
		}
		[Fact]
		public async Task WishListController_GetWishListById_ReturnsOk()
		{

			var controller = new WishListController(_wishListService, _mapper);
			Guid userid = Guid.NewGuid();	
			Guid WislistId=Guid.NewGuid();
			GetWishListDTO wishlist = new GetWishListDTO
			{
				id = Guid.NewGuid(),
				userid = Guid.NewGuid(),
				petId = Guid.NewGuid(),
				Name = "Tom",
				BreedName = "German Shepherd",
				Age = 2,
				Gender = "Female",
				Discription = "Good",
				Image = new byte[] { },

				CategoryName = "Dog"


			};
			A.CallTo(() => _wishListService.GetWishListByIdAsync(userid, WislistId)).Returns(wishlist);


			var result = await controller.GetWishListById(userid,WislistId);

			// Assert
			result.Should().BeOfType<OkObjectResult>()
			  .Which.Value.Should().BeEquivalentTo(wishlist);
		}
		[Fact]
		public async Task WishListController_GetWishListById_ReturnsBadRequest()
		{

			var controller = new WishListController(_wishListService, _mapper);
			Guid userid = Guid.Empty;
			Guid WislistId = Guid.Empty;
			
			A.CallTo(() => _wishListService.GetWishListByIdAsync(userid, WislistId)).Throws(new Exception("Simulated error"));


			var result = await controller.GetWishListById(userid, WislistId);

			// Assert
			Assert.IsType<BadRequestObjectResult>(result);
			var okResult = (BadRequestObjectResult)result;
			Assert.Equal("No Wishlist in this id", okResult.Value);
		}
		[Fact]
		public async Task WishListController_RemovePetFromWishList_ReturnsOK()
		{

			var controller = new WishListController(_wishListService, _mapper);
			Guid userid = Guid.NewGuid();
			Guid petId = Guid.NewGuid();

			A.CallTo(() => _wishListService.RemovePetFromWishListAsync(userid, petId)).Returns(Task.FromResult(true));


			var result = await controller.RemovePetFromWishList(userid, petId);

			// Assert
			Assert.IsType<OkObjectResult>(result);
			var okResult = (OkObjectResult)result;
			Assert.Equal("Pet removed from wishlist successfully", okResult.Value);
		}
		[Fact]
		public async Task WishListController_RemovePetFromWishList_ReturnsBadRequest()
		{

			var controller = new WishListController(_wishListService, _mapper);
			Guid userid = Guid.Empty;
			Guid petId = Guid.Empty;

			A.CallTo(() => _wishListService.RemovePetFromWishListAsync(userid, petId)).Returns(Task.FromResult(false));


			var result = await controller.RemovePetFromWishList(userid, petId);

			// Assert
			Assert.IsType<NotFoundObjectResult>(result);
			var okResult = (NotFoundObjectResult)result;
			Assert.Equal("Pet not found in the wishlist", okResult.Value);
		}
		[Fact]
		public async Task WishListController_RemovePetFromWishList_ReturnsBadRequestResultOnException()
		{

			var controller = new WishListController(_wishListService, _mapper);
			Guid userid = Guid.Empty;
			Guid petId = Guid.Empty;

			A.CallTo(() => _wishListService.RemovePetFromWishListAsync(userid, petId)).Throws(new Exception("Simulated exception"));


			var result = await controller.RemovePetFromWishList(userid, petId);

			// Assert
			Assert.IsType<BadRequestResult>(result);
		}

	}
}
