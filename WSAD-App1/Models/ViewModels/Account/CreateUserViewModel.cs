using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSAD_App1.Models.ViewModels.Account
{
    public class CreateUserViewModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public string gender { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public bool recieveEmails { get; set; }

    }
}