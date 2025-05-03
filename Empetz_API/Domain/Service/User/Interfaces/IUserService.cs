using Domain.Models;
using Domain.Service.User.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.User.Interfaces
{
    public interface IUserService
    {
        byte[] ConvertImageToByteArray(IFormFile imageFile);
        Task DeleteUserAsync(Guid id);
        Task<UserDto> GetByIdAsync(Guid userId);
        Task<bool> UpdateUserAsync(UpdateUserDTO updateUserDTO);
        //Task<UserDto> UpdateUserProfileAsync(Guid userId, UserDto userRequest);
        //Task UpdateAsync(UserDto userToUpdate);



		string GetUserId();
        Task<IEnumerable<Models.User>> GetAllUsersAsync();
        Task UpdateUserConnectionId(string Phone, string connectionId,bool IsOnline);
        Task<UserDto> GetByPhone(string? phone);
        Task<List<UserDto>> GetOnlineUsers();
		//Task updateNotification(string phone, bool v);
	}
}
