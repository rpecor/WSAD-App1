using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSAD_App1.Models.Data;

namespace WSAD_App1.Areas.Admin.Models.ViewModels
{
    
    public class SessionListByUserViewModel
    {
        public SessionListByUserViewModel()
        {

        }
        public SessionListByUserViewModel(SessionSignup userOdr)
        {
            Id = userOdr.Id;
            UserId = userOdr.UserId;
         ///   UserName = userOdr.attendee.Username;
         ///   FirstName = userOdr.attendee.FirstName;
         ///   LastName = userOdr.attendee.LastName;
         ///   EmailAddress = userOdr.attendee.EmailAddress;
            Title = userOdr.Session.Title;
            Presenter = userOdr.Session.Presenter;
            Time = userOdr.Session.Time;
            Room = userOdr.Session.Room;

        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string OrderStatus { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Title { get; set; }
        public string Presenter { get; set; }
        public DateTime Time { get; set; }
        public string Room { get; set; }
        public bool IsSelected { get; set; }
    }
}