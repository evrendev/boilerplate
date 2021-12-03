using Ardalis.Specification;
using EvrenDev.Domain.Entities;

namespace EvrenDev.Application.Specifications
{
    public class ContentFilterSpecification : Specification<Content>
    {
        public ContentFilterSpecification(bool? deleted,
            int? languageId)
        {
            Query.Where(content => 
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
            );
        }
    }
}
