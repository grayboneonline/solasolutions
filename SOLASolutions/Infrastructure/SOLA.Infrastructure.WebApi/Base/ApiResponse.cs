namespace SOLA.Infrastructure.WebApi.Base
{
    public class ApiResponse
    {
        public ResponseType ResponseType { get; set; }
        
        public string Message { get; set; }

        public ApiResponse() { }

        public ApiResponse(ResponseType responseType, string message = "")
        {
            ResponseType = responseType;
            Message = message;
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; set; }

        public ApiResponse() { }

        public ApiResponse(ResponseType responseType, T data, string message = "")
            : base(responseType, message)
        {
            Data = data;
        }
    }

    public enum ResponseType
    {
        Error = 0,
        Success = 1,
        Warning = 2,
    }
}
