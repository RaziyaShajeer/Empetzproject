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
    public interface IUserRepository
    {
        Task<Models.User> GetByIdAsync(Guid userId);
        string? CreateToken(Models.User user);

        Task UpdateAsync(Models.User user);
        Task<bool> UpdateUserAsync(Models.User userEntity);
        byte[] ConvertImageToByteArray(IFormFile imageFile);
        Task DeleteUserAsync(Guid id);
        Task<Models.User> GetAllUsers();
        Task<IEnumerable<Models.User>> GetAllUsersAsync();
        Task UpdateUserConnectionId(string Phone, string connectionId, bool IsOnline);
        Task<Models.User> GetByPhoneAsync(string? phone);
        Task<IList<Models.User>> GetOnlineUsersAsync();
 
     
    }
}
