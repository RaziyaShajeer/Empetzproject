using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Empetz.Controllers
{
    [Route("api/v1")]
    public abstract class BaseApiController<T> : ControllerBase
    {
           
    }
}
