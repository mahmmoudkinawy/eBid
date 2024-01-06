namespace SearchApi.Mappers;
public sealed class AuctionsMappers : Profile
{
    public AuctionsMappers()
    {
        CreateMap<AuctionCreated, ItemModel>();
    }
}
