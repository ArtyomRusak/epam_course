using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AR.EPAM.AuctionWebUI.IoC;
using AR.EPAM.AuctionWebUI.Models.AdministrationViewModels;
using AR.EPAM.Core.Entities.Membership;
using AR.EPAM.EFData;
using AR.EPAM.Services.MembershipServices;
using AttributeRouting;
using AttributeRouting.Web.Mvc;
using Microsoft.Ajax.Utilities;

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

        [AttributeRouting.Web.Mvc.Route("admin/users/{userId}")]
        public ActionResult UsersById(int? userId)
        {
            return View();
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