using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Login(string username, string password, IEnumerable<bool> rememberMe)
        {
                return Content("Hello " + username + ". Welcome to my website!"+ rememberMe);
            
            //Validate a username and password is passed (no empties)
            // open database connection
            //hash password
            //query for user based on username and password hash
            //if invalid, send error
            //valid, redirect to user profile
        }
      
    }
}