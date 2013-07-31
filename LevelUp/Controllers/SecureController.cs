using System.Web.Mvc;

namespace Present.WebMvc.Controllers
{
    public class SecureController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
