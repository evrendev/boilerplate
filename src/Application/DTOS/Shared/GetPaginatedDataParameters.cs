namespace EvrenDev.Application.DTOS.Shared {
    public class GetPaginatedDataParameters {
        public int PageNumber {get; set; } = 1;

        public int PageSize  {get; set; } = 25;

        public int? LanguageId  {get; set; }

        public bool? Deleted { get; set; }
    }
}