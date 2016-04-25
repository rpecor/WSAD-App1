using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSAD_App1.Areas.Admin.Models.ViewModels.ManageUser;
using WSAD_App1.Models.Data;
using WSAD_App1.Models.ViewModels.SessionSignup;

namespace WSAD_App1.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageUsersController : Controller
    {
        // GET: Admin/ManageUsers
        public ActionResult Index()
        {
            //db context
            List<ManageUserViewModel> collectionOfUserVM = new List<ManageUserViewModel>();
            using (WSADDbContext context = new WSADDbContext())
            {
                //get all users
                var dbUsers = context.Users;

                //move users to ViewModel 
                foreach (var userDTO in dbUsers)
                {
                    collectionOfUserVM.Add(
                        new ManageUserViewModel(userDTO)
                        );
                }

            }
                return View(collectionOfUserVM);
        }

        [HttpPost]
        public ActionResult Delete(List<ManageUserViewModel> collectionOfUserVM)
        {
            var vmItemsToDelete = collectionOfUserVM.Where(x => x.IsSelected == true);

            using (WSADDbContext context = new WSADDbContext())
            {
                foreach (var vmItems in vmItemsToDelete)
                {
                    var dtoToDelete = context.Users.FirstOrDefault(row => row.Id == vmItems.Id);
                    context.Users.Remove(dtoToDelete);

                }
                context.SaveChanges();
            }
                return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteUserSessions(List<SessionSignupViewModel> sessionsToAdd)
        {
            var vmItemsToDelete = sessionsToAdd.Where(x => x.IsSelected == true);
            using (WSADDbContext context = new WSADDbContext())
            {
                foreach (var vmItems in vmItemsToDelete)
                {
                    var dtoToDelete = context.SessionSignup.FirstOrDefault(row => row.Id == vmItems.Id);
                    context.SessionSignup.Remove(dtoToDelete);
                }
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        



        
    }
}