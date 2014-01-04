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
    public class CategoryService : IService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryFactory _factoryOfRepositories;

        public CategoryService(IUnitOfWork unitOfWork, IRepositoryFactory factoryOfRepositories)
        {
            Guard.AgainstNullReference(unitOfWork, "unitOfWork");
            Guard.AgainstNullReference(factoryOfRepositories, "factoryOfRepositories");

            _unitOfWork = unitOfWork;
            _factoryOfRepositories = factoryOfRepositories;
        }

        public List<Category> GetAllCategories()
        {
            var sectionRepository = _factoryOfRepositories.GetSectionRepository();
            return sectionRepository.All().ToList();
        }

        public List<Category> GetMainCategories()
        {
            var sectionRepository = _factoryOfRepositories.GetSectionRepository();
            try
            {
                return sectionRepository.Filter(e => e.ParentCategoryId == null).ToList();
            }
            catch (Exception e)
            {
                throw new CategoryServiceException(e.Message);
            }
        }

        public List<Category> GetSubCategories(int categoryId)
        {
            var sectionRepository = _factoryOfRepositories.GetSectionRepository();
            try
            {
                return sectionRepository.Filter(e => e.ParentCategoryId == categoryId).ToList();
            }
            catch (Exception e)
            {
                throw new CategoryServiceException(e.Message);
            }
        }

        public Category GetCategoryByName(string categoryName)
        {
            var sectionRepository = _factoryOfRepositories.GetSectionRepository();
            try
            {
                return sectionRepository.Filter(e => e.Name == categoryName).SingleOrDefault();
            }
            catch (Exception e)
            {
                throw new CategoryServiceException(e.Message);
            }
        }
    }
}
