using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Service.Login.DTOs;

namespace Domain.Service.Login.Interfaces
{
    public interface ILoginRequestService
    {
 
        Task<PublicUserLoginDto> login(string Phone);
        Task<UserLoginDto> login(string userName, string password);
    }
}
