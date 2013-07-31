using System.Web.Mvc;
using LevelUp.Attributes;
using LevelUp.Models;
using System.Web.Security;

namespace LevelUp.Controllers
{
    public class AuthorizationController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            // If user has already logged in, redirect to the secure area.
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Secure");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(LogOnModel model, string returnUrl)
        {
            // Verify the fields.
            if (ModelState.IsValid)
            {
                // Validate the user login.
                if (Membership.ValidateUser(model.Username, model.Password))
                {
                    // Create the authentication ticket.
                    FormsAuthentication.SetAuthCookie(model.Username, false);

                    // Redirect to the secure area.
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Secure");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            return View(model);
        }

        public ActionResult LogOff()
        {
            // Delete the user details from cache.
            System.Web.HttpContext.Current.Cache.Remove(User.Identity.Name);

            // Delete the authentication ticket and sign out.
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Authorization");
        }


        [AllowAnonymous]
        public ActionResult Registration()
        {
                return View();
            
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Registration(RegisrationModel model)
        {
            // Verify the fields.
            if (ModelState.IsValid)
            {
                // add values to DB 
                return RedirectToAction("Index", "Authorization");
            }

            return View();

        }

    }
}
