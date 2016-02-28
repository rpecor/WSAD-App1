using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSAD_App1.Models.Data;
using WSAD_App1.Models.ViewModels.ManageSessions;

namespace WSAD_App1.Controllers
{
    public class ManageSessionController : Controller
    {
        // GET: ManageSession
        public ActionResult Index()
        {
            //setup a db context
            List<ManageSessionViewModel> collectionOfSessionVM = new List<ManageSessionViewModel>();
            using (WSADDbContext context = new WSADDbContext())
            {
                var dbSessions = context.Sessions;
                // get all users 
                foreach (var sessionDTO in dbSessions)
                {
                    collectionOfSessionVM.Add(
                        new ManageSessionViewModel(sessionDTO)
                        );
                }


            }



            return View(collectionOfSessionVM);

            
        }

        [HttpPost]
        // TODO This method needs to be changed to delete sessions, another page will handle adding sessions
        // TODO similar to adding users. This will be an admin only method as will this entire class.

        // TODO collectionOfSessionVM is not picking up items from checkbox-- index looks good must be below
        public ActionResult AddSessions(List<ManageSessionViewModel> collectionOfSessionVM)
        {
            // filter collectionofsessions to find the selected items only
            var vmItemsToAdd = collectionOfSessionVM.Where(x => x.IsSelected == true);
            using (WSADDbContext context = new WSADDbContext())
            {
                foreach (var vmItems in vmItemsToAdd)
                {
                    var dtoToAdd = context.Sessions.FirstOrDefault(row => row.Id == vmItems.Id);
                    context.Sessions.Add(dtoToAdd);
                }
                context.SaveChanges();
            }
                return RedirectToAction("Index");
        }
    }
}