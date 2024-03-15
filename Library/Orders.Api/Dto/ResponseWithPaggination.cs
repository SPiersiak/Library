namespace Orders.Api.Dto;

public class ResponseWithPaggination<T>
{
    public ICollection<T> Result { get; set; }

    public int Page { get; set; }

    public int PageSize { get; set; }

    public int Total { get; set; }
}
