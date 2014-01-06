using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AR.EPAM.Core;
using AR.EPAM.EFData;
using AR.EPAM.EFData.EFContext;
using AR.EPAM.Services;
using AR.EPAM.Services.MembershipServices;
using Ninject;
using Ninject.Web.Common;

namespace AR.EPAM.AuctionWebUI.IoC
{
    public static class Dependencies
    {
        public static IKernel ConfigureNinject()
        {
            var kernel = new StandardKernel();

            kernel.Bind<AuctionContext>()
                .ToSelf()
                .WithConstructorArgument("connectionString", Resources.ConnectionString);

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<IRepositoryFactory>().To<UnitOfWork>();

            kernel.Bind<ProfileService>().ToSelf();
            kernel.Bind<MembershipService>().ToSelf();

            return kernel;
        }
    }
}