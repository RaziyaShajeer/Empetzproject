using Domain.Models;
using Domain.Service.Category.DTOs;
using Domain.Service.Category;
using Domain.Service.WishLIst.Interfaces;
using Empetz_API.API.Category.RequestObject;
using Empetz_API.API.WishList.RequestObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Domain.Service.WishLIst.DTOs;
using AutoMapper;
using Empetz.Controllers;
using Domain.Service.Category.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Domain.Helpers;
using Domain.Service.MyPets.DTOs;

namespace Empetz_API.API.WishList
{

    [ApiController]
    [Authorize(Roles = "PublicUser")]
    public class WishListController : BaseApiController<WishListController>
    {
        private readonly IWishListService _wishListService;
    readonly  IMapper _mapper;
        public WishListController(IWishListService wishListService,IMapper mapper)
        {
            _wishListService = wishListService;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("wishlist")]
        public async Task<IActionResult> AddToWishList([FromBody] WishListRequestObject request)
        {
            bool IsExist=false;
            IsExist = await _wishListService.IsExist(request.User,request.Pet) ;
            if (IsExist == true)
            {
                return Ok("Already Added");
            }
            else
            {
                bool result = await _wishListService.AddToWishList(request.User, request.Pet);
                if (result)
                {
                    return Ok("Pet added to favorites successfully");
                }

                return BadRequest("Failed to add pet to favorites");
            }
        }
        [HttpGet]
        [Route("wishlist/user/{userId}")]
        [ResponseCache(Duration = 2)]
       
        public async Task<IActionResult> GetWishListByuserId([FromQuery] Wishlistparams param)
        {
            try
            {
                var wishlist = await _wishListService.GetWishListbyuser(param);
                PagedList<WishlistReturnpage> wishlistofuser = _mapper.Map<PagedList<WishlistReturnpage>>(wishlist);

                return Ok(wishlistofuser);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("wishlist/{userId}")]
        public async Task<IActionResult> GetWishListByserId(Guid userId)
        {
            try
            {
                var wishList = await _wishListService.GetWishListByUserIdAsync(userId);
                return Ok(wishList);
            }
            catch (Exception ex)
            {
                return BadRequest("Invalid UserId");
            }
        }

        [HttpGet]
        [Route("wishlist/{wishListId}/user/{userid}")]
        public async Task<IActionResult> GetWishListById(Guid userid, Guid wishListId)
        {
            try
            {
                var wishList = await _wishListService.GetWishListByIdAsync(userid, wishListId);
                return Ok(wishList);
            }
            catch (Exception ex)
            {
                return BadRequest("No Wishlist in this id");
            }
        }

        [HttpDelete]
        [Route("wishlist/{userId}/pet/{petId}")]
        public async Task<IActionResult> RemovePetFromWishList(Guid userId, Guid petId)
        {
            try
            {
                bool removed = await _wishListService.RemovePetFromWishListAsync(userId, petId);

                if (removed)
                    return Ok("Pet removed from wishlist successfully");
                else
                    return NotFound("Pet not found in the wishlist");
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }


}
