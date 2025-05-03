using AutoMapper;
using Domain.Service.Login.Interfaces;
using Domain.Service.Register.Interfaces;
using Domain.Service.User;
using Domain.Service.User.DTO;
using Domain.Service.User.Interfaces;
using Empetz.Controllers;
using Empetz_API.API.Public.RequestObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Empetz_API.API.Public
{
    
    [ApiController]
    [Authorize(Roles = "PublicUser")]
    public class UserController : BaseApiController<UserController>
    {

        IMapper mapper;
        private IUserService userService1;

        protected IUserService userService { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserController(IMapper _mapper,  IUserService _userService, IHttpContextAccessor httpContextAccessor)
        {
            mapper = _mapper;
            _httpContextAccessor = httpContextAccessor;
            userService = _userService;
        }

       

        [HttpGet("/user/{userId}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {

            UserDto userDto = await userService.GetByIdAsync(userId);
            return Ok(userDto);


        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, "Internal Server Error");
            }
        }

      
        [HttpPut("user/{id}")]
        public async Task<IActionResult> UpdateUser(Guid id,[FromForm] UpdateUserRequest updateUserRequest)
        {
            // Map the request to DTO
            if (id == null)
            {
                return BadRequest("Id is required ");
            }
            
            var updateUserDTO = mapper.Map<UpdateUserDTO>(updateUserRequest);
            updateUserDTO.Id = id;
            byte[] byteArray = userService.ConvertImageToByteArray(updateUserRequest.ImageFile);
            updateUserDTO.Image = byteArray;
            // Call the service
            var result = await userService.UpdateUserAsync(updateUserDTO);

            if (result)
                return Ok("User updated successfully");

            return BadRequest("Failed to update user");
        }

     
        [HttpPut("user")]
        public async Task<IActionResult> Update([FromForm] UpdateUserRequest updateUserRequest)
        {
            // Fetch user ID from token
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Sid);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(); // Return 401 if user ID is not found in the token
            }

            // Map the request to DTO
            var updateUserDTO = mapper.Map<UpdateUserDTO>(updateUserRequest);
            updateUserDTO.Id = Guid.Parse(userId); // Assuming user ID is a GUID
            byte[] byteArray = userService.ConvertImageToByteArray(updateUserRequest.ImageFile);
            updateUserDTO.Image = byteArray;

            // Call the service
            var result = await userService.UpdateUserAsync(updateUserDTO);

            if (result)
                return Ok("User updated successfully");

            return BadRequest("Failed to update user");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (id == null)
            {
                return BadRequest("Id is required ");
            }
            await userService.DeleteUserAsync(id);

            return Ok("User deleted successfully");
        }

       
    }
}
