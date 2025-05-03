using Domain.Helpers;
using Domain.Models;
using Domain.Service.MyPets.Interfaces;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Models;

using Microsoft.AspNetCore.Http.HttpResults;
using Domain.Exceptions;

using System.Reflection;
using Microsoft.Extensions.Configuration;



namespace Domain.Service.MyPets
{
	public class PetRepository : IPetRepository
	{
		protected readonly EmpetzContext empetzContext;
		public PetRepository(EmpetzContext _empetzContext)
		{
			empetzContext = _empetzContext;
		}



		public async Task<bool>    AddPetAsync(Pet pet)
		{

			try
			{
                //var user = await empetzContext.Users.Where(e => e.Id == pet.UserId).FirstOrDefaultAsync();
                //if (user != null)
                //{
                //    pet.Address = user.Address;
                //    // Other operations involving user
                //}
                pet.PetPosted = DateTime.Now;
				pet.Status = "A";
				await empetzContext.Pets.AddAsync(pet);

				await empetzContext.SaveChangesAsync();


				return true;
			}
			catch (Exception ex)
			{
				return false;
			}

		}
		public async Task<Helpers.PagedList<Pet>> getAllPets(PetParams param)
		{
			try
			{
				var query = empetzContext.Pets.Include(e => e.Breed).Include(e => e.User).Include(e => e.Category).Include(e => e.Location).AsQueryable();
				return await Helpers.PagedList<Pet>.CreateAsync(query,
						param.PageNumber, param.PageSize);
			}
			catch(Exception ex) 
			{
				return null;
			}
		}
		public async Task<Pet> getpetById(Guid petid)
		{
			return await empetzContext.Pets.Where(e => e.Id == petid).Include(e => e.Category).Include(e => e.User).Include(e => e.Location).Include(e => e.Breed).FirstOrDefaultAsync();
		}
		public async Task<PagedList<Pet>> getPetsByBreedId(PetListBreedParams param)
		{
			var query = empetzContext.Pets.AsQueryable();
			if (param.BreedId != null)
			{
				query = query.Where(c => c.BreedId == param.BreedId).Include(e => e.User).Include(e => e.Location).Include(e => e.Category).Include(e => e.Breed);
			}

			return await Helpers.PagedList<Pet>.CreateAsync(query,
					param.PageNumber, param.PageSize);
		}
		public async Task<PagedList<Pet>> getPetsByCategoryId(PetListCategoryParams param)
		{
			var query = empetzContext.Pets.AsQueryable();
			if (param.categoryid != null)
			{
				query = query.Where(c => c.CategoryId == param.categoryid).Include(e => e.User).Include(e => e.Location).Include(e => e.Category).Include(e => e.Breed);
			}

			return await Helpers.PagedList<Pet>.CreateAsync(query,
					param.PageNumber, param.PageSize);
		}
		public async Task<bool> deletePet(Guid petid)
		{
			var pet = await empetzContext.Pets.Where(e => e.Id == petid).FirstOrDefaultAsync();
			if (pet == null)
			{

				throw new PetNotFoundException("Pet Not Found");
			}
			else
			{
				empetzContext.Pets.Remove(pet);
				empetzContext.SaveChanges();
				return true;

			}

		}
		public async Task<bool> updatePet(Pet Updatepet, Guid PetId)
		{
			Updatepet.Id = PetId;
			var petToUpdate = await empetzContext.Pets.Where(e => e.Id == PetId).FirstOrDefaultAsync();
			if (petToUpdate == null)
			{
				throw new PetNotFoundException("Pet Not Found");
			}
			else
			{
				petToUpdate.Name = (Updatepet.Name == null) ? petToUpdate.Name : Updatepet.Name;
				petToUpdate.Image = (Updatepet.Image == null) ? petToUpdate.Image : Updatepet.Image;
				petToUpdate.CategoryId = (Updatepet.CategoryId == null || Updatepet.CategoryId == new Guid()) ? petToUpdate.CategoryId : Updatepet.CategoryId;
				petToUpdate.UserId = (Updatepet.UserId == null || Updatepet.UserId == new Guid()) ? petToUpdate.UserId : Updatepet.UserId;
				petToUpdate.BreedId = (Updatepet.BreedId == null || Updatepet.BreedId == new Guid()) ? petToUpdate.BreedId : Updatepet.BreedId;
				petToUpdate.Age = (Updatepet.Age == null) ? petToUpdate.Age : Updatepet.Age;
				petToUpdate.Gender = (Updatepet.Gender == null) ? petToUpdate.Gender : Updatepet.Gender;
				petToUpdate.Discription = (Updatepet.Discription == null) ? petToUpdate.Discription : Updatepet.Discription;
				petToUpdate.Gender = (Updatepet.Gender == null) ? petToUpdate.Gender : Updatepet.Gender;


				petToUpdate.Location = (Updatepet.LocationId == null || Updatepet.LocationId == new Guid()) ? petToUpdate.Location : Updatepet.Location;



			}
			empetzContext.Pets.Update(petToUpdate);
			empetzContext.SaveChanges();
			return true;

		}
		public async Task<IEnumerable<Pet>> GetUserPetPostedHistoryAsync(Guid userId)
		{
			return await empetzContext.Pets
		 .Where(p => p.UserId == userId)
		 .OrderByDescending(p => p.PetPosted)
		 .ToListAsync();
		}

		public async Task<IEnumerable<Pet>> GetUserPostedPetsAsync(Guid userId)
		{
			return await empetzContext.Pets.Include(p => p.User).Include(p => p.Category).Include(p=>p.Location).Include(p=>p.Breed)
			 .Where(p => p.UserId == userId)
			 .OrderByDescending(p => p.PetPosted)
			 .ToListAsync();
		}
		public async Task<PagedList<Pet>> petfilter(PetfilterParams petfiltrer)
		{
			var query = empetzContext.Pets.Include(e => e.Breed).Include(e => e.User).Include(e => e.Category).Include(e => e.Location).AsQueryable();
			if (petfiltrer.BreedId != null)
			{
				query = query.Where(e => e.BreedId == petfiltrer.BreedId);
			}
			if (petfiltrer.FromAge != null)
			{
				query = query.Where(e => e.Age >= petfiltrer.FromAge);

			}
			if (petfiltrer.ToAge != null)
			{
				query = query.Where(e => e.Age <= petfiltrer.ToAge);

			}
			if (petfiltrer.Pricestarting != null)
			{
				query = query.Where(e => e.Price >= petfiltrer.Pricestarting);
			}
			if (petfiltrer.Priceending != null)
			{
				query = query.Where(e => e.Price <= petfiltrer.Priceending);
			}
			if (petfiltrer.DatePublished != null)
			{
				query = query.Where(e => e.PetPosted.HasValue && e.PetPosted.Value.Date == petfiltrer.DatePublished.Value.Date);
			}
			if (petfiltrer.vaccinated != null)
			{
				query = query.Where(e => e.Vaccinated == petfiltrer.vaccinated);
			}
			if (petfiltrer.certified != null)
			{
				query = query.Where(e => e.Certified == petfiltrer.certified);
			}
			return await Helpers.PagedList<Pet>.CreateAsync(query,
					petfiltrer.PageNumber, petfiltrer.PageSize);
		}



		public async Task<PagedList<Pet>> getPetsByLocationId(PetListLocationParams param)
		{
			var query = empetzContext.Pets.AsQueryable();
			if (param.Locationid != null)
			{
				query = query.Where(c => c.LocationId == param.Locationid).Include(e => e.User).Include(e => e.Location).Include(e => e.Category).Include(e => e.Breed);
			}

			return await Helpers.PagedList<Pet>.CreateAsync(query,
					param.PageNumber, param.PageSize);
		}

		public IEnumerable<Pet> GetPetsByBreedName(string breedName)
		{
			return empetzContext.Pets
		   .Include(p => p.Breed)
		   .Include(p => p.Category)
		   .Include(p => p.Location)
		   .Include(p => p.User)
		   .Where(p => p.Breed.Name == breedName)
		   .ToList();
		}

		public IEnumerable<Pet> PetsByBreedName(string breedName)
		{



			var matchingPets = empetzContext.Pets
			.Where(p => p.Breed.Name.Contains(breedName)).ToList();

			return matchingPets;
		}

		public IEnumerable<Pet> SearchPets(string searchTerm)
		{
			var matchingPets = empetzContext.Pets
			.Where(p => p.Name.Contains(searchTerm) ||
						p.Breed.Name.Contains(searchTerm) ||
						p.Category.Name.Contains(searchTerm) ||
						p.Location.Name.Contains(searchTerm))
			.ToList();

			return matchingPets;
		}

        public async Task<List<Location>> GetAllLocationsAsync()
        {
            return await empetzContext.Locations.ToListAsync();
        }

        public async Task<Location> AddLocationAsync(Location location)
        {
            empetzContext.Locations.Add(location);
            await empetzContext.SaveChangesAsync();
            return location;
        }

		public async Task UpdateAsync(Pet pet)
		{
			empetzContext.Pets.Update(pet);
			await empetzContext.SaveChangesAsync();
		}
	}
}
    
	

		
