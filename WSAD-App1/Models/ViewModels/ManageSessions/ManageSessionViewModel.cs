using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WSAD_App1.Models.Data;

namespace WSAD_App1.Models.ViewModels.ManageSessions
{
    public class ManageSessionViewModel
    {
        public ManageSessionViewModel()
        {

        }
        public ManageSessionViewModel(Session sessionDTO)
        {
            Id = sessionDTO.Id;
            Title = sessionDTO.Title;
            Description = sessionDTO.Description;
            Presenter = sessionDTO.Presenter;
            Room = sessionDTO.Room;
            Time = sessionDTO.Time;
            Occupancy = sessionDTO.Occupancy;
            CurrentEnrollment = sessionDTO.CurrentEnrollment;

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