namespace Auctions.Api.Consumers;
public sealed class BidPlacedConsumer : IConsumer<BidPlaced>
{
    private readonly AuctionsDbContext _dbContext;

    public BidPlacedConsumer(AuctionsDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    public async Task Consume(ConsumeContext<BidPlaced> context)
    {
        var auction = await _dbContext.Auctions.FindAsync(Guid.Parse(context.Message.AuctionId));

        if (
            auction.CurrentHighBid == null ||
            context.Message.BidStatus.Contains("Accepted") &&
            context.Message.Amount > auction.CurrentHighBid)
        {
            auction.CurrentHighBid = context.Message.Amount;
            await _dbContext.SaveChangesAsync();
        }
    }
}
