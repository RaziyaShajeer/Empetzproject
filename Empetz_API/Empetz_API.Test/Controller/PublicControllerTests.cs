using AutoMapper;
using Domain.Enums;
using Domain.Service.Category.DTOs;
using Domain.Service.Login.DTOs;
using Domain.Service.Login.Interfaces;
using Domain.Service.MyPets;
using Domain.Service.MyPets.DTOs;
using Domain.Service.Register.DTOs;
using Domain.Service.Register.Interfaces;
using Domain.Service.User.Interfaces;
using Empetz_API.API.Pets;
using Empetz_API.API.Pets.RequestObject;
using Empetz_API.API.Public;
using Empetz_API.API.Public.RequestObject;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empetz_API.Test.Controller
{
	public class PublicControllerTests
	{
		IMapper mapper;
		protected IPublicService publicService { get; set; }
		protected IUserService userService { get; set; }
		public ILoginRequestService loginRequestService { get; set; }
		public PublicControllerTests() {
			mapper = A.Fake<IMapper>();
			userService = A.Fake<IUserService>();
			loginRequestService = A.Fake<ILoginRequestService>();
			publicService=A.Fake<IPublicService>();	
		}
		[Fact]
		public async Task PublicController_userRegitration_ReturnsOK()
		{
			var publicController = new PublicController(mapper, publicService, userService, loginRequestService);
			var usersignup = new UserSignUp
			{
				FirstName = "Fluffy",
				Phone = "9072493551",
				LastName = "Amal",
				Email = "razzrasiya@gmail.com",
				//Image = new FormFile(new MemoryStream(), 0, 0, "image", "image.jpg"),
				//Accountcreated = DateTime.Now				
			};
			A.CallTo(() => mapper.Map<UserRegisterDto>(A<UserSignUp>._))
	.Returns(new UserRegisterDto()); // Adjust as needed
			
			A.CallTo(() => publicService.registerUser(A<UserRegisterDto>._))
			   .Returns(Task.FromResult(true));
			// Act
			var result =await  publicController.userRegitration(usersignup);
			//Assert
			Assert.IsType<OkObjectResult>(result);
			var okResult = (OkObjectResult)result;
			Assert.Equal("User Added SuccessFully", okResult.Value);

		}
		[Fact]
		public async Task PublicController_userRegitrationwithoutPhone_ReturnsOK()
		{
			var publicController = new PublicController(mapper, publicService, userService, loginRequestService);
			var usersignup = new UserSignUp
			{
				FirstName = "Fluffy",
				
				LastName = "Amal",
				Email = "razzrasiya@gmail.com",
				//Image = new FormFile(new MemoryStream(), 0, 0, "image", "image.jpg"),
				//Accountcreated = DateTime.Now
			};
			A.CallTo(() => mapper.Map<UserRegisterDto>(A<UserSignUp>._))
	.Returns(new UserRegisterDto()); // Adjust as needed

			A.CallTo(() => publicService.registerUser(A<UserRegisterDto>._))
			   .Returns(Task.FromResult(true));
			// Act
			var result = await publicController.userRegitration(usersignup);
			//Assert
			Assert.IsType<BadRequestObjectResult>(result);
			var okResult = (BadRequestObjectResult)result;
			Assert.Equal("Phone Number is required", okResult.Value);

		}
		[Fact]
		public async Task PublicController_userRegitrationwithoutFirstName_ReturnsBadRequest()
		{
			var publicController = new PublicController(mapper, publicService, userService, loginRequestService);
			var usersignup = new UserSignUp
			{
				Phone = "9072493551",
				LastName = "Amal",
				Email = "razzrasiya@gmail.com",
				//Image = new FormFile(new MemoryStream(), 0, 0, "image", "image.jpg"),
				//Accountcreated = DateTime.Now
			};
			A.CallTo(() => mapper.Map<UserRegisterDto>(A<UserSignUp>._))
	.Returns(new UserRegisterDto()); // Adjust as needed

			A.CallTo(() => publicService.registerUser(A<UserRegisterDto>._))
			   .Returns(Task.FromResult(true));
			// Act
			var result = await publicController.userRegitration(usersignup);
			//Assert
			Assert.IsType<BadRequestObjectResult>(result);
			var okResult = (BadRequestObjectResult)result;
			Assert.Equal("FirstName is Required", okResult.Value);

		}
		[Fact]
		public async Task PublicController_NumberVerification_ReturnsOK()
		{
			// Arrange
			var publicController = new PublicController(mapper, publicService, userService, loginRequestService);
			var verifyObject = new VerifyObject
			{
				Phone = "9072493551"
			};
			string phone = verifyObject.Phone;

			A.CallTo(() => publicService.IsUserExist(phone))
			   .Returns(Task.FromResult(false)); // User does not exist

			// Act
			var result = await publicController.NumberVerification(verifyObject);

			// Assert
			Assert.IsType<NotFoundObjectResult>(result);
		}
		[Fact]
		public async Task PublicController_NumberVerification_ReturnsBadRequest()
		{
			// Arrange
			var publicController = new PublicController(mapper, publicService, userService, loginRequestService);
			var verifyObject = new VerifyObject
			{
				Phone = "9072493551"
			};
			string phone = verifyObject.Phone;

			A.CallTo(() => publicService.IsUserExist(phone))
			   .Returns(Task.FromResult(true)); // User  exist

			// Act
			var result = await publicController.NumberVerification(verifyObject);

			// Assert
			Assert.IsType<BadRequestObjectResult>(result);
		}
		[Fact]
		public async Task PublicController_Login_ReturnsOk()
		{
			// Arrange
			var publicController = new PublicController(mapper, publicService, userService, loginRequestService);
			var Login = new PublicUserLoginRequest
			{
				Phone = "9072493551"
			};
			string phone = Login.Phone;
			var user=new PublicUserLoginDto
			{
				Id=Guid.NewGuid(),
				FirstName="Rasiya",
				LastName="Shajeer",
				Email="razzrasiya@gmail.com",
				Phone="9072493551",
				Token="your_token_value_here",
				Role= "PublicUser"

			};
			A.CallTo(() => loginRequestService.login(phone))
			   .Returns(Task.FromResult(user)); // User  exist

			// Act
			var result = await publicController.Login(Login);

			// Assert
			Assert.IsType<OkObjectResult>(result);
		}
		[Fact]
		public async Task PublicController_LoginwithInvalid_ReturnsBadRequest()
		{
			var publicController = new PublicController(mapper, publicService, userService, loginRequestService);

			var Login = new PublicUserLoginRequest
			{
				Phone = "90724931"
			};
			var user = new PublicUserLoginDto
			{
				Id = Guid.NewGuid(),
				FirstName = "Rasiya",
				LastName = "Shajeer",
				Email = "razzrasiya@gmail.com",
				Phone = "9072493551",
				Token = "your_token_value_here",
				Role = "PublicUser"

			};
			string phone = Login.Phone;
			A.CallTo(() => loginRequestService.login(phone))
				   .Returns(Task.FromResult<PublicUserLoginDto>(null));
			var result = await publicController.Login(Login);
			// Assert
			Assert.IsType<BadRequestObjectResult>(result);
			var okResult = (BadRequestObjectResult)result;
			Assert.Equal("Login Failed", okResult.Value);

		}
	}

} 
