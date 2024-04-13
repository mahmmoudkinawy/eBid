namespace Auctions.Api.Services;

public sealed class GrpcAuctionService : GrpcAuction.GrpcAuctionBase
{
	private readonly AuctionsDbContext _context;

	public GrpcAuctionService(AuctionsDbContext context)
	{
		_context = context;
	}

	public override async Task<GrpcAuctionResponse> GetAuction(GetAuctionRequest request, ServerCallContext context)
	{
		var auction =
			await _context.Auctions.FindAsync(Guid.Parse(request.Id)) ?? throw new RpcException(new Status(StatusCode.NotFound, "Not Found"));

		return new GrpcAuctionResponse
		{
			Auction = new GrpcAuctionModel
			{
				Id = auction.Id.ToString(),
				AuctionEnd = auction.AuctionEnd.ToString(),
				ReservePrice = auction.ReservePrice,
				Seller = auction.Seller
			}
		};
	}
}
