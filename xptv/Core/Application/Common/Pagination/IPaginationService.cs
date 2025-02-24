namespace xptv.Core.Application.Common.Pagination
{
    public interface IPaginationService
    {
        public IEnumerable<T> Paginate<T>(IEnumerable<T> items, int page, int pageSize);
        public int GetPageCount<T>(IEnumerable<T> items, int pageSize);
    }
}
