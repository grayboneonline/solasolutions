using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace SOLA.Infrastructure.WebApi.Base
{
    public class BaseApiController : ApiController
    {
        public string CustomerSite
        {
            get { return Request.Properties[WebApiContants.RequestKeyCustomerSite].ToString(); }
        }

        public IHttpActionResult Ok(string message = "", ResponseType responseType = ResponseType.Success)
        {
            return CreateResponse(Request, HttpStatusCode.OK, responseType, message);
        }

        public IHttpActionResult Ok<T>(T data, string message = "", ResponseType responseType = ResponseType.Success)
        {
            return CreateResponse(Request, HttpStatusCode.OK, responseType, data, message);
        }

        public IHttpActionResult Created(string message = "", ResponseType responseType = ResponseType.Success)
        {
            return CreateResponse(Request, HttpStatusCode.Created, responseType, message);
        }

        public IHttpActionResult Created<T>(T data, string message = "", ResponseType responseType = ResponseType.Success)
        {
            return CreateResponse(Request, HttpStatusCode.Created, responseType, data, message);
        }

        public IHttpActionResult NotFound(string message = "", ResponseType responseType = ResponseType.Error)
        {
            return CreateResponse(Request, HttpStatusCode.NotFound, responseType, message);
        }

        public IHttpActionResult BadRequest(string message = "", ResponseType responseType = ResponseType.Error)
        {
            return CreateResponse(Request, HttpStatusCode.BadRequest, responseType, message);
        }

        public new IHttpActionResult BadRequest(ModelStateDictionary modelState)
        {
            return CreateResponse(Request, HttpStatusCode.BadRequest, ResponseType.Error, new HttpError(modelState, true), "Invalid request data.");
        }

        private ApiResult CreateResponse(HttpRequestMessage request, HttpStatusCode statusCode, ResponseType responseType, string message)
        {
            return new ApiResult(request, statusCode, responseType, message);
        }

        private ApiResult CreateResponse<T>(HttpRequestMessage request, HttpStatusCode statusCode, ResponseType responseType, T data, string message)
        {
            return new ApiResult<T>(request, statusCode, data, responseType, message);
        }
    }
}
