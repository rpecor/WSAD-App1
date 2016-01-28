using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WSAD_App1.Models.ViewModels.Account;

namespace WSAD_App1.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return this.RedirectToAction("Login");
        }

        /// <summary>
        /// To Create a user account for my application
        /// </summary>
        /// <returns>ViewResult for the create</returns>


        public ActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Logging users into the web site
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginUserViewModel loginUser)
        {

            //Validate a username and password is passed (no empties)
            if (loginUser == null)
            {
                ModelState.AddModelError("", "Login is required");
                return View();
            }

            if (string.IsNullOrWhiteSpace(loginUser.Username))
            {
                ModelState.AddModelError("", "Username is required");
                return View();
            }

            if (string.IsNullOrWhiteSpace(loginUser.Password))
            {
                ModelState.AddModelError("", "Password is required");
                return View();

            }
            // open database connection
            //hash password
            //query for user based on username and password hash
            //if invalid, send error
            //valid, redirect to user profile
            System.Web.Security.FormsAuthentication.SetAuthCookie(loginUser.Username, loginUser.RememberMe);

            return Redirect(FormsAuthentication.GetRedirectUrl(loginUser.Username, loginUser.RememberMe));
        }
      
    }
}