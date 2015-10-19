using System.Web.Mvc;

namespace SOLA.WebApi.Controllers
{
    public class HomeController : Controller
    {
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