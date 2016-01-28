using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSAD_App1.Models.ViewModels.Correspondence;

namespace WSAD_App1.Controllers
{
    public class CorrespondenceController : Controller
    {
        // GET: Correspondence
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ContactEmailViewModel contactMessage)
        {
            return null;
        }
    }
}