namespace Bids.Api.Consumers;

public sealed class AuctionCreatedConsumer : IConsumer<AuctionCreated>
{
	public async Task Consume(ConsumeContext<AuctionCreated> context)
	{
		var auction = new AuctionEntity
		{
			ID = context.Message.Id.ToString(),
			AuctionEnd = context.Message.AuctionEnd,
			Seller = context.Message.Seller,
			ReservcePrice = context.Message.ReservePrice
		};

		await auction.SaveAsync();
	}
}
