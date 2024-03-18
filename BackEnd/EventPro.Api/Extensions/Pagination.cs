using System.Text.Json;
using EventPro.Api.ViewModels;

namespace EventPro.Api.Extensions;

public static class Pagination
{
    public static void AddPagination(this HttpResponse response, 
                                     int currentPage, 
                                     int itemsPerPage, 
                                     int totalItens, 
                                     int totalPages)
    {
        var pagination = new PaginationHeader(currentPage, itemsPerPage, totalItens, totalPages);
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        response.Headers.Add("Pagination", JsonSerializer.Serialize(pagination, options));
        response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
    }
}