namespace Auctions.Api.Mappers;
public sealed class AuctionsMappers : Profile
{
    public AuctionsMappers()
    {
        CreateMap<AuctionEntity, AuctionResponse>().IncludeMembers(a => a.Item);
        CreateMap<ItemEntity, AuctionResponse>();
        CreateMap<CreateAuctionRequest, AuctionEntity>()
            .ForMember(dest => dest.Item, opt => opt.MapFrom(src => src));
        CreateMap<CreateAuctionRequest, ItemEntity>();
        CreateMap<AuctionResponse, AuctionCreated>();
        CreateMap<AuctionEntity, AuctionUpdated>().IncludeMembers(a => a.Item);
        CreateMap<ItemEntity, AuctionUpdated>();
    }
}
