using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AR.EPAM.Core.Entities.Membership;

namespace AR.EPAM.EFData.EFContext.Initializers
{
    public class AuctionInitializer : IDatabaseInitializer<AuctionContext>
    {
        public void InitializeDatabase(AuctionContext context)
        {
            bool databaseExists;
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                databaseExists = context.Database.Exists();
            }
            if (databaseExists)
            {
                if (context.Database.CompatibleWithModel(true))
                {
                    return;
                }
                context.Database.Delete();
            }
            context.Database.Create();
            context.Database.ExecuteSqlCommand("ALTER TABLE Users ADD CONSTRAINT EmailDataUnique UNIQUE (Email)");
            context.Database.ExecuteSqlCommand("ALTER TABLE Users ADD CONSTRAINT NameDataUnique UNIQUE (UserName)");
            context.Database.ExecuteSqlCommand("ALTER TABLE Roles ADD CONSTRAINT RoleNameDataUnique UNIQUE (Name)");
            try
            {
                context.SaveChanges();
                Seed(context);
            }
            catch (Exception ex)
            {
                context.Dispose();
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

            context.SaveChanges();
        }
    }
}
