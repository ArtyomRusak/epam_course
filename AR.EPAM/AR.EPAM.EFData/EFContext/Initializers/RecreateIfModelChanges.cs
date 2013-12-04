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
    public class RecreateIfModelChanges : IDatabaseInitializer<AuctionContext>
    {
        #region Implementation of IDatabaseInitializer<in AuctionContext>

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

        #endregion

        #region [RecreateIfModelChanges's members]

        protected void Seed(AuctionContext context)
        {
            context.Roles.Add(new Role() {Name = "User"});
            context.Roles.Add(new Role() {Name = "Administrator"});
        }

        #endregion
    }
}
