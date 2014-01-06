using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AR.EPAM.Core;
using AR.EPAM.Core.Entities.Membership;
using AR.EPAM.EFData.EFContext;
using AR.EPAM.Services;
using AR.EPAM.Services.MembershipServices;
using Ninject;

namespace AR.EPAM.AuctionWebUI.IoC
{
    public static class Factory
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

        public static MembershipService GetMembershipService()
        {
            return DependencyContainer.Get<MembershipService>();
        }

        public static ProfileService GetProfileService()
        {
            return DependencyContainer.Get<ProfileService>();
        }
    }
}