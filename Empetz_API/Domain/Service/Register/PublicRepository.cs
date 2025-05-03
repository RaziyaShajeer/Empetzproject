using Domain.Models;
using Domain.Enums;
using Domain.Service.Register.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Role = Domain.Enums.Role;
using Domain.Service.Register.DTOs;
using System.Security.Cryptography;

namespace Domain.Service.Register
{
    public class PublicRepository : IPublicRepository
	{
		protected readonly EmpetzContext empetzContext;

		public PublicRepository(EmpetzContext _empetzContext)
		{
			empetzContext = _empetzContext;
		}
		public async Task<bool> userRegister(Models.User user)
		{
			user.Role = Role.PublicUser;
			user.Accountcreated = DateTime.Now;
			await empetzContext.Users.AddAsync(user);
			empetzContext.SaveChanges();
			return true;

		}
		public async Task<bool> IsUserExist(string phone)
		{
			var exist =		await empetzContext.Users.Where(e=>e.Phone==phone).FirstOrDefaultAsync();
			if(exist != null) {
				return true;
			}
			else
			{
				return false;
			}
		

		}


        public async Task<bool> IsUserExists(string userName,string phone)
        {
            return await empetzContext.Users.AnyAsync(u =>u.Phone==phone||u.UserName == userName);
        }

        public async Task<bool> RegisterUser(UserSignUpDto userSignUpDto)
        {

            // 1. Map the UserSignUpDto to User entity
            var user = new Models.User
            {
                Id = Guid.NewGuid(),
                FirstName = userSignUpDto.FirstName,// Generate a new GUID for the user ID
                UserName = userSignUpDto.UserName,
                Phone = userSignUpDto.Phone,
                Email = userSignUpDto.Email,
                // 2. Hash the password before saving
                Password = HashPassword(userSignUpDto.password),

                // 3. Set additional properties
                Accountcreated = DateTime.Now
            };

            try
            {
                user.Role = Role.PublicUser;
                user.Status =Status.Active; 
                // 4. Add the user to the database
                empetzContext.Users.Add(user);

                // 5. Save the changes and check if the operation was successful
                return await empetzContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                // Log the exception (you might use a logging framework here)
                Console.WriteLine($"An error occurred while registering the user: {ex.Message}");
                return false; // Registration failed
            }
        }
        private string HashPassword(string password)
        {
            // Implement a secure password hashing mechanism here
            // For example, using a cryptographic hash function with salt
            // Consider using a library like BCrypt, PBKDF2, or ASP.NET Core Identity
            // Below is a simple placeholder example:

            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
        public async Task<bool> IsUserNameExists(string userName)
        {
            return await empetzContext.Users.AnyAsync(u => u.UserName == userName);
        }

        public async Task<bool> IsPhoneExists(string phone)
        {
            return await empetzContext.Users.AnyAsync(u => u.Phone == phone);
        }
    }
}
