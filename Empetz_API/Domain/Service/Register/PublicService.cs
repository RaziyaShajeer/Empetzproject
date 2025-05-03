using AutoMapper;
using Domain.Models;
using Domain.Service.Register.DTOs;
using Domain.Service.Register.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Register
{
    public class PublicService : IPublicService
	{
		protected readonly IPublicRepository publicRepository;
		protected readonly IMapper mapper;	

		public PublicService(IPublicRepository _publicRepository,IMapper _mapper)
		{
		publicRepository = _publicRepository;
			mapper = _mapper;
		}
		public async Task<bool> registerUser(UserRegisterDto registerdto)
		{
            Models.User publicUser= mapper.Map<Models.User>(registerdto);
		return 	await publicRepository.userRegister(publicUser);

		}
		public async Task<bool> IsUserExist(string  phone)
		{
			
			return  await publicRepository.IsUserExist(phone);
		}

     
       

        public async Task<bool> IsUserNameExists(string userName)
        {
            return await publicRepository.IsUserNameExists(userName);
        }

        public async Task<bool> IsPhoneExists(string phone)
        {
            return await publicRepository.IsPhoneExists(phone);
        }

        public async Task<bool> RegisterUser(UserSignUpDto userSignUpDto)
        {
            return await publicRepository.RegisterUser(userSignUpDto);
        }
    }
}
