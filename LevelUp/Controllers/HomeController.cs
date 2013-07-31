using System.Web.Mvc;
using Present.Infrastructure.Services;
using Present.WebMvc.Attributes;

namespace Present.WebMvc.Controllers
{
     [AllowAnonymous]
    public class HomeController : BaseController
     {
         private IHomeControllerService _controllerService;

         public HomeController()
         {
         }

         public HomeController(IHomeControllerService controllerService)
         {
             _controllerService = controllerService;
         }

         public ActionResult Index()
        {
            return View();
        }
    }
}
