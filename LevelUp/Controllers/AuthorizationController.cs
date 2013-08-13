using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Security;
using Present.Infrastructure.Services;
using Present.Infrastructure.Services.Users;
using Present.WebMvc.Attributes;
using Present.WebMvc.Models;
using Present.WebMvc.Providers;

namespace Present.WebMvc.Controllers
{
    public class AuthorizationController : Controller
    {
        private IAuthorizationControllerService _authorizationControllerService;

        



         public AuthorizationController(IAuthorizationControllerService authorizationControllerService)
         {
             _authorizationControllerService = authorizationControllerService;
         }

       



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

                var user = _authorizationControllerService.ValidateUser(model.Username, model.Password);
                if (user != null)
                {
                    System.Web.HttpContext.Current.Cache.Add(user.UserName, new MyMembershipUser(user.UserId, user.UserName), null, Cache.NoAbsoluteExpiration, FormsAuthentication.Timeout, CacheItemPriority.Default, null);
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
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



                //// Validate the user login.
                //if (Membership.ValidateUser(model.Username, model.Password))
                //{
                //    // Create the authentication ticket.
                //    FormsAuthentication.SetAuthCookie(model.Username, false);

                //    // Redirect to the secure area.
                //    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                //        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                //    {
                //        return Redirect(returnUrl);
                //    }
                //    else
                //    {
                //        return RedirectToAction("Index", "Secure");
                //    }
                //}
                //else
                //{
                //    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                //}
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
