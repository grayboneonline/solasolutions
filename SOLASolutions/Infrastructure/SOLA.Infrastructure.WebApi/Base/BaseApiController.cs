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

        public IHttpActionResult Ok(string message = "")
        {
            return CreateResponse(Request, HttpStatusCode.OK, message);
        }

        public IHttpActionResult Ok<T>(T data, string message = "")
        {
            return CreateResponse(Request, HttpStatusCode.OK, data, message);
        }

        public IHttpActionResult Created(string message = "")
        {
            return CreateResponse(Request, HttpStatusCode.Created, message);
        }

        public IHttpActionResult Created<T>(T data, string message = "")
        {
            return CreateResponse(Request, HttpStatusCode.Created, data, message);
        }

        public IHttpActionResult NotFound(string message = "")
        {
            return CreateResponse(Request, HttpStatusCode.NotFound, message);
        }

        public new IHttpActionResult BadRequest(string message = "")
        {
            return CreateResponse(Request, HttpStatusCode.BadRequest, message);
        }

        public new IHttpActionResult BadRequest(ModelStateDictionary modelState)
        {
            return CreateResponse(Request, HttpStatusCode.BadRequest, new HttpError(modelState, true), "Invalid request data.");
        }

        private ApiResult CreateResponse(HttpRequestMessage request, HttpStatusCode statusCode, string message)
        {
            return new ApiResult(request, statusCode, message);
        }

        private ApiResult CreateResponse<T>(HttpRequestMessage request, HttpStatusCode statusCode, T data, string message)
        {
            return new ApiResult<T>(request, statusCode, data, message);
        }
    }
}
