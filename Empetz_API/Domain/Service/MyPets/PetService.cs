
using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Domain.Service.MyPets.DTOs;
using Domain.Service.MyPets.Interfaces;
using Domain.Service.Register.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.MyPets
{
	public class PetService:IPetservice
	{
		IMapper mapper;
		IPetRepository petRepository;

		public PetService(IMapper _mapper, IPetRepository _petRepository)
		{
			mapper = _mapper;
			petRepository = _petRepository;
		}

		public async Task<bool> AddPetAsync(PetPostDto petPost)
		{
			try
			{
				Pet pet = mapper.Map<Pet>(petPost);
				return await petRepository.AddPetAsync(pet);
			}
			catch(Exception ex)
			{
				throw ex;
			}
			
		}
		public async Task<PagedList<Pet>> getAllPets(PetParams param)
		{
			return await petRepository.getAllPets(param);
		}
		public async Task<Pet> getPet(Guid petid)
		{
			return await petRepository.getpetById(petid);
		}
		public async Task<PagedList<Pet>> getPetsByBreedId(PetListBreedParams param)
		{
			return await petRepository.getPetsByBreedId(param);
				}
        public async Task<PagedList<Pet>> GetAllPetsByCategoryAsync(PetListCategoryParams param)
        {
            return await petRepository.getPetsByCategoryId(param);
        }
        public async Task<bool> deletePet(Guid petid)
		{
			return await petRepository.deletePet(petid);
		}
		public async Task<bool> updatePet(Pet pettoUpdate, Guid PetId)
		{
			return await petRepository.updatePet(pettoUpdate,PetId);
		}


        private string CalculateTimeAgo(DateTime? postedTime)
        {
            if (postedTime.HasValue)
            {
                var timeDifference = DateTime.UtcNow - postedTime.Value;

                if (timeDifference.TotalMinutes < 1)
                    return "Just now";

                if (timeDifference.TotalHours < 1)
                    return $"{(int)timeDifference.TotalMinutes} {((int)timeDifference.TotalMinutes == 1 ? "minute" : "minutes")} ago";

                if (timeDifference.TotalDays < 1)
                    return $"{(int)timeDifference.TotalHours} {((int)timeDifference.TotalHours == 1 ? "hour" : "hours")} ago";

                return $"{(int)timeDifference.TotalDays} {((int)timeDifference.TotalDays == 1 ? "day" : "days")} ago";
            }

            return "Unknown time ago";
        }

        public async Task<IEnumerable<PostedPetsDTO>> GetUserPetPostedHistoryAsync(Guid userId)
        {
            var userPets = await petRepository.GetUserPetPostedHistoryAsync(userId);

            // Map pets to DTO and calculate time ago
            var petsDTO = mapper.Map<IEnumerable<PostedPetsDTO>>(userPets)
                .Select(dto =>
                {
                    dto.TimeAgo = CalculateTimeAgo(dto.PetPosted);
                    return dto;
                });

            return petsDTO;
        }

        public async Task<IEnumerable<PostedPetsDTO>> GetUserPostedPetsAsync(Guid userId)
        {
            var pets = await petRepository.GetUserPostedPetsAsync(userId);
            return mapper.Map<IEnumerable<PostedPetsDTO>>(pets);
        }

        public async Task<PagedList<Pet>> GetAllPetsByLocationAsync(PetListLocationParams param)
        {
            return await petRepository.getPetsByLocationId(param);
        }

        public IEnumerable<Pet> GetPetsByBreedName(string breedName)
        {
            return petRepository.GetPetsByBreedName(breedName);
        }

        public IEnumerable<Pet> PetsByBreedName(string breedName)
        {
            return petRepository.PetsByBreedName(breedName);
        }

        public IEnumerable<Pet> SearchPets(string searchTerm)
        {
            return petRepository.SearchPets(searchTerm);
        }

		Task<PagedList<Pet>> IPetservice.petfilter(PetfilterParams pettofilter)
		{
			return petRepository.petfilter(pettofilter);
		}

        public async Task<List<Location>> GetAllLocationsAsync()
        {
            return await petRepository.GetAllLocationsAsync();
        }
        public async Task<Location> AddLocationAsync(LocationPostDTO locationDTO)
        {
            var location = new Location
            {
                Name = locationDTO.Name
            };

            return await petRepository.AddLocationAsync(location);
        }
		public async Task UpdatePetStatusAsync(Guid petId)
		{
			var pet = await petRepository.getpetById(petId);
			if (pet == null)
			{
				// Handle case where pet is not found
				throw new Exception("Pet not found");
			}

			pet.Status = "D";
			await petRepository.UpdateAsync(pet);
		}
	}
    }

