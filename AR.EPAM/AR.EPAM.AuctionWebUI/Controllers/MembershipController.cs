using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AR.EPAM.AuctionWebUI.IoC;
using AR.EPAM.AuctionWebUI.Mappings;
using AR.EPAM.AuctionWebUI.Models;
using AR.EPAM.Core.Entities.Auction;
using AR.EPAM.EFData;
using AR.EPAM.EFData.EFContext;
using AR.EPAM.Services.AuctionServices;
using AR.EPAM.Services.Exceptions;
using AR.EPAM.Services.MembershipServices;
using AttributeRouting.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace AR.EPAM.AuctionWebUI.Controllers
{
    
    public class MembershipController : Controller
    {
        [HttpGet]
        [AttributeRouting.Web.Mvc.Route("profile/{username}")]
        public ActionResult ProfilePage(string username)
        {
            var context = Factory.GetContext();
            var unitOfWork = new UnitOfWork(context);
            var membershipService = new MembershipService(unitOfWork, unitOfWork);
            var profileService = new ProfileService(unitOfWork, unitOfWork);
            var lotService = new LotService(unitOfWork, unitOfWork);

            var user = membershipService.GetUserByUserName(username);
            if (user == null)
            {
                unitOfWork.Commit();
                return RedirectToAction("Index", "Home");
            }

            var mapper = new ProfileMapper();
            var lots = lotService.GetLotsByOwnderId(user.Id);
            var biddedLots = lotService.GetLotsWhereUserTakeBid(user.Id);

            
            var profile = profileService.GetProfileByUserId(user.Id);

            if (profile == null)
            {
                var viewModelNull = new ProfileViewModel
                {
                    Lots = new HashSet<Lot>(lots),
                    BiddedLots = new HashSet<Lot>(biddedLots)
                };

                unitOfWork.Commit();

                return View(viewModelNull);
            }

            var viewModel = mapper.MapEntityToViewModel(profile);
            viewModel.Lots = new HashSet<Lot>(lots);
            viewModel.BiddedLots = new HashSet<Lot>(biddedLots);

            unitOfWork.Commit();

            return View(viewModel);
        }

        [Authorize(Roles = "Member")]
        [HttpGet]
        [AttributeRouting.Web.Mvc.Route("YourPage")]
        public ActionResult UserPage()
        {
            var context = Factory.GetContext();
            var unitOfWork = new UnitOfWork(context);
            var membershipService = new MembershipService(unitOfWork, unitOfWork);

            var user = membershipService.GetUserByEmail(HttpContext.User.Identity.Name);
            var mapper = new UserMapper();
            var viewModel = mapper.MapEntityToViewModel(user);

            unitOfWork.Commit();

            return View(viewModel);
        }

        [Authorize(Roles = "Member")]
        [HttpGet]
        [AttributeRouting.Web.Mvc.Route("{username}/profile")]
        public ActionResult EditProfile(string username)
        {
            var context = Factory.GetContext();
            var unitOfWork = new UnitOfWork(context);
            var membershipService = new MembershipService(unitOfWork, unitOfWork);

            var user = membershipService.GetUserByUserName(username);

            if (user == null)
            {
                unitOfWork.Commit();
                return RedirectToAction("Index", "Home");
            }

            if (user.Email != HttpContext.User.Identity.Name)
            {
                unitOfWork.Commit();
                return RedirectToAction("Index", "Home");
            }

            var mapper = new ProfileMapper();
            var profileService = new ProfileService(unitOfWork, unitOfWork);
            var profile = profileService.GetProfileByUserId(user.Id);

            unitOfWork.Commit();

            if (profile == null)
            {
                unitOfWork.Commit();
                var profileViewModel = new ProfileViewModel();
                return View(profileViewModel);
            }
            else
            {
                var profileViewModel = mapper.MapEntityToViewModel(profile);
                return View(profileViewModel);
            }
        }

        [Authorize(Roles = "Member")]
        [HttpPost]
        public ActionResult EditProfile(ProfileViewModel model)
        {
            var context = Factory.GetContext();
            var unitOfWork = new UnitOfWork(context);
            var profileService = new ProfileService(unitOfWork, unitOfWork);
            var membershipService = new MembershipService(unitOfWork, unitOfWork);
            var user = membershipService.GetUserByEmail(HttpContext.User.Identity.Name);
            var profile = profileService.GetProfileByUserId(user.Id);

            if (profile == null)
            {
                try
                {
                    profileService.CreateProfile(model.Name, model.Surname, model.Patronymic, model.City,
                        model.PhoneNumber,
                        user.Id);
                    unitOfWork.Commit();
                }
                catch (ProfileServiceException ex)
                {
                    unitOfWork.Rollback();
                    throw;
                }
                catch (ServiceException e)
                {
                    unitOfWork.Rollback();
                    throw;
                }

            }
            else
            {
                var mapper = new ProfileMapper();
                mapper.UpdateProfile(model, profile);
                profileService.UpdateProfile(profile);
                unitOfWork.Commit();
            }

            //cause using ajax
            return null;
        }
    }
}