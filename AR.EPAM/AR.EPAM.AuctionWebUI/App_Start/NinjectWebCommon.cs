using System.Web.Mvc;
using AR.EPAM.Core;
using AR.EPAM.EFData;
using AR.EPAM.EFData.EFContext;
using AR.EPAM.Services.AuctionServices;
using AR.EPAM.Services.MembershipServices;

[assembly: WebActivator.PreApplicationStartMethod(typeof(AR.EPAM.AuctionWebUI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(AR.EPAM.AuctionWebUI.App_Start.NinjectWebCommon), "Stop")]

namespace AR.EPAM.AuctionWebUI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static IKernel GetKernel()
        {
            return bootstrapper.Kernel;
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<AuctionContext>()
                .ToSelf()
                .InRequestScope()
                .WithConstructorArgument("connectionString", Resources.ConnectionString);

            //kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            //kernel.Bind<IRepositoryFactory>().To<UnitOfWork>();
            //kernel.Bind<UnitOfWork>().ToSelf().InRequestScope();

            //kernel.Bind<ProfileService>()
            //    .ToSelf()
            //    .WithConstructorArgument("unitOfWork", kernel.Get<UnitOfWork>())
            //    .WithConstructorArgument("factoryOfRepositories", kernel.Get<UnitOfWork>());
            //kernel.Bind<MembershipService>()
            //    .ToSelf()
            //    .WithConstructorArgument("unitOfWork", kernel.Get<UnitOfWork>())
            //    .WithConstructorArgument("factoryOfRepositories", kernel.Get<UnitOfWork>());
            //kernel.Bind<BidService>()
            //    .ToSelf()
            //    .WithConstructorArgument("unitOfWork", kernel.Get<UnitOfWork>())
            //    .WithConstructorArgument("factoryOfRepositories", kernel.Get<UnitOfWork>());
            //kernel.Bind<CategoryService>()
            //    .ToSelf()
            //    .WithConstructorArgument("unitOfWork", kernel.Get<UnitOfWork>())
            //    .WithConstructorArgument("factoryOfRepositories", kernel.Get<UnitOfWork>());
            //kernel.Bind<CommentService>()
            //    .ToSelf()
            //    .WithConstructorArgument("unitOfWork", kernel.Get<UnitOfWork>())
            //    .WithConstructorArgument("factoryOfRepositories", kernel.Get<UnitOfWork>());
            //kernel.Bind<CurrencyService>()
            //    .ToSelf()
            //    .WithConstructorArgument("unitOfWork", kernel.Get<UnitOfWork>())
            //    .WithConstructorArgument("factoryOfRepositories", kernel.Get<UnitOfWork>());
            //kernel.Bind<LotService>()
            //    .ToSelf()
            //    .WithConstructorArgument("unitOfWork", kernel.Get<UnitOfWork>())
            //    .WithConstructorArgument("factoryOfRepositories", kernel.Get<UnitOfWork>());
        }
    }
}
