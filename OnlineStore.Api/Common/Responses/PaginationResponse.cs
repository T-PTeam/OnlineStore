namespace OnlineStore.Api.Common.Responses;

public class PaginationResponse<T> 
{
    public PaginationResponse(T data, int total)
    {
        Total = total;
        Data = data;
    }

    public int Total { get; set; }

    public T Data { get; set; }
}