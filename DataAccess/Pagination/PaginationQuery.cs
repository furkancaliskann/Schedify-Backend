namespace DataAccess.Pagination
{
    public class PaginationQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string? SortBy { get; set; } = "Id";
        public string? SortDirection { get; set; } = "asc";
        public string? SearchTerm { get; set; }
    }
}
