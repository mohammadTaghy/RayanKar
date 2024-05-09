namespace ClientBlazor.Model
{
    public interface IQueryResponse<T> where T : class, new()
    {
        int TotalCount { get; set; }
        T Result { get; set; }
        string Message { get; set; }
        bool IsSuccess { get; set; }
    }
    public sealed class QueryResponse<T> : IQueryResponse<T> where T : class, new()
    {

        public int TotalCount { get; set; }
        public T? Result { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public QueryResponse(T? result, int totalCount, bool isSuccess, string message)
        {
            Result = result;
            TotalCount = totalCount;
            IsSuccess = isSuccess;
            Message =  message;
        }

    }
}
