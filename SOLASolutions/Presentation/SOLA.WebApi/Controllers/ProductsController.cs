using System.Web.Http;
using SOLA.Business;
using SOLA.Infrastructure.WebApi.Base;
using SOLA.WebApi.Models;

namespace SOLA.WebApi.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : BaseApiController
    {
        private readonly IProductManagement productManagement;
        public ProductsController(IProductManagement productManagement)
        {
            this.productManagement = productManagement;
        }

        public IHttpActionResult Get()
        {
            return Ok();
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            return Ok();
        }

        public IHttpActionResult Post(ProductModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Created();
        }

        public IHttpActionResult Put(ProductModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();
        }

        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            return Ok();
        }
    }
}