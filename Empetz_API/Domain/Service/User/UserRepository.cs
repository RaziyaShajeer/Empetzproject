using AutoMapper;
using DAL.Models;
using Domain.Models;
using Domain.Service.PetsBreed.DTOs;
using Domain.Service.User.DTO;
using Domain.Service.User.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.Service.User
{
    public class UserRepository : IUserRepository
    {
        protected readonly EmpetzContext _context;
        IMapper mapper;
        private readonly IConfiguration _configuration;

        public UserRepository(EmpetzContext dbContext, IMapper _mapper, IConfiguration configuration)
        {
            _context = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }
        public string? CreateToken(Models.User user)
        {
           try
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "User object cannot be null.");
        }

        string tokenSecret = _configuration.GetSection("AuthSettings:Token").Value;

        if (string.IsNullOrEmpty(tokenSecret))
        {
            throw new InvalidOperationException("Token secret is missing or empty in configuration.");
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSecret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.FirstName),
            new Claim(ClaimTypes.MobilePhone, user.Phone),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim(ClaimTypes.Sid, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
    catch (Exception ex)
    {
        throw ex;
    }

            
        }


        public async Task<Models.User> GetByIdAsync(Guid userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task UpdateAsync(Models.User user)
        {
            
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateUserAsync(Models.User userEntity)
        {
            var userAccountCreationDate = _context.Users
                   .Where(user => user.Id == userEntity.Id)
                   .Select(user => user.Accountcreated)
                   .FirstOrDefault();

            var Role = _context.Users
                   .Where(user => user.Id == userEntity.Id)
                   .Select(user => user.Role)
                   .FirstOrDefault();
            userEntity.Role = Role;
            userEntity.Accountcreated = userAccountCreationDate;
            _context.Users.Update(userEntity);
            await _context.SaveChangesAsync();
            //_context.Entry(userEntity).State = EntityState.Modified;
            // await _context.SaveChangesAsync();
            return true; //
        }

        public byte[] ConvertImageToByteArray(IFormFile imageFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                imageFile.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        //public async Task DeleteUserAsync(Guid id)
        //{
        //    var user = await _context.PublicUsers.FindAsync(id);
        //    if (user != null)
        //    {
        //        _context.PublicUsers.Remove(user);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user != null)
            {
                var associatedPets = _context.Pets.Where(p => p.UserId == userId);
                _context.Pets.RemoveRange(associatedPets);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
     
        }

        public Task<Models.User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Models.User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task UpdateUserConnectionId(string Phone, string connectionId, bool IsOnline)
        {
            try
            {
                var usertoupdate = await _context.Users.Where(e => e.Phone==Phone).FirstOrDefaultAsync();
                if (usertoupdate != null)
                {
                    usertoupdate.ConnectionId= connectionId;
                    usertoupdate.IsOnline= IsOnline;
                    await _context.SaveChangesAsync();
                }
            }
            catch(Exception ex) { 
            
            }

        }

        public async Task<Models.User> GetByPhoneAsync(string? phone)
        {
            var userByPhone = await _context.Users.Where(e => e.Phone==phone).FirstOrDefaultAsync();
            return userByPhone;
        }

        public async Task<IList<Models.User>> GetOnlineUsersAsync()
        {
            return await _context.Users.Where(e=>e.IsOnline==true).ToListAsync();

        }

    


    }
}
