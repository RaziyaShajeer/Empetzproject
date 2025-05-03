using DAL.Models;
using Domain.Service.ContactUs.DTOs;
using Domain.Service.ContactUs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.ContactUs
{
    public class ContactRepository : IContactUsRepository
    {

        private readonly EmpetzContext _context;

        public ContactRepository(EmpetzContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Guid> AddContactUsAsync(ContactUsDTO contactUsDTO)
        {
            var contactUs = new Domain.Models.ContactUs
            {
                Email = contactUsDTO.Email,
                Message = contactUsDTO.Message,
                UserId = contactUsDTO.UserId
                // Add other properties as needed
            };

            _context.ContactUs.Add(contactUs);
            await _context.SaveChangesAsync();

            return contactUs.Id;
        }
    }
}
