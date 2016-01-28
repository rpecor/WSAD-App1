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

        [HttpGet]
        public ActionResult Create()
        { 
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateUserViewModel createUser)
        {
            if (createUser == null)
            {
                ModelState.AddModelError("","Please enter the required fields, first name, last name, username, password and email.");
                return View();
            }
            if (string.IsNullOrWhiteSpace(createUser.firstName))
            {
                ModelState.AddModelError("", "First Name is required");
                return View();
            }
            if (string.IsNullOrWhiteSpace(createUser.lastName))
            {
                ModelState.AddModelError("", "Last name is required");
                return View();
            }
            if (string.IsNullOrWhiteSpace(createUser.emailAddress))
            {
                ModelState.AddModelError("", "Email address is required");
                return View();
            }
            if (string.IsNullOrWhiteSpace(createUser.Username))
            {
                ModelState.AddModelError("", "Username is required");
                return View();
            }
            if (string.IsNullOrWhiteSpace(createUser.Password))
            {
                ModelState.AddModelError("", "Password is required");
                return View();

            }
            if (string.IsNullOrWhiteSpace(createUser.PasswordConfirm))
            {
                ModelState.AddModelError("", "Password Confirmation is required");
                return View();

            }
            if (createUser.PasswordConfirm != createUser.Password)
            {
                ModelState.AddModelError("", "Oops! Password does not match password confirmation.");
                return View();
            }
            return Content("Hello " + createUser.firstName + " " + createUser.lastName + 
                "! Welcome to our site. " + "Username:" + createUser.Username + "Password: " +
                createUser.Password + "Email: " + createUser.emailAddress
                );
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