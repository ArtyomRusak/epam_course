using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using AR.EPAM.AuctionWebUI.App_Start;
using AR.EPAM.AuctionWebUI.IoC;
using AR.EPAM.EFData;
using AR.EPAM.EFData.EFContext;
using AR.EPAM.Services.MembershipServices;

namespace AR.EPAM.AuctionWebUI
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            ContextFactory.Configure();

            RouteTable.Routes.MapRoute("Default", "{controller}/{action}", new { controller = "Home", action = "Index" });
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var i = HttpContext.Current.User.Identity;
                    var context = new AuctionContext(Resources.ConnectionString);
                    var unitOfWork = new UnitOfWork(context);
                    var membershipService = new MembershipService(unitOfWork, unitOfWork);
                    var user = membershipService.GetUserByEmail(i.Name);
                    var roles = user.Roles.Select(w => w.Name).ToArray();
                    HttpContext.Current.User = new GenericPrincipal(i, roles);
                    unitOfWork.Dispose();
                }
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}