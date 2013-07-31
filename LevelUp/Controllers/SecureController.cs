using System.Web.Mvc;

namespace LevelUp.Controllers
{
    public class SecureController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
