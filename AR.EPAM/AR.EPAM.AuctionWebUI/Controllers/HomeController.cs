using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting.Web.Mvc;

namespace AR.EPAM.AuctionWebUI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [AttributeRouting.Web.Mvc.Route("home")]
        public ActionResult Index()
        {
            return View();
        }
	}
}