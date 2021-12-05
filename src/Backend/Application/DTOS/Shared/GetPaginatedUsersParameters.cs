namespace EvrenDev.Application.DTOS.Shared {
    public class GetPaginatedUsersParameters {
        public int PageNumber {get; set; } = 1;

        public int PageSize  {get; set; } = 25;

        public bool? Deleted { get; set; }
    }
}