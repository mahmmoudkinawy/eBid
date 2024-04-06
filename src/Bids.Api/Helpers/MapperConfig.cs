namespace Bids.Api.Helpers;

public sealed class MapperConfig : Profile
{
	public MapperConfig()
	{
		CreateMap<BidEntity, BidResponse>();
		CreateMap<BidEntity, BidPlaced>();
	}
}
