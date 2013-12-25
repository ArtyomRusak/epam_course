using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AR.EPAM.AuctionWebUI.Mappings;
using AR.EPAM.AuctionWebUI.Models;
using AR.EPAM.EFData;
using AR.EPAM.EFData.EFContext;
using AR.EPAM.Services.Exceptions;
using AR.EPAM.Services.MembershipServices;
using AttributeRouting.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace AR.EPAM.AuctionWebUI.Controllers
{
    [Authorize(Roles = "Member")]
    public class MembershipController : Controller
    {
        [HttpGet]
        [AttributeRouting.Web.Mvc.Route("YourPage")]
        public ActionResult UserPage(string email)
        {
            var context = new AuctionContext(Resources.ConnectionString);
            var unitOfWork = new UnitOfWork(context);
            var membershipService = new MembershipService(unitOfWork, unitOfWork);

            var user = membershipService.GetUserByEmail(email);
            var mapper = new UserMapper();
            var viewModel = mapper.MapEntityToViewModel(user);

            unitOfWork.Dispose();

            return View(viewModel);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult EditProfileChild()
        {
            var context = new AuctionContext(Resources.ConnectionString);
            var unitOfWork = new UnitOfWork(context);
            var membershipService = new MembershipService(unitOfWork, unitOfWork);
            var user = membershipService.GetUserByEmail(HttpContext.User.Identity.Name);
            unitOfWork.Dispose();
            return RedirectToAction("EditProfile", "Membership", new { username = user.UserName });
        }

        [HttpGet]
        [AttributeRouting.Web.Mvc.Route("{username}/profile")]
        public ActionResult EditProfile(string username)
        {
            var context = new AuctionContext(Resources.ConnectionString);
            var unitOfWork = new UnitOfWork(context);
            var membershipService = new MembershipService(unitOfWork, unitOfWork);

            var user = membershipService.GetUserByUserName(username);

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var mapper = new ProfileMapper();
            var profileService = new ProfileService(unitOfWork, unitOfWork);
            var profile = profileService.GetProfileByUserId(user.Id);

            unitOfWork.Dispose();

            if (profile == null)
            {
                var profileViewModel = new ProfileViewModel();
                return View(profileViewModel);
            }
            else
            {
                var profileViewModel = mapper.MapEntityToViewModel(profile);
                return View(profileViewModel);
            }
        }

        [HttpPost]
        public ActionResult EditProfile(ProfileViewModel model)
        {
            var context = new AuctionContext(Resources.ConnectionString);
            var unitOfWork = new UnitOfWork(context);
            var profileService = new ProfileService(unitOfWork, unitOfWork);
            var membershipService = new MembershipService(unitOfWork, unitOfWork);
            var user = membershipService.GetUserByEmail(model.Email);

            if (!model.Exist)
            {
                try
                {
                    profileService.CreateProfile(model.Name, model.Surname, model.Patronymic, model.City, model.PhoneNumber,
                        user.Id);
                }
                catch (ServiceException e)
                {
                    throw;
                }
                unitOfWork.Dispose();
            }
            else
            {
                var profile = profileService.GetProfileByUserId(user.Id);
                var mapper = new ProfileMapper();
                mapper.UpdateProfile(model, profile);
                profileService.UpdateProfile(profile);
                unitOfWork.Dispose();
            }

            //cause using ajax
            return null;
        }
    }
}