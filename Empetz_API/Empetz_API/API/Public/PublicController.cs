using AutoMapper;
using Domain.Service.Category.DTOs;
using Domain.Service.Category;
using Domain.Service.Login.Interfaces;
using Domain.Service.Register.DTOs;
using Domain.Service.Register.Interfaces;
using Domain.Service.User.Interfaces;
using Empetz.Controllers;
using Empetz_API.API.Category.RequestObject;
using Empetz_API.API.Public.RequestObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Domain.Service.User.DTO;
using System.Runtime.Intrinsics.X86;
using Domain.Service.Category.Interfaces;
using Domain.Models;
using Newtonsoft.Json;
using System.Numerics;
using Microsoft.AspNetCore.Authorization;
using Domain.Service.Register;
using Domain.Service.Login.DTOs;

namespace Empetz_API.API.Public
{
	[ApiController]
   
    public class PublicController : BaseApiController<PublicController>		
	{
		IMapper mapper;
		protected  IPublicService publicService { get; set; }
        protected IUserService userService { get; set; }
        public ILoginRequestService loginRequestService { get; set; }
        public PublicController(IMapper _mapper, IPublicService _publicService, IUserService _userService, ILoginRequestService _loginRequestService)
		{
			mapper = _mapper;
			publicService = _publicService;
			loginRequestService = _loginRequestService;
			userService = _userService;
		}

		[HttpPost]
		[Route("user/register")]
		public async Task<IActionResult> userRegitration(UserSignUp data)
        {
            try
			{
                string phone = data.Phone;
                var result = await publicService.IsUserExist(phone);
				if (result == true)
				{
					return Ok("user Already Exist");
				}
				else
				{ 

					UserRegisterDto userSignUp = mapper.Map<UserRegisterDto>(data);
					var user = await publicService.registerUser(userSignUp);
					if (user == true)
					{
						return Ok("User Added SuccessFully");
					}
					else
					{
						return BadRequest("User Not Added");
					}
				}
							
			}
			catch(Exception ex)
			{
				throw ex;
			}
		
		}
       

        [HttpPost]
		[Route("user/number-verification")]
		public async Task<IActionResult> NumberVerification(VerifyObject Phone)
		{
			try
			{
				string phone = Phone.Phone;
				var result = await publicService.IsUserExist(phone);
				if (result == true)
				{
                  
                    return Ok("User Exist");
				}
				else
				{
                   
                    return Ok("User Not found");
				}
			}
			catch(Exception ex) { 
				throw ex;
			}

			
		}

        [HttpPost("user/registration")]
        public async Task<IActionResult> UserRegistration(UserSignUpRequest data)
        {
            try
            {
                string phone = data.Phone;
                string userName = data.UserName;

                // Check if the username or phone number already exists
                var isUserNameTaken = await publicService.IsUserNameExists(userName);
                var isPhoneTaken = await publicService.IsPhoneExists(phone);

                if (isUserNameTaken)
                {
                    return Ok("UserName Already Exists");
                }
                else if (isPhoneTaken)
                {
                    return Ok("Phone Number Already Taken");
                }
                else
                {
                    UserSignUpDto userSignUpDto = mapper.Map<UserSignUpDto>(data);
                    var userRegistered = await publicService.RegisterUser(userSignUpDto);
                    if (userRegistered)
                    {
                        return Ok("User Added Successfully");
                    }
                    else
                    {
                        return BadRequest("User Not Added");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }







        [HttpPost]
        [Route("user/login_by_phoneno")]
        public async Task<IActionResult> Login(PublicUserLoginRequest logdata)
        {
            //var user = _mapper.Map<User>(userDto);
            var user = await loginRequestService.login(logdata.Phone);

            if (user == null)
            {
                string jsonData = JsonConvert.SerializeObject(new { Text = "Invalid Creadentials" });
                return BadRequest(jsonData);
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("user/login")]
        public async Task<IActionResult> userLogin(UserLoginRequest logdata)
        {
            //var user = _mapper.Map<User>(userDto);
            var user = await loginRequestService.login(logdata.UserName, logdata.password);

            if (user == null)
            {
                string jsonData = JsonConvert.SerializeObject(new { Text = "Invalid Creadentials" });
                return BadRequest(jsonData);
            }
            return Ok(user);
        }

    }


}

