using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSAD_App1.Areas.Admin.Models.ViewModels;
using WSAD_App1.Areas.Admin.Models.ViewModels.ManageSessions;
using WSAD_App1.Models.Data;

namespace WSAD_App1.Areas.Admin.Controllers
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
        [HttpGet]

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Create(CreateSessionViewModel newSession)
        {

            using (WSADDbContext context = new WSADDbContext())
            {


                Session newSessionDTO = new WSAD_App1.Models.Data.Session()
                {
                    Title = newSession.Title,
                    Description = newSession.Description,
                    Presenter = newSession.Presenter,
                    Room = newSession.Room,
                    Time = newSession.Time,
                    Occupancy = newSession.Occupancy

                };

                newSessionDTO = context.Sessions.Add(newSessionDTO);

                context.SaveChanges();


            }

            return RedirectToAction("index");

        }
        [HttpPost]
        // TODO This method needs to be changed to delete sessions, another page will handle adding sessions
        // TODO similar to adding users. This will be an admin only method as will this entire class.

        // TODO collectionOfSessionVM is not picking up items from checkbox-- index looks good must be below
        public ActionResult DeleteSessions(List<ManageSessionViewModel> newSession)
        {
            // filter collectionofsessions to find the selected items only
            var vmItemsToDelete = newSession.Where(x => x.IsSelected == true);
            using (WSADDbContext context = new WSADDbContext())
            {
                foreach (var vmItems in vmItemsToDelete)
                {
                    var dtoToDelete = context.Sessions.FirstOrDefault(row => row.Id == vmItems.Id);
                    context.Sessions.Remove(dtoToDelete);
                }
                context.SaveChanges();
            }
            return RedirectToAction("index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            // get user by id
            EditSessionViewModel editSessionVM;
            using (WSADDbContext context = new WSADDbContext())
            {
                //get user from DB
                Session sessionDTO = context.Sessions.Find(id);

                //create a editviewmodel
                if (sessionDTO == null)
                {
                    return Content("Invalid Id");
                }
                //send viewmodel to the view
                editSessionVM = new EditSessionViewModel()
                {
                    Title = sessionDTO.Title,
                    Description = sessionDTO.Description,
                    Presenter = sessionDTO.Presenter,
                    Id = sessionDTO.Id,
                    Room = sessionDTO.Room,
                    Time = sessionDTO.Time,
                    Occupancy = sessionDTO.Occupancy

                };

            }

            //send viewmodel to the view
            return View(editSessionVM);
        }
        [HttpPost]
        public ActionResult Edit(EditSessionViewModel editSessionVM)
        {

            //validate Model
            if (!ModelState.IsValid)
            {
                return View(editSessionVM);
            }




            //get our user from DB
            using (WSADDbContext context = new WSADDbContext())

            {
                Session sessionDTO = context.Sessions.Find(editSessionVM.Id);
                if (sessionDTO == null) { return Content("Invalid user Id"); }


                //set update values from viewmodel
                sessionDTO.Title = editSessionVM.Title;
                sessionDTO.Description = editSessionVM.Description;
                sessionDTO.Presenter = editSessionVM.Presenter;
                sessionDTO.Time = editSessionVM.Time;
                sessionDTO.Room = editSessionVM.Room;
                sessionDTO.Occupancy = editSessionVM.Occupancy;





                //save changes
                context.SaveChanges();

            }


            return RedirectToAction("index");
        }
        

    
    }
}