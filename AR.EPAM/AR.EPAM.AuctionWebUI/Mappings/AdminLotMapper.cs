using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AR.EPAM.AuctionWebUI.Models.AdministrationViewModels;
using AR.EPAM.AuctionWebUI.Models.AuctionViewModels;
using AR.EPAM.Core.Entities.Auction;

namespace AR.EPAM.AuctionWebUI.Mappings
{
    public class AdminLotMapper : IMapper<Lot, AdminLotViewModel>
    {
        public AdminLotViewModel MapEntityToViewModel(Lot entity)
        {
            var viewModel = new AdminLotViewModel
            {
                Category = entity.Category,
                CreateDate = entity.CreateDate,
                Currency = entity.Currency,
                CurrentPrice = entity.CurrentPrice,
                StartPrice = entity.StartPrice,
                Description = entity.Description,
                DurationInDays = entity.DurationInDays,
                LotId = entity.Id,
                Name = entity.Name,
                Owner = entity.Owner,
                BidsViewModel = new BidsViewModel
                {
                    Bids = new HashSet<Bid>(entity.Bids)
                }
            };

            return viewModel;
        }

        public Lot MapViewModelToEntity(AdminLotViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public void UpdateLot(Lot entity, string name, string description, Category category)
        {
            entity.Name = name;
            entity.Description = description;
            if (category != null)
            {
                entity.CategoryId = category.Id;
            }
        }
    }
}