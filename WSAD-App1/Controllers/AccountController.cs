using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WSAD_App1.Models.Data;
using WSAD_App1.Models.ViewModels.Account;
using WSAD_App1.Models.ViewModels.Correspondence;

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
        public ActionResult Create(CreateUserViewModel newUser)
        {
            if (!ModelState.IsValid)
            {
                return View(newUser);
            }
            if (!newUser.Password.Equals(newUser.PasswordConfirm))
            {
                ModelState.AddModelError("", "Password does not match password confirm.");
                return View(newUser);
            }

            using (WSADDbContext context = new WSADDbContext())
            {
                if (context.Users.Any(row => row.Username.Equals(newUser.Username)))
                {
                    ModelState.AddModelError("", "Username '" + newUser.Username + "' already exists. Try again.");
                    newUser.Username = "";
                    return View(newUser);
                }

                User newUserDTO = new Models.Data.User()
                {
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    EmailAddress = newUser.EmailAddress,
                    IsActive = true,
                    IsAdmin = false,
                    Username = newUser.Username,
                    Password = newUser.Password,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now
                };

                newUserDTO = context.Users.Add(newUserDTO);

                context.SaveChanges();


            }

            return RedirectToAction("login");

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
            bool isValid = false;
            using (WSADDbContext context = new WSADDbContext())
            {
                //hash password
                //query for user based on username and password hash
                if (context.Users.Any(
                    row => row.Username.Equals(loginUser.Username)
                    && row.Password.Equals(loginUser.Password)
                    ))
                {
                    isValid = true;
                }
            }
            //if invalid, send error
            if (!isValid)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View();
            }
            else {
                //valid, redirect to user profile

                System.Web.Security.FormsAuthentication.SetAuthCookie(loginUser.Username, loginUser.RememberMe);
                return Redirect(FormsAuthentication.GetRedirectUrl(loginUser.Username, loginUser.RememberMe));

            }
            
        }
      
    }
}