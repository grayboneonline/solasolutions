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
        public string Message { get; set; }

        public bool Success
        {
            get
            {
                switch (StatusCode)
                {
                    case HttpStatusCode.OK:
                    case HttpStatusCode.Created:
                        return true;

                    default:
                        return false;
                }
            }
        }

        public ApiResult(HttpRequestMessage request, HttpStatusCode statusCode = HttpStatusCode.OK, string message = "")
        {
            Request = request;
            StatusCode = statusCode;
            Message = message;
        }

        public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Request.CreateResponse(StatusCode, new ApiResponse(Success, Message)));
        }
    }

    public class ApiResult<T> : ApiResult
    {
        public T Data { get; set; }

        public ApiResult(HttpRequestMessage request, HttpStatusCode statusCode, T data, string message = "")
            : base (request, statusCode, message)
        {
            Data = data;
        }

        public override Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Request.CreateResponse(StatusCode, new ApiResponse<T>(Success, Data, Message)));
        }
    }
}