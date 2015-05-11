using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AR.EPAM.AuctionWebUI.Models;
using AR.EPAM.AuctionWebUI.Models.AuctionViewModels;
using AR.EPAM.Core.Entities.Auction;

namespace AR.EPAM.AuctionWebUI.Mappings
{
    public class LotMapper : IMapper<Lot, LotViewModel>
    {
        public LotViewModel MapEntityToViewModel(Lot entity)
        {
            var viewModel = new LotViewModel
            {
                Bids = new HashSet<Bid>(entity.Bids),
                Category = entity.Category,
                CreateDate = entity.CreateDate,
                Description = entity.Description,
                Currency = entity.Currency,
                CurrentPrice = entity.CurrentPrice,
                DurationInDays = entity.DurationInDays,
                Name = entity.Name,
                Owner = entity.Owner,
                StartPrice = entity.StartPrice,
                Id = entity.Id,
                PathToImage = entity.PathToImage,
                CommentViewModel = new CommentViewModel { Comments = new HashSet<Comment>(entity.Comments), LotId = entity.Id }
            };

            return viewModel;
        }

        public Lot MapViewModelToEntity(LotViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}