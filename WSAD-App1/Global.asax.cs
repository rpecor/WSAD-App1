using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WSAD_App1.Models.Data;

namespace WSAD_App1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_AuthenticateRequest()
        {
            if (Context.User == null) { return; }
            //get current user username
            string username = Context.User.Identity.Name;

            //setup a Dbcontext
            string[] roles = null;
            using (WSADDbContext context = new WSADDbContext())
            {
                //add our roles to the iprincipal object
                User userDTO = context.Users.FirstOrDefault(row => row.Username == username);
                if (userDTO != null)
                {
                    roles = context.UserRoles.Where(row => row.UserId == userDTO.Id)
                        .Select(row => row.Role.Name)
                        .ToArray();
                }
            }
                
                //build iprincipal object
            IIdentity userIdentity = new GenericIdentity(username);
            IPrincipal newUserObj = new System.Security.Principal.GenericPrincipal(userIdentity,roles);
            //update the context.user with our Iprincipal
            Context.User = newUserObj;
        }


    }
}
