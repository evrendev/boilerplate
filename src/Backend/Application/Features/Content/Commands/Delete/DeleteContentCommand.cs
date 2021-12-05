using MediatR;
using EvrenDev.Application.DTOS.Content;
using EvrenDev.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using EvrenDev.Application.Interfaces.Result;
using Microsoft.Extensions.Localization;

namespace EvrenDev.Application.Features.Content.Commands.Delete
{
    public class DeleteContentCommandHandler : IRequestHandler<DeleteContentCommand, Result<Guid>>
    {
        private readonly IContentRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<DeleteContentCommand> _loc;

        public DeleteContentCommandHandler(IContentRepository repository, 
            IStringLocalizer<DeleteContentCommand> loc,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _loc = loc;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteContentCommand command, 
            CancellationToken cancellationToken)
        {
            var item = await _repository.GetByIdAsync(command.Id);
            await _repository.DeleteAsync(item);
            await _unitOfWork.Commit(cancellationToken);

            var message = string.Format(_loc["delete_content_success"], item.Title, item.Deleted ? _loc["deleted"] : _loc["restored"]);

            return Result<Guid>.Success(item.Id, message);
        }
    }
}