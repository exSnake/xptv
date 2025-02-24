namespace xptv.Core.Application.Common.Pagination;

public class PaginationService : IPaginationService
{
    public IEnumerable<T> Paginate<T>(IEnumerable<T> items, int page, int pageSize)
    {
        return items.Skip((page - 1) * pageSize).Take(pageSize);
    }

    public int GetPageCount<T>(IEnumerable<T> items, int pageSize)
    {
        return (int)Math.Ceiling(items.Count() / (double)pageSize);
    }
}