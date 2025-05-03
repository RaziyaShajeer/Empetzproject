using Empetz.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Empetz_API.API.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : BaseApiController<AdminController>
    {
        public AdminController()
        {
            
        }
    }
}
