using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Login.Interfaces
{
    public interface ILoginRequestRepository
    {
        Task<Models.User> GetUserByName(string userName);
        Task<Models.User> GetUserByPhone(string Phone);
    }
}
