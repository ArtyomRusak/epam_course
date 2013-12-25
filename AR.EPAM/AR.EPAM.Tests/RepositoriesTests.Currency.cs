using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core.Entities.Auction;
using AR.EPAM.Core.Entities.Membership;
using AR.EPAM.EFData.EFContext;
using AR.EPAM.EFData.EFContext.Initializers;
using NUnit.Framework;

namespace AR.EPAM.Tests
{
    [TestFixture]
    public partial class RepositoriesTests
    {
        [Test]
        public void ShouldAddCurrencyToDatabase()
        {
            AuctionContext context = new AuctionContext("AuctionTest");
            Database.SetInitializer(new AuctionInitializer());
            //context.Roles.Add(new Role() {Name = "Admin"});
            context.SaveChanges();
            context.Dispose();
        }
    }
}