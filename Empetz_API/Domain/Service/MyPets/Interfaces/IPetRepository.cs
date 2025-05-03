using Domain.Helpers;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.MyPets.Interfaces
{
	public interface IPetRepository
	{
		Task<bool> AddPetAsync(Pet pet);
		Task<Helpers.PagedList<Pet>> getAllPets(PetParams param);
		Task<Pet> getpetById(Guid petid);
		public Task<PagedList<Pet>> getPetsByBreedId( PetListBreedParams param);
		Task<bool> deletePet(Guid petid);

        Task<IEnumerable<Pet>> GetUserPetPostedHistoryAsync(Guid userId);
        Task<IEnumerable<Pet>> GetUserPostedPetsAsync(Guid userId);
        Task<PagedList<Pet>> getPetsByCategoryId(PetListCategoryParams param);
        Task<PagedList<Pet>> getPetsByLocationId(PetListLocationParams param);
    

		Task<bool> updatePet(Pet pet, Guid PetId);
        IEnumerable<Pet> GetPetsByBreedName(string breedName);
        IEnumerable<Pet> PetsByBreedName(string breedName);
        IEnumerable<Pet> SearchPets(string searchTerm);
    
		
		Task<PagedList<Pet>> petfilter(PetfilterParams petfiltrer);
        Task<List<Location>> GetAllLocationsAsync();
        Task<Location> AddLocationAsync(Location location);
		Task UpdateAsync(Pet pet);
	}

}
