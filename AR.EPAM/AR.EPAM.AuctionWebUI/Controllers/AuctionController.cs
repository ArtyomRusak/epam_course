using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AR.EPAM.AuctionWebUI.Mappings;
using AR.EPAM.AuctionWebUI.Models;
using AR.EPAM.Core.Entities.Auction;
using AR.EPAM.EFData;
using AR.EPAM.EFData.EFContext;
using AR.EPAM.Services.AuctionServices;
using AR.EPAM.Services.MembershipServices;
using AttributeRouting.Web.Mvc;

namespace AR.EPAM.AuctionWebUI.Controllers
{
    [Authorize(Roles = "Member")]
    public class AuctionController : Controller
    {
        public JsonResult GetCategories(string categoryName)
        {
            try
            {
                var context = new AuctionContext(Resources.ConnectionString);
                var unitOfWork = new UnitOfWork(context);
                var categoryService = new CategoryService(unitOfWork, unitOfWork);

                var category = categoryService.GetCategoryByName(categoryName);
                if (category != null)
                {
                    var categories = categoryService.GetSubCategories(category.Id);
                    unitOfWork.Dispose();
                    return Json(categories.Select(e => e.Name).ToList());
                }
                else
                {
                    unitOfWork.Dispose();
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [HttpGet]
        public ActionResult CreateLot()
        {
            var context = new AuctionContext(Resources.ConnectionString);
            var unitOfWork = new UnitOfWork(context);
            var currencyService = new CurrencyService(unitOfWork, unitOfWork);
            var categoryService = new CategoryService(unitOfWork, unitOfWork);

            var categories = categoryService.GetAllCategories();
            var currencies = currencyService.GetAllCurrencies();

            var model = new CreateLotViewModel
            {
                Currencies = new HashSet<string>(currencies.Select(e => e.Value)),
                Categories = new HashSet<Category>(categories)
            };

            unitOfWork.Dispose();

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateLot(CreateLotViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var context = new AuctionContext(Resources.ConnectionString);
                    var unitOfWork = new UnitOfWork(context);
                    var lotService = new LotService(unitOfWork, unitOfWork);
                    var categoryService = new CategoryService(unitOfWork, unitOfWork);
                    var currencyService = new CurrencyService(unitOfWork, unitOfWork);
                    var membershipService = new MembershipService(unitOfWork, unitOfWork);
                    var category = categoryService.GetCategoryByName(model.SelectedCategory);
                    var currency = currencyService.GetCurrencyByValue(model.SelectedCurrency);
                    var owner = membershipService.GetUserByEmail(HttpContext.User.Identity.Name);

                    var lot = lotService.CreateLot(model.Name, model.StartPrice, model.DurationInDays, model.Description,
                        currency.Id, owner.Id, category.Id);

                    unitOfWork.Dispose();

                    return RedirectToAction("UserPage", "Membership");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Error", e.Message);
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        [AttributeRouting.Web.Mvc.Route("lots/{id}")]
        public ActionResult ViewLot(int id)
        {
            var context = new AuctionContext(Resources.ConnectionString);
            var unitOfWork = new UnitOfWork(context);
            var lotService = new LotService(unitOfWork, unitOfWork);
            var lot = lotService.GetLotById(id);

            var mapper = new LotMapper();
            var viewModel = mapper.MapEntityToViewModel(lot);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ViewLot(double BidValue, string bidEmail)
        {
            return View();
        }
    }
}