using DAL.Models;
using Domain.Models;
using Domain.Service.Login.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Login
{
    public class LoginRequestRepository : ILoginRequestRepository
    {
        protected readonly EmpetzContext _context;
        public LoginRequestRepository(EmpetzContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Models.User> GetUserByName(string ?userName)
        {
            return await _context.Users.Where(e => e.UserName==userName).FirstOrDefaultAsync();
        }

        public async Task<Models.User> GetUserByPhone(string Phone)
        {
            
				return await _context.Users.Where(e=>e.Phone==Phone).FirstOrDefaultAsync();
			
        }

	}
       
    
}
