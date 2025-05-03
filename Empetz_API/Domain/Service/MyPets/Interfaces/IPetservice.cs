using Domain.Helpers;
using Domain.Models;
using Domain.Service.MyPets.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.MyPets.Interfaces
{
    public interface IPetservice
    {
        Task<List<Location>> GetAllLocationsAsync();
        public Task<bool> AddPetAsync(PetPostDto pet);
        Task<Helpers.PagedList<Pet>> getAllPets(PetParams param);
        Task<Pet> getPet(Guid petid);
        public Task<PagedList<Pet>> getPetsByBreedId(PetListBreedParams param);
        Task<bool> deletePet(Guid petid);
        Task<IEnumerable<PostedPetsDTO>> GetUserPetPostedHistoryAsync(Guid userId);
        Task<IEnumerable<PostedPetsDTO>> GetUserPostedPetsAsync(Guid userId);
        public Task<PagedList<Pet>> GetAllPetsByCategoryAsync(PetListCategoryParams param);

        public Task<PagedList<Pet>> GetAllPetsByLocationAsync(PetListLocationParams param);


        Task<bool> updatePet(Pet pettoUpdate, Guid PetId);
        IEnumerable<Pet> GetPetsByBreedName(string breedName);
        IEnumerable<Pet> PetsByBreedName(string breedName);
        IEnumerable<Pet> SearchPets(string searchTerm);

        Task<PagedList<Pet>> petfilter(PetfilterParams pettofilter);
        Task<Location> AddLocationAsync(LocationPostDTO locationDTO);
		Task UpdatePetStatusAsync(Guid petId);
	}
}
