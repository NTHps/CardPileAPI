using CardPileAPI.Services.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CardPileAPI.Controllers
{

    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[Controller]")]
    [TypeFilter(typeof(CustomExceptionFilterAttribute))]
    public class BaseController : ControllerBase
    {

    }

}
