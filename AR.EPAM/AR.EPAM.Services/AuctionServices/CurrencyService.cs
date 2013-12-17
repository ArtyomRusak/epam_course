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
    public class CurrencyService : IService
    {
        #region [Private members]

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryFactory _factoryOfRepositories;

        #endregion


        #region [Ctor's]

        public CurrencyService(IUnitOfWork unitOfWork, IRepositoryFactory factoryOfRepositories)
        {
            Guard.AgainstNullReference(unitOfWork, "unitOfWork");
            Guard.AgainstNullReference(factoryOfRepositories, "factoryOfRepositories");

            _unitOfWork = unitOfWork;
            _factoryOfRepositories = factoryOfRepositories;
        }

        #endregion

        #region [CurrencyService's members]

        public Currency AddCurrency(string value)
        {
            var currencyRepository = _factoryOfRepositories.GetCurrencyRepository();
            var currency = currencyRepository.Find(e => e.Value == value);
            if (currency != null)
            {
                throw new CurrencyServiceException("Currency is currently used.");
            }
            currency = new Currency { Value = value };
            currencyRepository.Create(currency);
            try
            {
                _unitOfWork.PreSave();
            }
            catch (Exception e)
            {
                throw new ServiceException(e);
            }
            return currency;
        }

        public void UpdateCurrency(Currency currency)
        {
            var currencyRepository = _factoryOfRepositories.GetCurrencyRepository();
            try
            {
                currencyRepository.Update(currency);
            }
            catch (ArgumentNullException ex)
            {
                throw new CurrencyServiceException(ex.Message);
            }
            catch (Exception e)
            {
                throw new CurrencyServiceException(e);
            }
        }

        public void RemoveCurrency(Currency currency)
        {
            var currencyRepository = _factoryOfRepositories.GetCurrencyRepository();
            currencyRepository.Remove(currency);
        }

        #endregion
    }
}
