using EvrenDev.Application.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EvrenDev.PublicApi.Controllers.Api.Shared
{
    [ApiController]
    [AuthorizeRoles(RoleNames.ADMINISTRATOR,RoleNames.SUPER_USER,RoleNames.MODERATOR)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController<T> : Controller
    {
        private IMediator _mediatorInstance;
        private ILogger<T> _loggerInstance;
        protected IMediator _mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
        protected ILogger<T> _logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>();
    }
}
