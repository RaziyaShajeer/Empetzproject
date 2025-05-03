using AutoMapper;
using Domain.Service.Login.DTOs;
using Domain.Service.Login.Interfaces;
using Domain.Service.User.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Login
{
    public class LoginRequestService : ILoginRequestService
    {
        ILoginRequestRepository PublicUserRepository;
        IUserRepository UserRepository;
        IMapper mapper;
        public LoginRequestService(ILoginRequestRepository _PublicUserRepository, IMapper _mapper, IUserRepository _UserRepository)
        {
            PublicUserRepository = _PublicUserRepository;
                mapper = _mapper;
            UserRepository = _UserRepository;
        }
        public async Task<PublicUserLoginDto> login( string Phone)
        {
            var user = await PublicUserRepository.GetUserByPhone(Phone);
            if (user == null)
            {
                return null;
            }
            else
            {
                if ((Phone == user.Phone))
                {
                    var userReturn = mapper.Map<PublicUserLoginDto>(user);
                    userReturn.Token = UserRepository.CreateToken(user);
                    return userReturn;
                }
                return null;
            }
        }

 public async Task<UserLoginDto> login(string userName, string password)
        {
            var user = await PublicUserRepository.GetUserByName(userName);
            if (user == null)
            {
                return null;
            }
            else
            {
                var hashedPassword = HashPassword(password);
                if ((userName==user.UserName&& hashedPassword == user.Password))
                {
                    var userReturn = mapper.Map<UserLoginDto>(user);
                    userReturn.Token = UserRepository.CreateToken(user);
                    return userReturn;
                }
                return null;
            }
        }
        private string HashPassword(string password)
        {
            // Implement a secure password hashing mechanism here
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
