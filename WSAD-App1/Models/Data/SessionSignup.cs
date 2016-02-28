using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WSAD_App1.Models.Data
{
    [Table("tblSessionSignup")]
    public class SessionSignup
    {
        
        public SessionSignup()
        {

        }
        [Key]
        public int Id { get; set; }
        [Column("User_Id")]
        public int UserId { get; set; }
        [Column("Session_Id")]
        public int SessionId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("SessionId")]
        public virtual Session Session { get; set; }
        [ForeignKey("UserId")]
        public virtual User attendee { get; set; }

    }
}