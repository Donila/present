using System.Web.Mvc;
using LevelUp.Attributes;

namespace LevelUp.Controllers
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
