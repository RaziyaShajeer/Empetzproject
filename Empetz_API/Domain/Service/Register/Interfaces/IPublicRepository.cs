using Domain.Models;
using Domain.Service.Register.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Register.Interfaces
{
	public interface IPublicRepository
	{
        Task<bool> IsUserExists(string userName,string phone);
        Task<bool> IsUserExist(string  phone);
        Task<bool> RegisterUser(UserSignUpDto userSignUpDto);
        Task<bool> userRegister(Models.User user);
        Task<bool> IsUserNameExists(string userName);
        Task<bool> IsPhoneExists(string phone);
    }
}
