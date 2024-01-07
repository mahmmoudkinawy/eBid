using Search.Api.Models;

namespace Search.Api.Mappers;
public sealed class AuctionsMappers : Profile
{
    public AuctionsMappers()
    {
        CreateMap<AuctionCreated, ItemModel>();
        CreateMap<AuctionUpdated, ItemModel>();
    }
}
