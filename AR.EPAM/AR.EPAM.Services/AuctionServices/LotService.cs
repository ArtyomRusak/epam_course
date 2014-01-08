using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AR.EPAM.Core;
using AR.EPAM.Core.Entities.Auction;
using AR.EPAM.Core.Entities.Membership;
using AR.EPAM.Infrastructure.Guard;
using AR.EPAM.Services.Exceptions;

namespace AR.EPAM.Services.AuctionServices
{
    public class LotService : IService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryFactory _factoryOfRepositories;

        public LotService(IUnitOfWork unitOfWork, IRepositoryFactory factoryOfRepositories)
        {
            Guard.AgainstNullReference(unitOfWork, "unitOfWork");
            Guard.AgainstNullReference(factoryOfRepositories, "factoryOfRepositories");

            _unitOfWork = unitOfWork;
            _factoryOfRepositories = factoryOfRepositories;
        }

        public Lot CreateLot(string name, double startPrice, int duration, string description, int currencyId,
            int ownerId, int categoryId)
        {
            var lot = new Lot
            {
                Name = name,
                StartPrice = startPrice,
                CurrentPrice = startPrice,
                CreateDate = DateTime.Now,
                Description = description,
                DurationInDays = duration,
                CurrencyId = currencyId,
                OwnerId = ownerId,
                SectionId = categoryId
            };

            var lotRepository = _factoryOfRepositories.GetLotRepository();
            lotRepository.Create(lot);
            try
            {
                _unitOfWork.PreSave();
            }
            catch (Exception e)
            {
                throw new ServiceException(e);
            }

            return lot;
        }

        public void UpdateLot(Lot lot)
        {
            var lotRepository = _factoryOfRepositories.GetLotRepository();
            try
            {
                lotRepository.Update(lot);
            }
            catch (Exception ex)
            {
                throw new LotServiceException(ex.Message);
            }
        }

        public void RemoveLot(Lot lot)
        {
            var lotRepository = _factoryOfRepositories.GetLotRepository();
            lotRepository.Remove(lot);
        }

        public Lot GetLotById(long lotId)
        {
            var lotRepository = _factoryOfRepositories.GetLotRepository();
            try
            {
                return lotRepository.GetEntityById(lotId);
            }
            catch (Exception e)
            {
                throw new LotServiceException(e.Message);
            }
        }

        public List<Lot> GetLotsByOwnderId(int ownerId)
        {
            var lotRepository = _factoryOfRepositories.GetLotRepository();
            try
            {
                return lotRepository.Filter(e => e.OwnerId == ownerId).ToList();
            }
            catch (Exception e)
            {
                throw new LotServiceException(e.Message);
            }
        }

        //public List<Lot> GetLotsBySectionId(int sectionId)
        //{
        //    var lotRepository = _factoryOfRepositories.GetLotRepository();
        //    var sectionRepository = _factoryOfRepositories.GetSectionRepository();
        //    //return lotRepository.Filter(e => e.SectionId == sectionId).ToList();
        //    var section = sectionRepository.GetEntityById(sectionId);
        //}

        public List<Lot> GetListOfLotsWhereUserTakeComments(int userId)
        {
            var commentRepository = _factoryOfRepositories.GetCommentRepository();
            return commentRepository.All().Where(e => e.UserId == userId).Select(e => e.Lot).Distinct().ToList();
        }

        public List<Lot> GetLotsWhereUserTakeBid(int userId)
        {
            var lotRepository = _factoryOfRepositories.GetLotRepository();
            try
            {
                return lotRepository.All().Where(w => w.Bids.Select(e => e.UserId).Contains(userId)).Distinct().ToList();
            }
            catch (Exception ex)
            {
                throw new LotServiceException(ex.Message);
            }
        } 
    }
}
