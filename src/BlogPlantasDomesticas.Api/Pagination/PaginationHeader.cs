namespace BlogPlantasDomesticas.Api.Pagination;

public class PaginationHeader
{
    public int CurrentPage { get; set; }
    public int ItemsPerpage { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    
    public PaginationHeader(int currentPage, int itemsPerpage, int totalPages, int totalItems)
    {
        CurrentPage = currentPage;
        ItemsPerpage = itemsPerpage;
        TotalPages = totalPages;
        TotalItems = totalItems;
    }
}