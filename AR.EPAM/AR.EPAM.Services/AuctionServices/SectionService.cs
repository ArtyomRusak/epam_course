using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core;
using AR.EPAM.Core.Entities.Auction;
using AR.EPAM.Infrastructure.Guard;
using AR.EPAM.Services.Exceptions;

namespace AR.EPAM.Services.AuctionServices
{
    public class SectionService : IService
    {
        #region [Private members]

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryFactory _factoryOfRepositories;

        #endregion


        #region [Ctor's]

        public SectionService(IUnitOfWork unitOfWork, IRepositoryFactory factoryOfRepositories)
        {
            Guard.AgainstNullReference(unitOfWork, "unitOfWork");
            Guard.AgainstNullReference(factoryOfRepositories, "factoryOfRepositories");

            _unitOfWork = unitOfWork;
            _factoryOfRepositories = factoryOfRepositories;
        }

        #endregion

        #region [SectionService's exception]

        public List<Section> GetMainSections()
        {
            var sectionRepository = _factoryOfRepositories.GetSectionRepository();
            try
            {
                return sectionRepository.Filter(e => e.SectionId == null).ToList();
            }
            catch (Exception e)
            {
                throw new SectionServiceException(e.Message);
            }
        }

        public List<Section> GetSubSections(int sectionId)
        {
            var sectionRepository = _factoryOfRepositories.GetSectionRepository();
            try
            {
                return sectionRepository.Filter(e => e.SectionId == sectionId).ToList();
            }
            catch (Exception e)
            {
                throw new SectionServiceException(e.Message);
            }
        }

        #endregion
    }
}
