using System.Net;

namespace ClientBlazor.Model
{
    public sealed class CommandResponse<T>
    {

        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }

        public T? Result { get; set; }

        public CommandResponse(HttpStatusCode statusCode, string message, T? result)
        {
            StatusCode = statusCode;
            Message = message;
            Result = result;
        }



    }
}
