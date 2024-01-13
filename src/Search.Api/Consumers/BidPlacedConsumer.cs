namespace Search.Api.Consumers;
public sealed class BidPlacedConsumer : IConsumer<BidPlaced>
{
    public async Task Consume(ConsumeContext<BidPlaced> context)
    {
        var auction = await DB.Find<ItemModel>().OneAsync(context.Message.AuctionId);

        if (context.Message.BidStatus.Contains("Accepted") &&
            context.Message.Amount > auction.CurrentHighBid)
        {
            auction.CurrentHighBid = context.Message.Amount;
            await auction.SaveAsync();
        }
    }
}
