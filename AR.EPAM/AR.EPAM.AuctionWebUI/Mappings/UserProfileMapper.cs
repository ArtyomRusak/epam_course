using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AR.EPAM.AuctionWebUI.Models.AdministrationViewModels;
using AR.EPAM.Core.Entities.Auction;
using AR.EPAM.Core.Entities.Membership;

namespace AR.EPAM.AuctionWebUI.Mappings
{
    public class UserProfileMapper
    {
        public AdminUserViewModel MapEntitiesToViewModel(User user, Profile profile)
        {
            var viewModel = new AdminUserViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                RegisterDate = user.RegisterDate,
                Email = user.Email,
                Name = profile.Name,
                Surname = profile.Surname,
                Patronymic = profile.Patronymic,
                City = profile.City,
                PhoneNumber = profile.PhoneNumber,
                Lots = new HashSet<Lot>(user.Lots),
                Roles = new HashSet<Role>(user.Roles),
                IsAdministrator = user.Roles.Select(e => e.Name).Contains(Resources.Administrator)
            };

            return viewModel;
        }

        public AdminUserViewModel MapEntityWithoutProfile(User user)
        {
            var viewModel = new AdminUserViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                RegisterDate = user.RegisterDate,
                Email = user.Email,
                Lots = new HashSet<Lot>(user.Lots),
                Roles = new HashSet<Role>(user.Roles),
                IsAdministrator = user.Roles.Select(e => e.Name).Contains(Resources.Administrator)
            };

            return viewModel;
        }

        public void UpdateProfile(Profile profile, AdminUserViewModel model)
        {
            profile.Name = model.Name;
            profile.Surname = model.Surname;
            profile.Patronymic = model.Patronymic;
            profile.City = model.City;
            profile.PhoneNumber = model.PhoneNumber;
        }
    }
}