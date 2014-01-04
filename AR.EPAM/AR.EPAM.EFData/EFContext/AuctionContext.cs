using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core.Entities.Auction;
using AR.EPAM.Core.Entities.Membership;
using AR.EPAM.EFData.EFContext.Mappings.Auction;
using AR.EPAM.EFData.EFContext.Mappings.Membership;

namespace AR.EPAM.EFData.EFContext
{
    public class AuctionContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Bid> Bids { get; set; }

        public AuctionContext(string connectionString)
            : base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new LotMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new CurrencyMap());
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new ProfileMap());
            modelBuilder.Configurations.Add(new BidMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
