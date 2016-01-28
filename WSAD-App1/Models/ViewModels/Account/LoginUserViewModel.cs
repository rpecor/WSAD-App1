using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSAD_App1.Models.ViewModels.Account
{
    public class LoginUserViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}