using Domain.Service.ContactUs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.ContactUs.Interfaces
{
    public interface IContactUsRepository
    {
        Task<Guid> AddContactUsAsync(ContactUsDTO contactUsDTO);
    }
}
