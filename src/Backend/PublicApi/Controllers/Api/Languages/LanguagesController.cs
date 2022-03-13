using Microsoft.AspNetCore.Mvc;
using EvrenDev.Application.Enums.Language;
using Microsoft.AspNetCore.Authorization;

namespace EvrenDev.PublicApi.Controllers.Api
{
    [AllowAnonymous]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LanguagesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var items = Languages.List();
            return Ok(items);
        }
    }
}