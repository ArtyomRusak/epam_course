using AR.EPAM.AuctionWebUI.Models;
using AR.EPAM.Core.Entities;

namespace AR.EPAM.AuctionWebUI.Mappings
{
    public interface IMapper<TEntity, TViewModel>
        where TEntity : Entity
        where TViewModel : ViewModel
    {
        TViewModel MapEntityToViewModel(TEntity entity);
        TEntity MapViewModelToEntity(TViewModel viewModel);
    }
}