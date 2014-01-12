﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AR.EPAM.AuctionWebUI.IoC;
using AR.EPAM.AuctionWebUI.Models.AdministrationViewModels;
using AR.EPAM.AuctionWebUI.Models.AuctionViewModels;
using AR.EPAM.Core.Entities.Auction;
using AR.EPAM.Core.Entities.Membership;
using AR.EPAM.EFData;
using AR.EPAM.Services.AuctionServices;
using AttributeRouting.Web.Mvc;
using WebGrease;

namespace AR.EPAM.AuctionWebUI.Controllers.Administration
{
    [Authorize(Roles = "Administrator")]
    public class LotController : Controller
    {
        [HttpGet]
        [AttributeRouting.Web.Mvc.Route("admin/lots")]
        public ActionResult Lots()
        {
            var context = Factory.GetContext();
            var unitOfWork = new UnitOfWork(context);
            var lotService = new LotService(unitOfWork, unitOfWork);
            var categoryService = new CategoryService(unitOfWork, unitOfWork);
            var currencyService = new CurrencyService(unitOfWork, unitOfWork);

            var categories = categoryService.GetMainCategories();
            var currencies = currencyService.GetAllCurrencies();
            var lots = lotService.GetLastCreatedLots(int.Parse(Resources.CountOfLots));

            var viewModel = new SearchLotViewModel
            {
                CategoriesViewModel = new CategoriesViewModel
                {
                    Categories = new HashSet<Category>(categories)
                },
                LotsPartialViewModel = new LotsPartialViewModel
                {
                    Lots = new HashSet<Lot>(lots)
                },
                Currencies = new HashSet<string>(currencies.Select(e => e.Value))
            };

            unitOfWork.Commit();

            return View(viewModel);
        }

        [HttpGet]
        [AttributeRouting.Web.Mvc.Route("admin/lots/{lotId:int}")]
        public ActionResult LotById(int lotId)
        {
            var context = Factory.GetContext();
            var unitOfWork = new UnitOfWork(context);
            var lotService = new LotService(unitOfWork, unitOfWork);

            var lot = lotService.GetLotById(lotId);
            if (lot == null)
            {
                //TODO: Add helper.
                unitOfWork.Commit();
                return RedirectToAction("Index", "Home");
            }



            return View();
        }

        [HttpPost]
        public ActionResult LotsPartial(SearchLotViewModel model, string selectedCategory)
        {
            var context = Factory.GetContext();
            var unitOfWork = new UnitOfWork(context);
            var lotService = new LotService(unitOfWork, unitOfWork);
            var categoryService = new CategoryService(unitOfWork, unitOfWork);
            var currencyService = new CurrencyService(unitOfWork, unitOfWork);

            var viewModel = new LotsPartialViewModel();
            //TODO: Need to implement method for all cases in services. This is not a good solution.
            var lots = lotService.GetAllLotsIQueryable();
            List<Lot> lotsToFind;
            var currency = currencyService.GetCurrencyByValue(model.SelectedCurrency);
            Category category = null;
            if (selectedCategory != null)
            {
                category = categoryService.GetCategoryByName(selectedCategory);
            }

            if ((model.MinBorder >= 0) && (model.MaxBorder > 0))
            {
                if (category != null)
                {
                    lotsToFind = lots.Where(
                        e =>
                            e.Name.Contains(model.Name) &&
                            (e.StartPrice >= model.MinBorder && e.StartPrice <= model.MaxBorder) &&
                            e.CategoryId == category.Id && e.CurrencyId == currency.Id).ToList();
                    viewModel.Lots = new HashSet<Lot>(lotsToFind);
                }
                else
                {
                    lotsToFind =
                        lots.Where(
                            e =>
                                e.Name.Contains(model.Name) &&
                                (e.StartPrice >= model.MinBorder && e.StartPrice <= model.MaxBorder) &&
                                e.CurrencyId == currency.Id).ToList();
                    viewModel.Lots = new HashSet<Lot>(lotsToFind);
                }
            }
            else
            {
                if (category != null)
                {
                    lotsToFind =
                        lots.Where(
                            e =>
                                e.Name.Contains(model.Name) && e.CategoryId == category.Id &&
                                e.CurrencyId == currency.Id).ToList();
                    viewModel.Lots = new HashSet<Lot>(lotsToFind);
                }
                else
                {
                    lotsToFind = lots.Where(e => e.Name.Contains(model.Name) && e.CurrencyId == currency.Id).ToList();
                    viewModel.Lots = new HashSet<Lot>(lotsToFind);
                }
            }

            unitOfWork.Commit();

            return PartialView("_LotsPartial", viewModel);
        }
    }
}