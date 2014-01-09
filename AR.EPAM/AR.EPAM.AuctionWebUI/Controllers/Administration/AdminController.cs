using System.Web.Mvc;

namespace AR.EPAM.AuctionWebUI.Controllers.Administration
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        [HttpGet]
        [AttributeRouting.Web.Mvc.Route("admin")]
        public ActionResult Index()
        {
            return View();
        }
	}
}