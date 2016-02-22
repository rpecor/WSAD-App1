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
    [Authorize]
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
        [AllowAnonymous]
        public ActionResult Create()
        { 
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
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
                    DateModified = DateTime.Now,
                    Gender = newUser.Gender
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
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
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

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }

        public ActionResult UserNavPartial()
        {
            //capture logged in user
            string username;
            username = this.User.Identity.Name;
            //get user information from database

            UserNavPartialViewModel userNavVM;

            using (WSADDbContext context = new WSADDbContext())
            {
                //search for user
                Models.Data.User userDTO = context.Users.FirstOrDefault(x => x.Username == username);

                if (userDTO == null) { return Content(""); }
                //Build  our UserNavPartialViewModel
                userNavVM = new UserNavPartialViewModel()
                {
                    FirstName = userDTO.FirstName,
                    LastName  = userDTO.LastName,
                    id = userDTO.Id
                
                };
                
            }
                //send the view model to the partial view
                return PartialView(userNavVM);
        }

        public ActionResult UserProfile(int? id = null)
        {
            //capture logged in user
            string username = User.Identity.Name;
            //Retreive the user from database
            UserProfileViewModel profileVM;
            using (WSADDbContext context = new WSADDbContext())
            {
                // get user from DB
                User userDTO;
                if (id.HasValue)
                {
                    userDTO = context.Users.Find(id.Value);
                }
                else {
                    userDTO = context.Users.FirstOrDefault(row => row.Username == username);
                }
                if (userDTO == null)
                {
                    return Content("Invalid Username");
                }

                // populate our userprofileviewmodel
                profileVM = new UserProfileViewModel()
                {
                    DateCreated = userDTO.DateCreated,
                    EmailAddress = userDTO.EmailAddress,
                    FirstName = userDTO.FirstName,
                    //Gender = userDTO.Gender,
                    Id = userDTO.Id,
                    IsAdmin = userDTO.IsAdmin,
                    LastName = userDTO.LastName,
                    UserName = userDTO.Username
                };
            }
                

                return View(profileVM);

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            // get user by id
            EditViewModel editVM;
            using (WSADDbContext context = new WSADDbContext())
            {
                //get user from DB
                User userDTO = context.Users.Find(id);

                //create a editviewmodel
                if (userDTO == null)
                {
                    return Content("Invalid Id");
                }
                //send viewmodel to the view
                editVM = new EditViewModel()
                {
                    EmailAddress = userDTO.EmailAddress,
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Id = userDTO.Id,
                    UserName = userDTO.Username,
                    Gender = userDTO.Gender

                };

            }

            //send viewmodel to the view
            return View(editVM);
        }
        [HttpPost]
        public ActionResult Edit(EditViewModel editVM)
        {
            //Variables
            bool needsPasswordReset = false;
            bool usernameHasChanged = false;
            //validate Model
            if (!ModelState.IsValid)
            {
                return View(editVM);
            }

            //check for password change?
            if (!string.IsNullOrWhiteSpace(editVM.Password))
            {
                //compare password and passwordconfirm
                if (editVM.Password != editVM.PasswordConfirm)
                {
                    ModelState.AddModelError("", "Password and password confirm must match.");
                    return View(editVM);
                }
                else
                {
                    needsPasswordReset = true;
                }
            }


            //get our user from DB
            using (WSADDbContext context = new WSADDbContext())

            {
                User userDTO = context.Users.Find(editVM.Id);
                if (userDTO == null) { return Content("Invalid user Id"); }
                //check for username change
                if (userDTO.Username != editVM.UserName)
                {
                    userDTO.Username = editVM.UserName;

                    usernameHasChanged = true;
                }

                //set update values from viewmodel
                userDTO.FirstName = editVM.FirstName;
                userDTO.LastName = editVM.LastName;
                userDTO.DateModified = DateTime.Now;
                userDTO.EmailAddress = editVM.EmailAddress;
                userDTO.Gender = editVM.Gender;
                

                if (needsPasswordReset)
                {
                    userDTO.Password = editVM.Password;
                }


                //save changes
                context.SaveChanges();

            }

            if (usernameHasChanged || needsPasswordReset)
            {
                TempData["LogoutMessage"] = "After a username or password change. Please log in with the new credentials.";
                return RedirectToAction("Logout");
            }
                return RedirectToAction("UserProfile");
        }
    }
}