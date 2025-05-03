using Domain.Helpers;
using Domain.Models;
using Domain.Service.WishLIst.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.WishLIst.Interfaces
{
    public interface IWishListRepository
    {
        Task<bool> AddToWishList(Guid user, Guid pet);
        Task<List<GetWishListDTO>> GetByUserId(Guid userId);
        Task<PagedList<Favourite>> GetByUserId(Wishlistparams param);
        Task<IEnumerable<GetWishListDTO>> GetByUserIdAsync(Guid userId);
        Task<GetWishListDTO> GetWishListByIdAsync(Guid userid, Guid wishListId);
        Task<bool> IsExist(Guid user, Guid pet);
        Task<bool> RemovePetFromWishListAsync(Guid userId, Guid petId);
    }
}
