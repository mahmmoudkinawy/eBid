namespace Search.Api.Consumers;
public sealed class AuctionDeletedConsumer : IConsumer<AuctionDeleted>
{
    public async Task Consume(ConsumeContext<AuctionDeleted> context) =>
        await DB.DeleteAsync<ItemModel>(context.Message.Id);
}
