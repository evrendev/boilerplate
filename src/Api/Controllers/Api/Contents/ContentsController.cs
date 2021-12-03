using EvrenDev.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using EvrenDev.Application.DTOS.Content;
using EvrenDev.Application.DTOS.Shared;
using EvrenDev.Controllers.Api.Shared;

namespace EvrenDev.Controllers.Api
{
    public class ContentsController : BaseApiController<Content>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll(GetPaginatedDataParameters request)
        {
            var query = new ContentsPagedQuery() {
                PageNumber = request.PageNumber, 
                PageSize = request.PageSize, 
                LanguageId = request.LanguageId,
                Deleted = request.Deleted,
            };

            var items = await _mediator.Send(query);

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _mediator.Send(new GetContentByIdQuery() { Id = id });

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateContentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, UpdateContentCommand command)
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
            return Ok(await _mediator.Send(new DeleteContentCommand { Id = id }));
        }
    }
}
