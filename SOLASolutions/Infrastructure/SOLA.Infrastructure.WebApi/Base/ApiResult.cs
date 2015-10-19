using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace SOLA.Infrastructure.WebApi.Base
{
    public class ApiResult : IHttpActionResult
    {
        public HttpRequestMessage Request { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public ResponseType ResponseType { get; set; }
        public string Message { get; set; }

        public ApiResult(HttpRequestMessage request, HttpStatusCode statusCode = HttpStatusCode.OK, ResponseType responseType = ResponseType.Success, string message = "")
        {
            Request = request;
            ResponseType = responseType;
            StatusCode = statusCode;
            Message = message;
        }

        public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Request.CreateResponse(StatusCode, new ApiResponse(ResponseType, Message)));
        }
    }

    public class ApiResult<T> : ApiResult
    {
        public T Data { get; set; }

        public ApiResult(HttpRequestMessage request, HttpStatusCode statusCode, T data, ResponseType responseType = ResponseType.Success, string message = "")
            : base(request, statusCode, responseType, message)
        {
            Data = data;
        }

        public override Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Request.CreateResponse(StatusCode, new ApiResponse<T>(ResponseType, Data, Message)));
        }
    }
}