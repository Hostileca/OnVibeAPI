namespace Application.Dtos.Page;

public class PagedResponse<T>(IList<T> items, int currentPage, int pageSize)
{
    public IList<T> Items { get; init; } = items;
    public int CurrentPage { get; init; } = currentPage;
    public int PageSize { get; init; } = pageSize;
}