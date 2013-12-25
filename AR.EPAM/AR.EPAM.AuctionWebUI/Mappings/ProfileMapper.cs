using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using AR.EPAM.AuctionWebUI.Models;
using AR.EPAM.Core.Entities.Membership;
using AR.EPAM.EFData;
using AR.EPAM.EFData.EFContext;
using AR.EPAM.Services.MembershipServices;

namespace AR.EPAM.AuctionWebUI.Mappings
{
    public class ProfileMapper : IMapper<Profile, ProfileViewModel>
    {
        public void UpdateProfile(ProfileViewModel viewModel, Profile profile)
        {
            profile.Name = viewModel.Name;
            profile.Surname = viewModel.Surname;
            profile.Patronymic = viewModel.Patronymic;
            profile.City = viewModel.City;
            profile.PhoneNumber = viewModel.PhoneNumber;
        }

        public ProfileViewModel MapEntityToViewModel(Profile entity)
        {
            var viewModel = new ProfileViewModel
            {
                City = entity.City,
                Name = entity.Name,
                Patronymic = entity.Patronymic,
                PhoneNumber = entity.PhoneNumber,
                Surname = entity.Surname,
                UserName = entity.User.UserName
            };

            return viewModel;
        }

        public Profile MapViewModelToEntity(ProfileViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}