using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AR.EPAM.EFData.EFContext;
using Autofac;
using Ninject;
using Ninject.Web.Common;

namespace AR.EPAM.AuctionWebUI.IoC
{
    public static class Dependencies
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.Register(x => new AuctionContext(Resources.ConnectionString)).InstancePerLifetimeScope();

            return builder.Build();
        }

        public static IKernel ConfigureNinject()
        {
            var kernel = new StandardKernel();

            kernel.Bind<AuctionContext>()
                .ToSelf()
                .InRequestScope()
                .WithConstructorArgument("connectionString", Resources.ConnectionString);

            return kernel;
        }
    }
}