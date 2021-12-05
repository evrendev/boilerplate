using EvrenDev.Application.Interfaces.Result;
using MediatR;

namespace EvrenDev.Application.DTOS.Content
{
    public class ContentsPagedQuery : IRequest<PaginatedResult<ContentBasicDto>>
    {
        public ContentsPagedQuery()
        {
            
        }

        public ContentsPagedQuery(int pageNumber,
            int pageSize,
            bool? deleted,
            int? languageId)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Deleted = deleted;
            LanguageId = languageId;
        }

        public int PageNumber { get; set; }
        
        public int PageSize { get; set; }

        public bool? Deleted { get; set; }

        public int? LanguageId { get; set; }
    }
}