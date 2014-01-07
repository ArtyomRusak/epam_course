using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AR.EPAM.AuctionWebUI.App_Start;
using AR.EPAM.Core;
using AR.EPAM.Core.Entities.Membership;
using AR.EPAM.EFData.EFContext;
using AR.EPAM.Services;
using AR.EPAM.Services.AuctionServices;
using AR.EPAM.Services.MembershipServices;
using Ninject;

namespace AR.EPAM.AuctionWebUI.IoC
{
    public static class Factory
    {
        public static IKernel DependencyContainer { get; private set; }

        public static void Configure()
        {
            DependencyContainer = NinjectWebCommon.GetKernel();
        }

        public static AuctionContext GetContext()
        {
            return DependencyContainer.Get<AuctionContext>();
        }

        //public static IUnitOfWork GetUnitOfWork()
        //{
        //    return DependencyContainer.Get<IUnitOfWork>();
        //}

        //public static MembershipService GetMembershipService()
        //{
        //    return DependencyContainer.Get<MembershipService>();
        //}

        //public static ProfileService GetProfileService()
        //{
        //    return DependencyContainer.Get<ProfileService>();
        //}

        //public static BidService GetBidService()
        //{
        //    return DependencyContainer.Get<BidService>();
        //}

        //public static CommentService GetCommentService()
        //{
        //    return DependencyContainer.Get<CommentService>();
        //}

        //public static CategoryService GetCategoryService()
        //{
        //    return DependencyContainer.Get<CategoryService>();
        //}

        //public static CurrencyService GetCurrencyService()
        //{
        //    return DependencyContainer.Get<CurrencyService>();
        //}

        //public static LotService GetLotService()
        //{
        //    return DependencyContainer.Get<LotService>();
        //}
    }
}