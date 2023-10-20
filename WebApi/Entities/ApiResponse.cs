using DataAccess.Utils;

namespace WebApi.Entities
{
    public class ApiResponse<T>: OperationResult<T>
    {
        public int StatusCode { get; set; }
        public ApiResponse(bool succeeded, string errorMessage, T data, int statusCode)
        {
            Succeeded = succeeded;
            ErrorMessage = errorMessage;
            Data = data;
            StatusCode = statusCode;
        }
    }
}
