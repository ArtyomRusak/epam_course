using System.Web.Mvc;

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