using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using EvrenDev.Infrastructure.DTOS.User;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using EvrenDev.Application.Constants;
using EvrenDev.Controllers.Api.Shared;
using EvrenDev.Application.DTOS.Shared;
using EvrenDev.Infrastructure.Model;

namespace EvrenDev.Controllers.Api
{
    [AuthorizeRoles(RoleNames.SUPER_USER, RoleNames.ADMINISTRATOR)]
    public class UsersController : BaseApiController<ApplicationUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(GetPaginatedUsersParameters request)
        {
            var user = await _userManager.GetUserAsync(User as ClaimsPrincipal);
            var currentUserId = user.Id;

            var query = new ApplicationUsersPagedQuery() {
                CurrentUserId = currentUserId,
                PageNumber = request.PageNumber, 
                PageSize = request.PageSize, 
                Deleted = request.Deleted
            };

            var items = await _mediator.Send(query);

            return Ok(items);
        }

        // POST accounts
        [HttpPost]
        public async Task<IActionResult> Post(CreateApplicationUserCommand command)
        {
            var item = await _mediator.Send(command);

            return Ok(item);
        }

        // GET: Account
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _mediator.Send(new GetApplicationUserByIdQuery() { Id = id });

            return Ok(item);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(Guid id, 
            UpdateApplicationUserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            return Ok(await _mediator.Send(command));
        }

        // DELETE: Account
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteApplicationUserCommand { Id = id }));
        }
    }
}
