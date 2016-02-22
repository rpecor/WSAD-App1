﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WSAD_App1.Models.Data
{
    [Table("tblSession")]
    public class Session
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Presenter { get; set; }
        public string Room { get; set; }
        public DateTime Time { get; set; }
        public DateTime DateCreated { get; set; }
        public int Occupancy { get; set; }
        public int CurrentEnrollment { get; set; }
    }
}