using AutoMapper;
using Domain.Helpers;
using Domain.Models;
using Domain.Service.Category.DTOs;
using Domain.Service.Category;
using Domain.Service.MyPets;
using Domain.Service.MyPets.DTOs;
using Domain.Service.MyPets.Interfaces;
using Domain.Service.User.Interfaces;
using Empetz.Controllers;
using Empetz_API.API.Category.RequestObject;
using Empetz_API.API.Pets.RequestObject;
using Empetz_API.Extensions;
using HireMeNow_WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp;
using Domain.Enums;
using Domain.Service.Category.Interfaces;


namespace Empetz_API.API.Pets
{

	[ApiController]
    [Authorize(Roles = "PublicUser")]
    public class PetsController : BaseApiController<PetsController>
	{
		IMapper mapper;
		IPetservice petservice;
		IUserService userService;
        private readonly IWebHostEnvironment environment;
        public PetsController( IWebHostEnvironment environment, IMapper _mapper, IPetservice _petservice, IUserService _userService)
		{
			mapper = _mapper;
			petservice = _petservice;
			userService = _userService;
            this.environment = environment;
        }

        [HttpPost]
        [Route("pet")]
        public async Task<IActionResult> AddPet([FromForm] PetPostRequest pet)
        {
            try
            {
                string Imageurl = string.Empty;

                if (pet.ImageFile == null || pet.ImageFile.Length == 0)
                {
                    return BadRequest("Image file is required.");
                }

                // Check the image file size
                if (pet.ImageFile.Length > 3 * 1024 * 1024) // 3 MB limit
                {
                    return BadRequest("Image file size should be less than 3 MB.");
                }
                byte[] compressedImage;
                using (var memoryStream = new MemoryStream())
                {
                    await pet.ImageFile.CopyToAsync(memoryStream);
                    compressedImage = CompressImage(memoryStream.ToArray());
                }

                if (pet.Vaccinated == true && !pet.VaccinationDate.HasValue)
                {
                    return BadRequest("Vaccination date is required for vaccinated pets.");
                }

                // Map the PetPostRequest to PetPostDto
                var petPostDto = mapper.Map<PetPostDto>(pet);



                petPostDto.Image = compressedImage;

                string uniqueFileName1 = Guid.NewGuid().ToString();
             try
             {
             string Filepath = GetFilepath(pet.Name);
                  if (!System.IO.Directory.Exists(Filepath))
                  {
                   System.IO.Directory.CreateDirectory(Filepath);
                }

               string imagepath = Filepath + "\\" + uniqueFileName1 + ".png";
                 if (System.IO.File.Exists(imagepath))
             {
                     System.IO.File.Delete(imagepath);
             }
               using (FileStream stream = System.IO.File.Create(imagepath))
               {
                    await pet.ImageFile.CopyToAsync(stream);

                 }

                string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


                if (System.IO.File.Exists(imagepath))
                 {
                    Imageurl = hosturl + "/Upload/Category/" + pet.Name + "/" + uniqueFileName1 + ".png";
                }

                    petPostDto.ImagePath = Imageurl;

              }
               catch (Exception ex)
              {
                    return BadRequest(ex.Message);  
              }

            //    var result = await CategoryRequestService.AddCategoryAsync(categoryDto);

            //    if (result)
            //        return Ok("Category Added successfully");

            //    return BadRequest("Failed to Add Category");
            //}

                var result = await petservice.AddPetAsync(petPostDto);

                if (result)

                {
                    return Ok("Pets Added Successfully");
                }
                else
                {
                    return BadRequest("Failed to Add Category");
                }
            }
            catch (Exception ex) { 

            return BadRequest(ex.Message);

            }
        

		}
          [NonAction]
  private string GetFilepath(string itemname)
  {
      return this.environment.WebRootPath + "\\Upload\\Category\\" + itemname;
  }
		[HttpGet]
		[Route("pet")]
		
		public async Task<ActionResult> getAllPets([FromQuery] PetParams param)
		{
			var pets = await petservice.getAllPets(param);
			Response.AddPaginationHeader(pets.CurrentPage, pets.PageSize, pets.TotalCount, pets.TotalPages);
			List<PetList> petlists = mapper.Map<List<PetList>>(pets);
			return Ok(petlists);

		}
       
        [HttpGet("user/{userId}/pet-posted-history")]
        public async Task<IActionResult> GetUserPetPostedHistory(Guid userId)
        {
			try
			{
				var userPetHistory = await petservice.GetUserPetPostedHistoryAsync(userId);
				return Ok(userPetHistory);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
        [HttpGet("user-posted-history/{userId}")]
        public async Task<IActionResult> GetUserPostedPets(Guid userId)
        {
            try
            {
                var pets = await petservice.GetUserPostedPetsAsync(userId);
                // Calculate time ago and set it in the DTO
                var petsWithTimeAgo = CalculateTimeAgo(pets);
                return Ok(petsWithTimeAgo);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // Helper method to calculate time ago
        private IEnumerable<PostedPetsDTO> CalculateTimeAgo(IEnumerable<PostedPetsDTO> pets)
        {
            foreach (var pet in pets)
            {
                pet.TimeAgo = CalculateTimeDifference(pet.PetPosted);
            }
            return pets;
        }

        // Helper method to calculate time difference
        private string CalculateTimeDifference(DateTime? petPosted)
        {
            if (petPosted.HasValue)
            {
                var timeDifference = DateTime.UtcNow - petPosted.Value;
                if (timeDifference.TotalHours < 1)
                {
                    return $"{(int)timeDifference.TotalMinutes} minutes ago";
                }
                else if (timeDifference.TotalDays < 1)
                {
                    return $"{(int)timeDifference.TotalHours} hours ago";
                }
                else if (timeDifference.TotalDays < 30)
                {
                    return $"{(int)timeDifference.TotalDays} days ago";
                }
            }
            return "N/A";
        }
      

	
		[HttpGet]

		[Route("pet/{petid}")]
		
		public async Task<ActionResult> getPetsById(Guid petid)
		{

			Pet pet = await petservice.getPet(petid);
            if (pet == null)
            {
                return NotFound();  // Return NotFoundResult when the pet is not found
            }
            PetList selectedpet = mapper.Map<PetList>(pet);

			return Ok(selectedpet);


		}
		[HttpGet]
		[Route("pet/breed/{breedId}")]
		public async Task<ActionResult> getPetsByBreedId([FromQuery] PetListBreedParams param)
		{

			var pet = await petservice.getPetsByBreedId(param);
			PagedList<PetsByBreed> selectedpets = mapper.Map<PagedList<PetsByBreed>>(pet);


			return Ok(selectedpets);


		}
        [HttpGet("pet/catagory")]
        public async Task<IActionResult> GetPetsByCategory([FromQuery] PetListCategoryParams param)
        {
            var pets = await petservice.GetAllPetsByCategoryAsync(param);
            PagedList<PetDTO> selectedpets = mapper.Map<PagedList<PetDTO>>(pets);

            return Ok(selectedpets);
        }
        [HttpGet("pet/location")]
        public async Task<IActionResult> GetPetsByLocation([FromQuery] PetListLocationParams param)
        {
            var pets = await petservice.GetAllPetsByLocationAsync(param);
            PagedList<PetDTO> selectedpets = mapper.Map<PagedList<PetDTO>>(pets);

            return Ok(selectedpets);
        }
		[HttpPut("pets/{petId}/status")]
		public async Task<IActionResult> UpdatePetStatus(Guid petId)
		{
			try
			{
				await petservice.UpdatePetStatusAsync(petId);
				return Ok("Pet status updated successfully");
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}
		}
		[HttpDelete]
		[Route("pet/{petId}")]
		public async Task<ActionResult> DeletePet( Guid petId)
		{

			var result = await petservice.deletePet(petId);

			if (result == true)

			{
				return Ok(new { Message = "Pets Deleted SuccessFully" });

			}
			else
			{
				return NotFound("PetNotFound");
			}
		}

   
        //[HttpPut]
        //[Route("Pets/{PetId}/Pet")]
        //public IActionResult updatePet(PetPostRequest petupdate)
        //{


		[HttpPut]
		[Route("pet/{petId}")]
		public async Task<IActionResult> updatePet([FromForm] PetUpdateRequest petupdate, Guid PetId)
		{
			bool result=true;
            string Imageurl = string.Empty;

            if (petupdate.ImageFile!=null)
			{
                // Check the image file size
                if (petupdate.ImageFile.Length > 3 * 1024 * 1024) // 3 MB limit
                {
                    return BadRequest("Image file size should be less than 3 MB.");
                }
                byte[] compressedImage;
                using (var memoryStream = new MemoryStream())
                {
                    await petupdate.ImageFile.CopyToAsync(memoryStream);
                    compressedImage = CompressImage(memoryStream.ToArray());
                }

                var pet = mapper.Map<Pet>(petupdate);
				pet.Image = compressedImage;

                string uniqueFileName1 = Guid.NewGuid().ToString();
                try
                {
                    string Filepath = GetFilepath(pet.Name);
                    if (!System.IO.Directory.Exists(Filepath))
                    {
                        System.IO.Directory.CreateDirectory(Filepath);
                    }

                    string imagepath = Filepath + "\\" + uniqueFileName1 + ".png";
                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }
                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await petupdate.ImageFile.CopyToAsync(stream);

                    }

                    string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";


                    if (System.IO.File.Exists(imagepath))
                    {
                        Imageurl = hosturl + "/Upload/Category/" + pet.Name + "/" + uniqueFileName1 + ".png";
                    }

                    pet.ImagePath = Imageurl;

                }
                catch (Exception ex)
                {
                }


                result = await petservice.updatePet(pet, PetId);
				if (result == true)
				{
					return Ok(new { Message = "Pets Updated SuccessFully" });
				}
				else
				{
					return BadRequest(result);
				}
			}
			else
			{
				var pet = mapper.Map<Pet>(petupdate);
				result = await petservice.updatePet(pet,PetId);

				if (result == true)
				{
					return Ok(new { Message = "Pets Updated SuccessFully" });
				}
				else
				{
					return BadRequest(result);
				}

			}
			
				
		}
		[HttpGet]
		[Route("pet/filter")]
		public async Task<IActionResult> filter([FromQuery]PetfilterParams pettofilter)
		{
			var pets =await  petservice.petfilter(pettofilter);
			Response.AddPaginationHeader(pets.CurrentPage, pets.PageSize, pets.TotalCount, pets.TotalPages);
			var petlist=mapper.Map<List<PetList>>(pets);
			

			return Ok(petlist);
			
		}
        //[HttpGet("pet/breed/{breedName}")]
        //public IActionResult GetPetsByBreedName([FromQuery] string breedName)
        //{
        //    try
        //    {
        //        var pets = petservice.GetPetsByBreedName(breedName);
        //        return Ok(pets);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        // Handle exceptions
        //        return StatusCode(500, "Internal Server Error");
        //    }
        //}
        [HttpGet("pet/breedName")]
        public IActionResult SearchPetsByBreedName([FromQuery] string breedName)
        {
            var pets = petservice.PetsByBreedName(breedName);
            return Ok(pets);
        }
        [HttpGet("search")]
        public IActionResult SearchPets([FromQuery] string searchTerm)
        {
            var pets = petservice.SearchPets(searchTerm);
            return Ok(pets);
        }

        [HttpPost("location")]
		
        public async Task<IActionResult> AddLocationAsync(LocationPostDTO locationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var location = await petservice.AddLocationAsync(locationDTO);
                return Ok(location);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
       
        [HttpGet("location")]
        public async Task<IActionResult> GetAllLocationsAsync()
        {
            try
            {
                var locations = await petservice.GetAllLocationsAsync();
                return Ok(locations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

 private byte[] CompressImage(byte[] imageData)
   {
    using (var input = new MemoryStream(imageData))
    using (var output = new MemoryStream())
    {
        using (var image = Image.Load(input))
        {
            // Set the quality of the compressed image (0-100)
            var encoder = new JpegEncoder { Quality = 70 };
            image.Save(output, encoder);
        }
        return output.ToArray();
    }
}

    }

    


}
					

	

