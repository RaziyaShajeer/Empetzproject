using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Domain.Service.WishLIst.DTOs;
using Domain.Service.WishLIst.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.WishLIst
{
    public class WishListService : IWishListService
    {
        private readonly IWishListRepository _wishListRepository;
        private readonly IMapper _mapper;

        public WishListService(IWishListRepository wishListRepository,IMapper mapper)
        {
            _wishListRepository = wishListRepository;
            _mapper = mapper;
        }
       
        public async Task<bool> AddToWishList(Guid user, Guid pet)
        {
          return  await _wishListRepository.AddToWishList(user, pet);
        }

        public async Task<GetWishListDTO> GetWishListByIdAsync(Guid userid, Guid wishListId)
        {
            return await _wishListRepository.GetWishListByIdAsync(userid, wishListId);
        }

        public async Task<List<GetWishListDTO>> GetWishListbyuser(Guid userId)
        {
            return await _wishListRepository.GetByUserId(userId);
        }

        public async Task<IEnumerable<GetWishListDTO>> GetWishListByUserIdAsync(Guid userId)
        {
            return await _wishListRepository.GetByUserIdAsync(userId);
        }

        public async Task<bool> IsExist(Guid user, Guid pet)
        {
            return await _wishListRepository.IsExist(user, pet);
        }

        public async Task<bool> RemovePetFromWishListAsync(Guid userId, Guid petId)
        {
            return await _wishListRepository.RemovePetFromWishListAsync(userId, petId);
        }
        public async Task<List<WishlistDtopage>> GetWishListbyuser(Wishlistparams param)
        {
           var wishlist=await  _wishListRepository.GetByUserId(param);
            PagedList<WishlistDtopage> getWishListDTO=_mapper.Map<PagedList<WishlistDtopage>>(wishlist);
            return getWishListDTO;
        }
    }
}
