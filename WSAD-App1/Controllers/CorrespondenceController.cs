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
            //Validate contact message input 
            if (contactMessage == null)
            {
                ModelState.AddModelError("","No message provided.");
            }

            if (string.IsNullOrWhiteSpace(contactMessage.Name) ||
                string.IsNullOrWhiteSpace(contactMessage.Email) ||
                string.IsNullOrWhiteSpace(contactMessage.Message))
            {
                ModelState.AddModelError("", "All fields are required.");
                return View();
            }
            //Create an email message object
            System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage();

            //Populate Email Object 
            email.To.Add("pecorrt@mail.uc.edu");
            email.From = new System.Net.Mail.MailAddress(contactMessage.Email);
            email.Subject = "This is our correspondence";
            email.Body = string.Format(
                "Name: {0}\r\nMessage: {1}",
                    contactMessage.Name,
                    contactMessage.Message
                );
            email.IsBodyHtml = false;

            //setup SMTP Client (to send the email message)

            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
            smtpClient.Host = "smtp.fuse.net";
                
            // send message
            smtpClient.Send(email);
            //notify the user the message was sent
            return View("emailConfirmation");

        }
    }
}