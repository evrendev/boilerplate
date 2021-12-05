using EvrenDev.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using EvrenDev.Application.DTOS.Settings.Department;
using EvrenDev.Application.Constants;
using EvrenDev.Controllers.Api.Shared;

namespace EvrenDev.Controllers.Api
{
    [AuthorizeRoles(RoleNames.SUPER_USER, RoleNames.ADMINISTRATOR)]
    public class DepartmentsController : BaseApiController<Department>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(DepartmentsPagedQuery request, 
            bool deleted)
        {
            var query = new DepartmentsPagedQuery() {
                PageNumber = request.PageNumber, 
                PageSize = request.PageSize, 
                Deleted = request.Deleted,
            };

            var items = await _mediator.Send(query);

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _mediator.Send(new GetDepartmentByIdQuery() { Id = id });

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateDepartmentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, UpdateDepartmentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteDepartmentCommand { Id = id }));
        }
    }
}
