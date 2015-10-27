using System.Web.Mvc;
using Microsoft.Owin;
using SOLA.Cache;

namespace SOLA.WebApi.Controllers
{
    public class HomeController : Controller
    {
        private IRequestScopeCache c;
        public HomeController(IRequestScopeCache c)
        {
            this.c = c;
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
    }
}