using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AR.EPAM.Core;
using AR.EPAM.EFData.EFContext;
using AR.EPAM.Services;
using Ninject;

namespace AR.EPAM.AuctionWebUI.IoC
{
    public static class ContextFactory
    {
        public static IKernel DependencyContainer { get; private set; }

        public static void Configure()
        {
            DependencyContainer = Dependencies.ConfigureNinject();
        }

        public static AuctionContext GetContextKernel()
        {
            return DependencyContainer.Get<AuctionContext>();
        }

        public static IUnitOfWork GetUnitOfWork()
        {
            return DependencyContainer.Get<IUnitOfWork>();
        }

        public static IService GetService<TService>() where TService : IService
        {
            return DependencyContainer.Get<TService>();
        }
    }
}