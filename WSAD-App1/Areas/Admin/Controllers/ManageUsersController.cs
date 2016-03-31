using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSAD_App1.Areas.Admin.Models.ViewModels.ManageUser;
using WSAD_App1.Models.Data;

namespace WSAD_App1.Areas.Admin.Controllers
{
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
    }
}