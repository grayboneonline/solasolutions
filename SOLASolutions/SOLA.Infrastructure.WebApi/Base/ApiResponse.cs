namespace SOLA.Infrastructure.WebApi.Base
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        
        public string Message { get; set; }

        public ApiResponse() { }

        public ApiResponse(bool success, string message = "")
        {
            Success = success;
            Message = message;
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; set; }

        public ApiResponse() { }

        public ApiResponse(bool success, T data, string message = "")
            : base(success, message)
        {
            Data = data;
        }
    }
}
