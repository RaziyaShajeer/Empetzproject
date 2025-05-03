using AutoMapper;
using Domain.Service.ContactUs.DTOs;
using Domain.Service.ContactUs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.ContactUs
{
    public class ContactUsService : IContactUsService
    {
        IMapper _mapper;
        private readonly IContactUsRepository _repository;
        public ContactUsService(IContactUsRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<Guid> AddContactUsAsync(ContactUsDTO contactUsDTO)
        {
            return await _repository.AddContactUsAsync(contactUsDTO);
        }
    }
}
