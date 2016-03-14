using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WSAD_App1.Models.Data;

namespace WSAD_App1.Areas.Admin.Models.ViewModels
{
    public class CreateSessionViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Presenter { get; set; }
        [Required]
        public string Room { get; set; }
        
        [DataType(DataType.Date)]
        
        
        public DateTime Time { get; set; }
        [Required]
        public int Occupancy { get; set; }
        
    }
}