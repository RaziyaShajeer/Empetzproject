using AutoMapper;
using Domain.Models;
using Domain.Service.User.DTO;
using Domain.Service.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.User
{
	[Authorize()]
	public class UserService : IUserService
    {

        protected IUserRepository _repository;
        protected IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public UserService(IUserRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
			_httpContextAccessor = httpContextAccessor;
			_repository = repository;
            _mapper = mapper;
        }


        public async Task<UserDto> GetByIdAsync(Guid userId)
        {
            Models.User user = await _repository.GetByIdAsync(userId);
            return _mapper.Map<UserDto>(user);
        }
		public string GetUserId()
		{
			var result = string.Empty;
			if (_httpContextAccessor.HttpContext != null)
			{
				result = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value;
			}
			return result;
		}

        public byte[] ConvertImageToByteArray(IFormFile imageFile)
        {
            return _repository.ConvertImageToByteArray(imageFile);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _repository.DeleteUserAsync(id);
        }

        public async Task<bool> UpdateUserAsync(UpdateUserDTO updateUserDTO)
        {
            var userEntity = _mapper.Map<Models.User>(updateUserDTO);

            // Call repository for update
            var result = await _repository.UpdateUserAsync(userEntity);

            return result;
        }

        public async Task<IEnumerable<Models.User>> GetAllUsersAsync()
        {
            return await _repository.GetAllUsersAsync();
        }


        //Updating connection ID
        public async Task UpdateUserConnectionId(string Phone, string connectionId, bool IsOnline)
        {
            await _repository.UpdateUserConnectionId(Phone, connectionId, IsOnline);
        }

        public async Task<UserDto> GetByPhone(string? phone)
        {
            Models.User user = await _repository.GetByPhoneAsync(phone);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<List<UserDto>> GetOnlineUsers()
        {
            IList<Models.User> user = await _repository.GetOnlineUsersAsync();
            return _mapper.Map<List<UserDto>>(user);
        }

		//public async Task updateNotification(string phone, bool v)
		//{
		//	Models.User user = await _repository.GetByPhoneAsync(phone);
		//	user.IsAnyNotifications=v;
  //          await _repository.UpdateAsync(user);
		//}
	}
}
