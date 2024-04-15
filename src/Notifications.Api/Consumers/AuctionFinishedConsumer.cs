namespace Notifications.Api.Consumers;

public sealed class AuctionFinishedConsumer : IConsumer<AuctionFinished>
{
	private readonly IHubContext<NotificationHub> _hubContext;

	public AuctionFinishedConsumer(IHubContext<NotificationHub> hubContext)
	{
		_hubContext = hubContext;
	}

	public async Task Consume(ConsumeContext<AuctionFinished> context) => await _hubContext.Clients.All.SendAsync("AuctionFinished", context.Message);
}
