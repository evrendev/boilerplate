namespace EvrenDev.Domain.Shared
{
    public abstract class FullBaseEntity : BaseEntity
    {
        protected FullBaseEntity()
        {

        }

        protected FullBaseEntity(
            string metaTitle, 
            string metaDescription,
            string[] metaKeywords) : base()
        {
            MetaTitle = metaTitle;
            MetaDescription = metaDescription;
            MetaKeywords = metaKeywords;
        }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string[] MetaKeywords { get; set; }
    }
}