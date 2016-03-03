using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSAD_App1.Models.ViewModels.ManageSessions;

namespace WSAD_App1.Models.ViewModels.SessionSignup
{
    public class SessionSignupViewModel
    {
        public SessionSignupViewModel()
        {

        }

        public SessionSignupViewModel(Data.SessionSignup row)
        {
            this.Id = row.Id;
            this.UserId = row.UserId;
            this.SessionId = row.SessionId;
            this.Quantity = row.Quantity;
            this.Session = new SessionViewModel(row.Session);

        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SessionId { get; set; }
        public int Quantity { get; set; }
        public SessionViewModel Session { get; set; }
        public bool IsSelected { get; set; }
    }
}