using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AR.EPAM.AuctionWebUI.IoC;
using AR.EPAM.AuctionWebUI.Models.MembershipViewModels;
using AR.EPAM.EFData;
using AR.EPAM.Services.Exceptions;
using AR.EPAM.Services.MembershipServices;

namespace AR.EPAM.AuctionWebUI.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        [Route("register")]
        public ActionResult Register()
        {
            var model = new RegisterUserViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var context = Factory.GetContext();
                var unitOfWork = new UnitOfWork(context);
                var membershipService = new MembershipService(unitOfWork, unitOfWork);
                try
                {
                    var user = membershipService.RegisterUser(model.Email, model.UserName, model.Password);
                    unitOfWork.Commit();
                    return RedirectToAction("Login");
                }
                catch (MembershipServiceException e)
                {
                    if (e.Message == "User is registered.")
                    {
                        ModelState.AddModelError("Exist", e.Message);
                        return View(model);
                    }
                    else
                    {
                        ModelState.AddModelError("Email", Resources.EmailExist);
                        return View(model);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("Error", "Unknown error");
                return View(model);
            }
        }

        [HttpGet]
        [Route("login")]
        public ActionResult Login()
        {
            var model = new LoginUserViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var context = Factory.GetContext();
                    var unitOfWork = new UnitOfWork(context);
                    var membershipService = new MembershipService(unitOfWork, unitOfWork);
                    var loginUser = membershipService.LoginUser(model.Email, model.Password);
                    if (loginUser == null)
                    {
                        ModelState.AddModelError("NullUser", Resources.NullUser);
                        return View(model);
                    }

                    var roles = loginUser.Roles.Select(e => e.Name).ToList();
                    string rolesToString = roles.Aggregate("", (current, role) => current + String.Format("{0},", role));

                    unitOfWork.Commit();

                    var ticket = new FormsAuthenticationTicket(3, loginUser.Email, DateTime.Now,
                        DateTime.Now.AddMinutes(20), model.RememberMe, rolesToString);
                    var cookieString = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieString);
                    if (ticket.IsPersistent)
                    {
                        cookie.Expires = ticket.Expiration;
                    }
                    Response.Cookies.Add(cookie);

                    string returnUrl = HttpContext.Request.QueryString["ReturnUrl"];
                    if (returnUrl == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(returnUrl);
                }
                catch (MembershipServiceException e)
                {
                    if (e.Message == "Wrong password.")
                    {
                        ModelState.AddModelError("Password", Resources.WrongPassword);
                        return View(model);
                    }
                    else
                    {
                        ModelState.AddModelError("Ex", e.Message);
                        return View(model);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("Error", "Unknown exception");
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}