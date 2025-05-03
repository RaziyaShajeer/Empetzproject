using Domain.Models;
using Domain.Service.Register.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Register.Interfaces
{
	public interface IPublicService
	{
		Task<bool> registerUser(UserRegisterDto registerdto);
		Task<bool> IsUserExist(string phone);
        Task<bool> IsUserNameExists(string userName);
        Task<bool> IsPhoneExists(string phone);
        Task<bool> RegisterUser(UserSignUpDto userSignUpDto);
    }
}
