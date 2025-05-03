
using AutoMapper;
using Domain.Models;
using Domain.Service.PetsBreed.DTOs;
using Domain.Service.Category.DTOs;
using Domain.Service.Login.DTOs;
using Domain.Models;
using Domain.Service.MyPets.DTOs;
using Domain.Service.Register.DTOs;
using Domain.Service.User.DTO;
using Empetz_API.API.Breed.RequestObject;
using Empetz_API.API.Category.RequestObject;
using Domain.Service.User.DTO;
using Empetz_API.API.Pets.RequestObject;
using Empetz_API.API.Public.RequestObject;
using Empetz_API.API.User.RequestObject;
using Domain.Service.WishLIst.DTOs;
using Empetz_API.API.WishList.RequestObject;
using Domain.Service.Report.DTOs;
using Empetz_API.API.Report.RequestObject;
using Domain.Service.Chat.DTOs;
using Empetz_API.API.Chat.RequestObject;
using Domain.Enums;

namespace Empetz_API.Extensions
{
    public class AutoMapperProfiles: AutoMapper.Profile
    {
        public AutoMapperProfiles()
		{
            CreateMap<UserSignUpRequest, UserSignUpDto>();
            CreateMap<UserSignUp,UserRegisterDto>().ReverseMap();
			CreateMap<UserRegisterDto, User>().ReverseMap();
            //CreateMap<PetPostRequest, PetPostDto>()
            //             .ForMember(dest => dest.Vaccinated, opt => opt.MapFrom(src => src.Vaccinated.HasValue ? (src.Vaccinated.Value ? Vaccinated.True : Vaccinated.False) : Vaccinated.False));
            CreateMap<PetPostRequest, PetPostDto>().ReverseMap();
            CreateMap< PetPostDto,Pet>().ReverseMap();

            CreateMap<User, PublicUserLoginDto>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<UserRequest, UserDto>();
            CreateMap<BreedDto, BreedRequest>();
            CreateMap<Breed, BreedDto>();
            CreateMap<BreedDto, BreedRequest>();
            CreateMap<Favourite, GetWishListDTO>();
            CreateMap<GetWishListDTO, WishListreturnRequest>();
            CreateMap<PetsCategory, CategoryDto>();
            CreateMap<CategoryDto, CategoryRequest>();
            CreateMap<UpdateUserDTO, User>();
            CreateMap<UpdateUserRequest, UpdateUserDTO>();
            CreateMap<CategoryUpdateDTo,PetsCategory>();
            CreateMap<CategoryUpdateRequest, CategoryUpdateDTo>();
            CreateMap<PetsCategory, CategoryUpdateDTo>();
            CreateMap<PetPostDto, Pet>().ReverseMap();
			CreateMap<PetPostRequest,PetPostDto>();
			CreateMap<PetPostDto, Pet>();
			CreateMap<Pet,PetList>().ReverseMap();
			CreateMap<Pet, PetsByBreed>().ReverseMap();

			CreateMap<PetUpdateRequest,Pet>().ReverseMap();

            CreateMap<User, UserLoginDto>();

            CreateMap<Reason, ReasonDTO>();
            CreateMap<ReasonDTO, ReasonRequest>();
			CreateMap<PetsCategory, CategoryAddDTO>();
            CreateMap<CategoryAddDTO, PetsCategory>();
            CreateMap<CategoryAddDTO, CategoryAddRequest>();
            CreateMap<CategoryAddRequest, CategoryAddDTO>();
            CreateMap<Pet, PostedPetsDTO>()
           .ForMember(dest => dest.TimeAgo, opt => opt.Ignore());
            CreateMap<Pet,PetDTO>()
			.ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.User.Phone ));
			CreateMap<BreedPostRequest, BreedPostDto>();
            CreateMap<BreedPostDto, Breed>().ReverseMap();
            CreateMap<Favourite,WishlistDtopage>();
            CreateMap<WishlistDtopage,WishlistReturnpage>();

            CreateMap<MessageDto, Message>().ReverseMap();

        }


    }
}
