namespace ReserveHub.Application.Services;

public class PagedList<T> where T : class
{
    public IReadOnlyList<T> Data { get; } = [];
    public int Page { get; }
    public int PageSize { get; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public int TotalCount { get; }
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;
    private PagedList(IReadOnlyList<T> data, int page, int pageSize, int totalCount)
    {
        Data = data;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
    public static PagedList<T> Create(IReadOnlyList<T> data, int page, int pageSize, int totalCount)
        => new(data, page, pageSize, totalCount);
}
