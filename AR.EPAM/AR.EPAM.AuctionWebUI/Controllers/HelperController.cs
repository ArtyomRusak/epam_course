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