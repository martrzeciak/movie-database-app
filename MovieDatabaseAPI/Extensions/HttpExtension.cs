using MovieDatabaseAPI.Helpers;
using System.Text.Json;

namespace MovieDatabaseAPI.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, PaginationHeader paginationHeader)
        {
            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            response.Headers.Append("Pagination", JsonSerializer.Serialize(paginationHeader, jsonOptions));
            response.Headers.Append("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
