using BlogPlantasDomesticas.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogPlantasDomesticas.Api.Helpers;

public class PaginationHelper
{
    public static async Task<PagedList<T>> CreateAsync<T>(IQueryable<T> query, int pageNumber, int pageSize, CancellationToken cancellationToken) where T : class
    {
        var count = await query.CountAsync(cancellationToken);

        var items = await query.Skip((pageNumber - 1) * pageSize).Take((pageSize)).ToListAsync(cancellationToken);

        return new PagedList<T>(items, pageNumber, pageSize, count);
    }
}