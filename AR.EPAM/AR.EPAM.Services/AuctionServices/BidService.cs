using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AR.EPAM.Core;
using AR.EPAM.Core.Entities.Auction;
using AR.EPAM.Core.Exceptions;
using AR.EPAM.Infrastructure.Guard;
using AR.EPAM.Services.Exceptions;

namespace AR.EPAM.Services.AuctionServices
{
    public class BidService : IService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryFactory _factoryOfRepositories;

        public BidService(IUnitOfWork unitOfWork, IRepositoryFactory factoryOfRepositories)
        {
            Guard.AgainstNullReference(unitOfWork, "unitOfWork");
            Guard.AgainstNullReference(factoryOfRepositories, "factoryOfRepositories");

            _unitOfWork = unitOfWork;
            _factoryOfRepositories = factoryOfRepositories;
        }

        public Bid CreateBid(double price, int userId, long lotId, int currencyId)
        {
            var bid = new Bid
            {
                Price = price,
                UserId = userId,
                LotId = lotId,
            };

            var bidRepository = _factoryOfRepositories.GetBidRepository();
            bidRepository.Create(bid);
            try
            {
                _unitOfWork.PreSave();
            }
            catch (Exception e)
            {
                throw new ServiceException(e);
            }
            return bid;
        }

        public Bid GetBidById(long bidId)
        {
            var bidRepository = _factoryOfRepositories.GetBidRepository();
            try
            {
                return bidRepository.GetEntityById(bidId);
            }
            catch (RepositoryException exception)
            {
                throw new BidServiceException(exception.Message);
            }
        }

        public List<Bid> GetBidsByUserId(int userId)
        {
            var bidRepository = _factoryOfRepositories.GetBidRepository();
            try
            {
                return bidRepository.Filter(e => e.UserId == userId).ToList();
            }
            catch (Exception e)
            {
                throw new BidServiceException(e.Message);
            }
        }

        public Bid GetBidWithHigherPrice()
        {
            var bidRepository = _factoryOfRepositories.GetBidRepository();
            try
            {
                return bidRepository.All().OrderBy(e => e.Price).First();
            }
            catch (Exception e)
            {
                throw new BidServiceException(e.Message);
            }
        }
    }
}
