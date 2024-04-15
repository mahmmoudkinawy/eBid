namespace Notifications.Api.Consumers;

public sealed class AuctionCreatedConsumer : IConsumer<AuctionCreated>
{
	private readonly IHubContext<NotificationHub> _hubContext;

	public AuctionCreatedConsumer(IHubContext<NotificationHub> hubContext)
	{
		_hubContext = hubContext;
	}

	public async Task Consume(ConsumeContext<AuctionCreated> context) => await _hubContext.Clients.All.SendAsync("AuctionCreated", context.Message);
}
