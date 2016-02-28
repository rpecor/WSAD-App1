using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSAD_App1.Models.Data;
using WSAD_App1.Models.ViewModels.ManageSessions;

namespace WSAD_App1.Controllers
{
    public class SessionController : Controller
    {
        [Authorize]
        // GET: Session
        public ActionResult Index()
        {
            List<SessionViewModel> sessionVM;
            using (WSADDbContext context = new WSADDbContext())
            {
                sessionVM = context.Sessions
                    .ToArray()
                    .Select(x => new SessionViewModel(x))
                    .ToList();
            }


                return View(sessionVM);
        }
    }
}