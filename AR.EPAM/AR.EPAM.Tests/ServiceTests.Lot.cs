using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.EFData;
using AR.EPAM.EFData.EFContext;
using AR.EPAM.Services.AuctionServices;
using AR.EPAM.Services.MembershipServices;
using FluentAssertions;
using NUnit.Framework;

namespace AR.EPAM.Tests
{
    [TestFixture]
    public partial class ServiceTests
    {
        [Test]
        public void ShouldAddLotToDatabase()
        {
            var context = new AuctionContext("AuctionTestNotebook");
            var unitOfWork = new UnitOfWork(context);
            var lotService = new LotService(unitOfWork, unitOfWork);
            var currencyService = new CurrencyService(unitOfWork, unitOfWork);
            var membershipService = new MembershipService(unitOfWork, unitOfWork);

            var currency = currencyService.GetCurrencyByValue("USD");
            var user = membershipService.GetUserByEmail("admin@auction.com");

            var lot = lotService.CreateLot("name", 120, 5, "qfq", currency.Id, user.Id, 3);
            unitOfWork.Dispose();
            lot.Should().NotBeNull();
        }
    }
}
