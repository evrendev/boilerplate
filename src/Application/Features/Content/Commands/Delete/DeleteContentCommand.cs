using MediatR;
using EvrenDev.Application.DTOS.Content;
using EvrenDev.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using EvrenDev.Application.Interfaces.Result;

namespace EvrenDev.Application.Features.Content.Commands.Delete
{
    public class DeleteContentCommandHandler : IRequestHandler<DeleteContentCommand, Result<Guid>>
    {
        private readonly IContentRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteContentCommandHandler(IContentRepository repository, 
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteContentCommand command, 
            CancellationToken cancellationToken)
        {
            var item = await _repository.GetByIdAsync(command.Id);
            await _repository.DeleteAsync(item);
            await _unitOfWork.Commit(cancellationToken);
            return Result<Guid>.Success(item.Id, $"{item.Title} içeriği başarılı bir şekilde {(item.Deleted ? "silindi" : "geri yüklendi")}");
        }
    }
}