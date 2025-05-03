using AutoMapper;
using Domain.Service.PetsBreed.DTOs;
using Domain.Service.PetsBreed.Interfaces;
using Domain.Service.Category.DTOs;
using Domain.Service.Category.Interfaces;
using Empetz.Controllers;
using Empetz_API.API.Breed.RequestObject;
using Empetz_API.API.Category.RequestObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Empetz_API.API.Breed
{
    [Authorize(Roles = "PublicUser")]
    [ApiController]
    public class BreedController : BaseApiController<BreedController>
    {
        IMapper _mapper;
        public IBreedRequestService _BreedRequestService { get; set; }
        public BreedController(IMapper mapper, IBreedRequestService BreedRequestService)
        {
            _mapper = mapper;
            _BreedRequestService = BreedRequestService;
        }
        [HttpGet]
        [Route("breed")]
        public async Task<IActionResult> GetBreeds()
        {
            try
            {
                List<BreedDto> Breed = await _BreedRequestService.GetBreeds();
                return Ok(_mapper.Map<List<BreedRequest>>(Breed));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
        [HttpPost]
        [Route("breed")]
        public async Task<IActionResult> AddBreeds(BreedPostRequest addBreed)
        {
           BreedPostDto breed=_mapper.Map<BreedPostDto>(addBreed);
            var result = await _BreedRequestService.AddBreed(breed);
            if(result==true)
            {
                return Ok(new { Message = "Breeds Added Successfully" });
              
            }
            else
            {
                return Ok(new { Message = "Breeds Added Successfully" });
               
            }


        }
        [HttpGet]
        [Route("breed/category/{categoryId}")]
        public async Task<IActionResult> GetBreedsBycategory(Guid categoryId)
        {
            try
            {
                List<BreedDto> Breed = await _BreedRequestService.GetBreedCategory(categoryId);
                return Ok(_mapper.Map<List<BreedRequest>>(Breed));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
        [HttpGet]
        [Route("breed/{id}")]
        public async Task<IActionResult> GetBreedsById(Guid id)
        {
            try
            {
                List<BreedDto> Breed = await _BreedRequestService.GetBreedbyId(id);
                return Ok(_mapper.Map<List<BreedRequest>>(Breed));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
    

    }
}
