using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AR.EPAM.EFData.EFContext;
using Autofac;
using Ninject;

namespace AR.EPAM.AuctionWebUI.IoC
{
    public static class ContextFactory
    {
        public static IContainer DependencyContainer { get; private set; }
        public static IKernel DependencyContainerKernel { get; private set; }

        public static void Configure()
        {
            DependencyContainer = Dependencies.Configure();
            DependencyContainerKernel = Dependencies.ConfigureNinject();
        }

        public static AuctionContext GetContext()
        {
            return DependencyContainer.Resolve<AuctionContext>();
        }

        public static AuctionContext GetContextKernel()
        {
            return DependencyContainerKernel.Get<AuctionContext>();
        }
    }
}