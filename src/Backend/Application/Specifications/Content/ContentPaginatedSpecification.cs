using Ardalis.Specification;
using EvrenDev.Domain.Entities;

namespace EvrenDev.Application.Specifications
{
    public class ContentFilterPaginatedSpecification : Specification<Content>
    {
        public ContentFilterPaginatedSpecification(int? languageId,
            bool? deleted,
            int skip = 0, 
            int take = 25)  
            : base()
        {                           
            Query
                .Where(content => 
                    (
                        !deleted.HasValue
                        ||
                        content.Deleted == deleted.Value
                    )
                    &&
                    (
                        !languageId.HasValue
                        ||
                        content.LanguageId == languageId.Value
                    )
                )
                .Skip(skip)
                .Take(take);
        }
    }
}
