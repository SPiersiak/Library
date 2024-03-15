namespace Orders.Api.Queries;

public class GetOrdersQuery
{
    private const int maxPageSize = 100;

    private int _pageSize = 10;

    public int PageNamber { get; set; } = 1;

    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = value > maxPageSize ? maxPageSize : value;
        }
    }
}
