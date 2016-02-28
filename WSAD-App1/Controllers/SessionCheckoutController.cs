using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSAD_App1.Models.Data;
using WSAD_App1.Models.ViewModels.ManageSessions;
using WSAD_App1.Models.ViewModels.SessionSignup;

namespace WSAD_App1.Controllers
{
    public class SessionCheckoutController : Controller
    {
        [Authorize]
        // GET: SessionCheckout
        public ActionResult Index()
        {
            List<SessionSignupViewModel> sessionSignupItems;

            using (WSADDbContext context = new WSADDbContext())
            {
                //get user info
                string username = User.Identity.Name;

                //get user id from db
                int userId = context.Users
                    .Where(x => x.Username == username)
                    .Select(x => x.Id)
                    .FirstOrDefault();
                //get session signup items

                sessionSignupItems =
                    context.SessionSignup.Where(x => x.UserId == userId)
                    .ToArray()
                    .Select(x => new SessionSignupViewModel(x))
                    .ToList();

                //generate session signup view model
            }


                return View(sessionSignupItems);
        }
        [HttpPost]

        public ActionResult AddToSessionOrder(List<SessionViewModel> sessionsToAdd)
        {
            //verify that sessionsToAdd is not null
            if(sessionsToAdd == null) { return RedirectToAction("Index"); }
           
            //capture sessions to add (filter by isSelected)
            sessionsToAdd = sessionsToAdd.Where(p => p.IsSelected).ToList();

            //IF there are no sessions to add, redirect to shopping cart index
            if (sessionsToAdd.Count <= 0) { return RedirectToAction("Index"); }

            //get user from User.Identity.Name
            string username = User.Identity.Name;

            
            using (WSADDbContext context = new WSADDbContext())
            {

                //get user from db, need their user Id
                int userId = context.Users
                    .Where(row => row.Username == username)
                    .Select(row => row.Id)
                    .FirstOrDefault();

                foreach (SessionViewModel sVM in sessionsToAdd)
                {
                    //does this session/user combo exist
                    if (context.SessionSignup.Any(row =>
                         row.UserId == userId && row.SessionId == sVM.Id))
                    {
                        //update quantity
                        SessionSignup existingSessionSignupDTO = context.SessionSignup.FirstOrDefault(row =>
                         row.UserId == userId && row.SessionId == sVM.Id);

                        existingSessionSignupDTO.Quantity++;
                    }
                    else
                    {
                        //create a Session checkout DTO
                        SessionSignup sessionDTO = new SessionSignup()
                        {
                            //add the session id and user id and quantity to the dto 
                            UserId = userId,
                            SessionId = sVM.Id,
                            Quantity = 1

                        };

                        //add the dto to the dbcontext
                        context.SessionSignup.Add(sessionDTO);
                    }
                }

                //save the db context
                context.SaveChanges();
                

            }
            //redirect to session list index

            return RedirectToAction("Index");

        }
    }
}