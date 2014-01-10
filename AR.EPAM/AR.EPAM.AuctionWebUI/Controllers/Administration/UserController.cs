using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AR.EPAM.AuctionWebUI.IoC;
using AR.EPAM.AuctionWebUI.Mappings;
using AR.EPAM.AuctionWebUI.Models.AdministrationViewModels;
using AR.EPAM.Core.Entities.Membership;
using AR.EPAM.EFData;
using AR.EPAM.Services.MembershipServices;

namespace AR.EPAM.AuctionWebUI.Controllers.Administration
{
    public class UserController : Controller
    {
        [HttpGet]
        [AttributeRouting.Web.Mvc.Route("admin/users")]
        public ActionResult Users()
        {
            var context = Factory.GetContext();
            var unitOfWork = new UnitOfWork(context);
            var membershipService = new MembershipService(unitOfWork, unitOfWork);

            var viewModel = new IndexUserViewModel();
            var criteries = new HashSet<string> { "UserName", "Email" };
            var users = membershipService.GetLastRegisteredUsers(int.Parse(Resources.CountOfUsers));

            unitOfWork.Commit();

            viewModel.Criteria = criteries;
            viewModel.UsersPartialViewModel.Users = new HashSet<User>(users);
            return View(viewModel);
        }

        [HttpGet]
        [AttributeRouting.Web.Mvc.Route("admin/users/{userId:int}")]
        public ActionResult UsersById(int userId)
        {

            var context = Factory.GetContext();
            var unitOfWork = new UnitOfWork(context);
            var membershipService = new MembershipService(unitOfWork, unitOfWork);
            var profileService = new ProfileService(unitOfWork, unitOfWork);
            var mapper = new UserProfileMapper();

            var user = membershipService.GetUserById(userId);

            if (user == null)
            {
                unitOfWork.Commit();
                return RedirectToAction("Users");
            }

            var profile = profileService.GetProfileByUserId(user.Id);
            AdminUserViewModel viewModel = profile == null ? mapper.MapEntityWithoutProfile(user) : mapper.MapEntitiesToViewModel(user, profile);

            unitOfWork.Commit();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult UsersById(AdminUserViewModel model)
        {
            var context = Factory.GetContext();
            var unitOfWork = new UnitOfWork(context);
            var membershipService = new MembershipService(unitOfWork, unitOfWork);
            var profileService = new ProfileService(unitOfWork, unitOfWork);
            var mapper = new UserProfileMapper();

            var user = membershipService.GetUserById(model.UserId);
            var profile = profileService.GetProfileByUserId(user.Id);
            var role = membershipService.GetRoleByName(Resources.Administrator);

            if (profile == null)
            {
                profile = profileService.CreateProfile(model.Name, model.Surname, model.Patronymic, model.City,
                    model.PhoneNumber, model.UserId);
            }
            else
            {
                mapper.UpdateProfile(profile, model);
                profileService.UpdateProfile(profile);
            }

            if (model.IsAdministrator)
            {
                if (user.Roles.Select(e => e.Name).Contains(Resources.Administrator))
                {
                    unitOfWork.Commit();
                    return null;
                }
                else
                {
                    user.Roles.Add(role);
                    unitOfWork.Commit();
                    return null;
                }
            }
            else
            {
                if (user.Roles.Select(e => e.Name).Contains(Resources.Administrator))
                {
                    user.Roles.Remove(role);
                    unitOfWork.Commit();
                    return null;
                }
                else
                {
                    unitOfWork.Commit();
                    return null;
                }
            }
        }

        [HttpPost]
        public ActionResult UsersPartial(string findString, string selectedCriterion)
        {
            var context = Factory.GetContext();
            var unitOfWork = new UnitOfWork(context);
            var membershipService = new MembershipService(unitOfWork, unitOfWork);

            List<User> users;
            var viewModel = new UsersPartialViewModel();

            switch (selectedCriterion)
            {
                case "Email":
                    {
                        var user = membershipService.GetUserByEmail(findString);
                        if (user == null)
                        {
                            unitOfWork.Commit();
                            return null;
                        }

                        users = new List<User> { user };
                        viewModel.Users = new HashSet<User>(users);
                        unitOfWork.Commit();

                        break;
                    }
                case "UserName":
                    {
                        users = membershipService.GetUsersByUserNameContaining(findString);
                        if (users.Count == 0)
                        {
                            unitOfWork.Commit();
                            return null;
                        }

                        viewModel.Users = new HashSet<User>(users);
                        unitOfWork.Commit();

                        break;
                    }
            }

            return PartialView("_UsersPartial", viewModel);
        }
    }
}