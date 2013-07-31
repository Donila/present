using System.Web.Mvc;
using Present.WebMvc.Attributes;

namespace Present.WebMvc.Controllers
{
     [AllowAnonymous]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

    }
}
