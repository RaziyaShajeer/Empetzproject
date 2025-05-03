using DAL.Models;
using Domain.Helpers;
using Domain.Models;
using Domain.Service.WishLIst.DTOs;
using Domain.Service.WishLIst.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.WishLIst
{
    public class WishListRepository : IWishListRepository
    {
        private readonly EmpetzContext _context; 
        public WishListRepository(EmpetzContext context)
        {
                _context = context;
        }
     
        public async Task<List<GetWishListDTO>> GetByUserId(Guid userId)
        {
            var userFavorites = await _context.Favourites
    .Where(f => f.User == userId)
    .Select(f => new GetWishListDTO
    {
        id = f.PetNavigation.Id,
        userid = userId,
        petId = f.Pet,
        Name = f.PetNavigation.Name ?? "DefaultName",
        BreedName = f.PetNavigation.Breed.Name, // Assuming Breed has a Name property
        
        Gender = f.PetNavigation.Gender,
        Discription = f.PetNavigation.Discription,
        Image = f.PetNavigation.Image,
        CategoryName = f.PetNavigation.Category.Name // Assuming Category has a Name property
                                                     // Include other properties as needed
    })
    .ToListAsync();

            return userFavorites;
        }

        public async Task<IEnumerable<GetWishListDTO>> GetByUserIdAsync(Guid userId)
        {
            var userFavorites = await _context.Favourites
       .Where(f => f.User == userId)  
       .Select(f => new GetWishListDTO
       {
           id = f.PetNavigation.Id,
           userid = userId,
           petId = f.Pet,
           Name = f.PetNavigation.Name ?? "DefaultName",
           BreedName = f.PetNavigation.Breed.Name, // Assuming Breed has a Name property
		   height = f.PetNavigation.height,
		   weight = f.PetNavigation.weight,
		   location = f.PetNavigation.Location.Id,
		   Gender = f.PetNavigation.Gender,
           Discription = f.PetNavigation.Discription,
           Image = f.PetNavigation.Image,
           CategoryName = f.PetNavigation.Category.Name,
           Status=f.PetNavigation.Status// Assuming Category has a Name property
                                                        // Include other properties as needed
       })
       .ToListAsync();

            return userFavorites;
        }

        public async Task<GetWishListDTO> GetWishListByIdAsync(Guid userid, Guid wishListId)
        {
            return await _context.Favourites
                .Where(f => f.User == userid && f.Id == wishListId)
                .Select(f => new GetWishListDTO
                {
                    id = f.PetNavigation.Id,
                    userid = userid,
                    petId = f.Pet,
                    Name = f.PetNavigation.Name,
                    BreedName = f.PetNavigation.Breed.Name,
                  
  
                    Gender = f.PetNavigation.Gender,
                    Discription = f.PetNavigation.Discription,
                    Image = f.PetNavigation.Image,
                    CategoryName = f.PetNavigation.Category.Name
                    // Include other properties as needed
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsExist(Guid user, Guid pet)
        {
            var favorite = await _context.Favourites
               .FirstOrDefaultAsync(f => f.User == user && f.Pet == pet);
            if (favorite != null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> RemovePetFromWishListAsync(Guid userId, Guid petId)
        {
            var favorite = await _context.Favourites
               .FirstOrDefaultAsync(f => f.User == userId && f.Pet == petId);

            if (favorite != null)
            {
                _context.Favourites.Remove(favorite);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        async Task<bool> IWishListRepository.AddToWishList(Guid user, Guid pet)
        {
            try
            {

                var favorite = new Domain.Models.Favourite // Use the correct namespace
                {
                    Id = Guid.NewGuid(),
                    User = user,
                    Pet = pet
                };

                _context.Favourites.Add(favorite);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                // Handle exceptions as needed
                return false;
            }
        }

        public async  Task<PagedList<Favourite>> GetByUserId(Wishlistparams param)
        {
            var query = _context.Favourites.AsQueryable();
            if (param.userId != null)
            {
                query= query.Where(e=>e.User==param.userId).Include(e=>e.UserNavigation).Include(e=>e.PetNavigation).Include(e=>e.PetNavigation.Breed).Include(e=>e.PetNavigation.Category); 
            }
            return await Helpers.PagedList<Favourite>.CreateAsync(query,
                    param.PageNumber, param.PageSize);
        }
    }
    }

