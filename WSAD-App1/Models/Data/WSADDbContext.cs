﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WSAD_App1.Models.Data
{
    public class WSADDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}