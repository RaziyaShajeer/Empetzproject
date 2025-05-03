using Domain.Service.ContactUs.DTOs;
using Domain.Service.ContactUs.Interfaces;
using Empetz.Controllers;
using Empetz_API.ContactUs.RequestObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Empetz_API.ContactUs
{
   
    [ApiController]
    public class ContactUsController : BaseApiController<ContactUsController>
    {
        private readonly IContactUsService _contactUsService;

        public ContactUsController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService ?? throw new ArgumentNullException(nameof(contactUsService));
        }


        [HttpPost("contact-us")]
        public async Task<IActionResult> AddContactUs([FromBody] ContactUsRequest contactUsRequest)
        {
            try
            {
                // You can get the UserId from the logged-in user's claims
                //var userId = new Guid(User.FindFirst("sub")?.Value); 

                var contactUsDTO = new ContactUsDTO
                {
                    Name = contactUsRequest.Name,
                    Email = contactUsRequest.Email,
                    Message = contactUsRequest.Message,
                    UserId = contactUsRequest.UserId
                };

                var contactUsId = await _contactUsService.AddContactUsAsync(contactUsDTO);

                return Ok(new { ContactUsId = contactUsId });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
