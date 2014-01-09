using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AR.EPAM.AuctionWebUI.IoC;
using AR.EPAM.AuctionWebUI.Mappings;
using AR.EPAM.AuctionWebUI.Models;
using AR.EPAM.AuctionWebUI.Models.AuctionViewModels;
using AR.EPAM.Core.Entities.Auction;
using AR.EPAM.EFData;
using AR.EPAM.EFData.EFContext;
using AR.EPAM.Services.AuctionServices;
using AR.EPAM.Services.Exceptions;
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
                var context = Factory.GetContext();
                var unitOfWork = new UnitOfWork(context);
                var categoryService = new CategoryService(unitOfWork, unitOfWork);

                var category = categoryService.GetCategoryByName(categoryName);
                if (category != null)
                {
                    var categories = categoryService.GetSubCategories(category.Id);
                    unitOfWork.Commit();
                    return Json(categories.Select(e => e.Name).ToList());
                }
                else
                {
                    unitOfWork.Rollback();
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
            var context = Factory.GetContext();
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

            unitOfWork.Commit();

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateLot(CreateLotViewModel model)
        {
            model.Categories = new HashSet<Category>();
            model.Currencies = new HashSet<string>();
            if (ModelState.IsValid)
            {
                try
                {
                    var context = Factory.GetContext();
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

                    unitOfWork.Commit();

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
            var context = Factory.GetContext();
            var unitOfWork = new UnitOfWork(context);
            var lotService = new LotService(unitOfWork, unitOfWork);
            var lot = lotService.GetLotById(id);

            if (lot == null)
            {
                unitOfWork.Commit();
                return RedirectToAction("Index", "Home");
                //TODO: Need to implement helper.
            }

            var mapper = new LotMapper();
            var viewModel = mapper.MapEntityToViewModel(lot);

            unitOfWork.Commit();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ViewLot(double BidValue, string bidEmail, long lotId)
        {
            var context = Factory.GetContext();
            var unitOfWork = new UnitOfWork(context);
            try
            {
                var membershipService = new MembershipService(unitOfWork, unitOfWork);
                var lotService = new LotService(unitOfWork, unitOfWork);
                var bidService = new BidService(unitOfWork, unitOfWork);

                var user = membershipService.GetUserByEmail(bidEmail);
                var lot = lotService.GetLotById(lotId);
                bidService.CreateBid(BidValue, user.Id, lotId, lot.Currency.Id);
                lot.CurrentPrice = BidValue;
                lotService.UpdateLot(lot);

                unitOfWork.Commit();

                return RedirectToAction("ViewLot", new { id = lotId });
            }
            catch (MembershipServiceException e)
            {
                return RedirectToAction("ViewLot", new { id = lotId });
            }
            catch (BidServiceException e)
            {
                return RedirectToAction("ViewLot", new { id = lotId });
            }
        }

        [HttpPost]
        public ActionResult GetCommentPartial(CommentViewModel model)
        {
            var context = Factory.GetContext();
            var unitOfWork = new UnitOfWork(context);
            try
            {
                var commentService = new CommentService(unitOfWork, unitOfWork);
                var membershipService = new MembershipService(unitOfWork, unitOfWork);

                var user = membershipService.GetUserByEmail(model.UserMail);
                commentService.CreateComment(model.Description, user.Id, model.LotId);
                var comments = commentService.GetCommentsByLotId(model.LotId);
                unitOfWork.Commit();

                var viewModel = new CommentViewModel { Comments = new HashSet<Comment>(comments), LotId = model.LotId };

                return PartialView("_CommentPartial", viewModel);
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
                throw;
            }
        }
    }
}