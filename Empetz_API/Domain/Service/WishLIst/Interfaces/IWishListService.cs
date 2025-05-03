using Domain.Helpers;
using Domain.Service.Category.DTOs;
using Domain.Service.WishLIst.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.WishLIst.Interfaces
{
    public interface IWishListService
    {
        Task <bool>AddToWishList(Guid user, Guid pet);
       
        Task<GetWishListDTO> GetWishListByIdAsync(Guid userid, Guid wishListId);
        Task<List<GetWishListDTO>> GetWishListbyuser(Guid userId);
        Task<List<WishlistDtopage>> GetWishListbyuser(Wishlistparams param);
        Task<IEnumerable<GetWishListDTO>> GetWishListByUserIdAsync(Guid userId);
        Task<bool> IsExist(Guid user, Guid pet);
        Task<bool> RemovePetFromWishListAsync(Guid userId, Guid petId);
    }
}
