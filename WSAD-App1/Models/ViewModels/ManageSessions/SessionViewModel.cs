using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSAD_App1.Models.Data;

namespace WSAD_App1.Models.ViewModels.ManageSessions
{

    

    public class SessionViewModel
    {
        public SessionViewModel()
            {

            }
    public SessionViewModel(Data.Session row)
    {
            this.Id = row.Id;
            this.Title = row.Title;
            this.Description = row.Description;
            this.Presenter = row.Presenter;
            this.Room = row.Room;
            this.Time = row.Time;
            this.Occupancy = row.Occupancy;
            this.CurrentEnrollment = row.CurrentEnrollment;
            

    }

    
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Presenter { get; set; }
        public string Room { get; set; }
        public DateTime Time { get; set; }
        public int Occupancy { get; set; }
        public int CurrentEnrollment { get; set; }
        public bool IsSelected { get; set; }
    }
}