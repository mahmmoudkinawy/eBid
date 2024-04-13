namespace Bids.Api.Services;

public sealed class CheckAuctionFinished : BackgroundService
{
	private readonly IServiceProvider _services;

	public CheckAuctionFinished(IServiceProvider services)
	{
		_services = services;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			await CheckAuctionsAsync(stoppingToken);

			await Task.Delay(5000, stoppingToken);
		}
	}

	private async Task CheckAuctionsAsync(CancellationToken stoppingToken)
	{
		var finishedAuctions = await DB.Find<AuctionEntity>()
			.Match(a => a.AuctionEnd <= DateTime.UtcNow)
			.Match(a => !a.Finished)
			.ExecuteAsync(stoppingToken);

		if (finishedAuctions.Count == 0)
		{
			return;
		}

		using var scope = _services.CreateScope();
		var endpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

		foreach (var auction in finishedAuctions)
		{
			auction.Finished = true;
			await auction.SaveAsync(null, stoppingToken);

			var winningBid = await DB.Find<BidEntity>()
				.Match(b => b.AuctionId == auction.ID)
				.Match(b => b.BidStatus == BidStatusEnum.Accepted)
				.Sort(b => b.Descending(ab => ab.Amount))
				.ExecuteFirstAsync(stoppingToken);

			await endpoint.Publish(
				new AuctionFinished
				{
					AuctionId = auction.ID,
					Amount = winningBid == null ? 0 : (int)winningBid.Amount,
					Winner = winningBid?.Bidder,
					Seller = auction.Seller,
					ItemSold = winningBid != null
				},
				stoppingToken
			);
		}
	}
}
