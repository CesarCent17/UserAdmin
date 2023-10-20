using DataAccess.Utils;

namespace WebApi.Entities
{
    public class ApiResponse<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int StatusCode { get; set; }

        public ApiResponse(bool succeeded, string message, T data, int statusCode)
        {
            Succeeded = succeeded;
            Message = message;
            Data = data;
            StatusCode = statusCode;
        }
    }
}
