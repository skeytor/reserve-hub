using System.Text.Json.Serialization;

namespace SharedKernel;

public sealed record PaginationParams(
    string? Q,
    string? SortColumn,
    SortOrder SortOrder,
    int Page = 1,
    int PageSize = 10)
{
    private const int MAX_PAGE_SIZE = 50;
    public int PageSize { get; init; } = PageSize > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : PageSize;
};

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SortOrder
{
    [JsonStringEnumMemberName("asc")]
    Asc,

    [JsonStringEnumMemberName("desc")]
    Desc
}