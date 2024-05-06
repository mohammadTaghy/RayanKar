using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Customers.Query
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
            Message = totalCount == 0 && string.IsNullOrEmpty(message)? CommonMessage.EmptyResponse: message;
        }

    }
}
