using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AR.EPAM.AuctionWebUI.Models;
using AR.EPAM.Core.Entities.Auction;
using AR.EPAM.Core.Entities.Membership;

namespace AR.EPAM.AuctionWebUI.Mappings
{
    public class UserMapper : IMapper<User, UserViewModel>
    {
        public UserViewModel MapEntityToViewModel(User entity)
        {
            var userViewModel = new UserViewModel
            {
                Email = entity.Email,
                UserName = entity.UserName,
                Lots = (HashSet<Lot>) entity.Lots,
                Roles = (HashSet<Role>) entity.Roles
            };

            return userViewModel;
        }

        public User MapViewModelToEntity(UserViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}