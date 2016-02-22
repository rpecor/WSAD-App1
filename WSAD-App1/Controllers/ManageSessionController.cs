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
        public ActionResult AddSessions(List<ManageSessionViewModel> collectionOfSessionVM)
        {
            // do work
            return RedirectToAction("Index");
        }
    }
}