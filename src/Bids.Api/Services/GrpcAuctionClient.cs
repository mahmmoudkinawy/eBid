namespace Bids.Api.Services;

public sealed class GrpcAuctionClient
{
	private readonly ILogger<GrpcAuctionClient> _logger;
	private readonly IConfiguration _config;

	public GrpcAuctionClient(ILogger<GrpcAuctionClient> logger, IConfiguration config)
	{
		_logger = logger;
		_config = config;
	}

	public AuctionEntity GetAuction(string id)
	{
		_logger.LogInformation("Calling gRPC Service");

		var channel = GrpcChannel.ForAddress(_config["GrpcAuction"]);
		var client = new GrpcAuction.GrpcAuctionClient(channel);
		var request = new GetAuctionRequest { Id = id };

		try
		{
			var reply = client.GetAuction(request);
			return new AuctionEntity
			{
				ID = reply.Auction.Id,
				AuctionEnd = DateTime.Parse(reply.Auction.AuctionEnd),
				ReservcePrice = reply.Auction.ReservePrice,
				Seller = reply.Auction.Seller,
			};
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Could not call gRPC Server");
			return null;
		}
	}
}
