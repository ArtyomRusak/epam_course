using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AR.EPAM.AuctionWebUI.Controllers
{
    public class HelperController : Controller
    {
        public ActionResult NoProfile()
        {
            return View();
        }
    }
}