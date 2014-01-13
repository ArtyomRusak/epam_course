using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core.Entities.Auction;
using AR.EPAM.Core.Entities.Membership;

namespace AR.EPAM.EFData.EFContext.Initializers
{
    public class RecreateAlways:IDatabaseInitializer<AuctionContext>
    {
        public void InitializeDatabase(AuctionContext context)
        {
            context.Database.Delete();
            context.Database.Create();
            context.Database.ExecuteSqlCommand("ALTER TABLE Users ADD CONSTRAINT EmailDataUnique UNIQUE (Email)");
            context.Database.ExecuteSqlCommand("ALTER TABLE Users ADD CONSTRAINT NameDataUnique UNIQUE (UserName)");
            context.Database.ExecuteSqlCommand("ALTER TABLE Roles ADD CONSTRAINT RoleNameDataUnique UNIQUE (Name)");
            Seed(context);
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Seed(AuctionContext context)
        {
            var roles = new List<Role>
            {
                new Role { Name = "Member" },
                new Role { Name = "Administrator" }
            };
            roles.ForEach(e => context.Roles.Add(e));

            var currencies = new List<Currency>
            {
                new Currency {Value = "USD"},
                new Currency {Value = "BYR"}
            };
            currencies.ForEach(e => context.Currencies.Add(e));

            var fashion = new Category { Name = "Fashion", ParentCategoryId = null };
            context.Categories.Add(fashion);
            context.SaveChanges();
            var menFashion = new Category { Name = "Men", ParentCategoryId = fashion.Id };
            var womenFashion = new Category { Name = "Women", ParentCategoryId = fashion.Id };
            var kidFashion = new Category { Name = "Kids", ParentCategoryId = fashion.Id };

            var sportingGoods = new Category { Name = "Sporting Goods", ParentCategoryId = null };
            context.Categories.Add(sportingGoods);
            context.SaveChanges();
            var indoorGames = new Category { Name = "Indoor Games", ParentCategoryId = sportingGoods.Id };
            var outdoorGames = new Category { Name = "Outdoor Games", ParentCategoryId = sportingGoods.Id };

            var electronics = new Category { Name = "Electronics", ParentCategoryId = null };
            context.Categories.Add(electronics);
            context.SaveChanges();
            var computersElectronics = new Category { Name = "Computers", ParentCategoryId = electronics.Id };
            var camerasElectronics = new Category { Name = "Cameras", ParentCategoryId = electronics.Id };
            var mobilePhonesElectonics = new Category { Name = "Mobile Phones", ParentCategoryId = electronics.Id };

            var home = new Category { Name = "Home", ParentCategoryId = null };
            context.Categories.Add(home);
            context.SaveChanges();
            var decorHome = new Category { Name = "Home Decor", ParentCategoryId = home.Id };
            var beddingHome = new Category { Name = "Bedding", ParentCategoryId = home.Id };
            var furnitureHome = new Category { Name = "Furniture", ParentCategoryId = home.Id };

            var categories = new List<Category>
            {
                menFashion,
                womenFashion,
                kidFashion,
                indoorGames,
                outdoorGames,
                computersElectronics,
                camerasElectronics,
                mobilePhonesElectonics,
                decorHome,
                beddingHome,
                furnitureHome
            };
            categories.ForEach(e => context.Categories.Add(e));

            var user = new User
            {
                Email = "admin@auction.com",
                PasswordSalt = "admin",
                Password = ("admin" + "admin").GetHashCode(),
                RegisterDate = DateTime.Now,
                Roles = new HashSet<Role>(roles),
                UserName = "Administrator"
            };
            context.Users.Add(user);

            context.SaveChanges();
        }
    }
}
