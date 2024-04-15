namespace Notifications.Api.Consumers;

public sealed class BidPlacedConsumer : IConsumer<BidPlaced>
{
	private readonly IHubContext<NotificationHub> _hubContext;

	public BidPlacedConsumer(IHubContext<NotificationHub> hubContext)
	{
		_hubContext = hubContext;
	}

	public async Task Consume(ConsumeContext<BidPlaced> context) => await _hubContext.Clients.All.SendAsync("BidPlaced", context.Message);
}
