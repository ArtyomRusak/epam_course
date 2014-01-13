using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core.Entities.Auction;
using AR.EPAM.Core.Entities.Membership;
using AR.EPAM.EFData;
using AR.EPAM.EFData.EFContext;
using AR.EPAM.EFData.EFContext.Initializers;
using AR.EPAM.Services.MembershipServices;
using FluentAssertions;
using NUnit.Framework;

namespace AR.EPAM.Tests
{
    [TestFixture]
    public partial class ServiceTests
    {
        [Test]
        public void ShouldRegisterUser()
        {
            var context = new AuctionContext("AuctionTestNotebook");
            var unitOfWork = new UnitOfWork(context);
            var membershipService = new MembershipService(unitOfWork, unitOfWork);

            var user = membershipService.RegisterUser("email@email.com", "Artyom", "123456");
            unitOfWork.Dispose();
            user.Should().NotBeNull();
        }

        [Test]
        public void ShouldLoginUser()
        {
            var context = new AuctionContext("AuctionTestNotebook");
            var unitOfWork = new UnitOfWork(context);
            var membershipService = new MembershipService(unitOfWork, unitOfWork);

            var loginUser = membershipService.LoginUser("admin@auction.com", "admin");
            unitOfWork.Dispose();
            loginUser.Should().NotBeNull();
        }

        [Test]
        public void ShouldNotLoginUser()
        {
            var context = new AuctionContext("AuctionTestNotebook");
            var unitOfWork = new UnitOfWork(context);
            var membershipService = new MembershipService(unitOfWork, unitOfWork);

            var loginUser = membershipService.LoginUser("admin123@auction.com", "admin");
            unitOfWork.Dispose();
            loginUser.Should().BeNull();
        }

        [Test]
        public void ShouldBeInRoleAdministrator()
        {
            var context = new AuctionContext("AuctionTestNotebook");
            var unitOfWork = new UnitOfWork(context);
            var membershipService = new MembershipService(unitOfWork, unitOfWork);

            var role = membershipService.GetRoleByName("Administrator");
            var user = membershipService.GetUserByEmail("admin@auction.com");
            user.Roles.Contains(role).Should().BeTrue();
            unitOfWork.Dispose();
        }
    }
}