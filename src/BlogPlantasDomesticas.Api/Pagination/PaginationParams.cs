using System.ComponentModel.DataAnnotations;

namespace BlogPlantasDomesticas.Api.Pagination;

public class PaginationParams
{
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; }
    [Range(1,50, ErrorMessage = "O maximo de itens por pagina e: 50")]
    public int PageSize { get; set; }
}