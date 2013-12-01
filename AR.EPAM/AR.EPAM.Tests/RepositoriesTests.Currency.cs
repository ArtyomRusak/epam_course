using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core.Entities.Auction;
using AR.EPAM.Core.Entities.Membership;
using AR.EPAM.EFData.EFContext;
using NUnit.Framework;

namespace AR.EPAM.Tests
{
    [TestFixture]
    public partial class RepositoriesTests
    {
        [Test]
        public void ShouldAddCurrencyToDatabase()
        {
            AuctionContext context = new AuctionContext();
            Database.SetInitializer(new DropCreateDatabaseAlways<AuctionContext>());
            context.Roles.Add(new Role() {Name = "Admin"});
            context.SaveChanges();
            context.Dispose();
        }
    }
}