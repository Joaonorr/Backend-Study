namespace WebApplication1.Pagination;

public abstract class QueryStringParameters
{
    const int MaxPageSize = 20;
    public int PageNumer { get; set; }
    private int _pageSize = 10;
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
