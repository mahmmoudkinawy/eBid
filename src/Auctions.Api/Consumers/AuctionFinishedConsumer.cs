namespace Auctions.Api.Consumers;
public sealed class AuctionFinishedConsumer : IConsumer<AuctionFinished>
{
    private readonly AuctionsDbContext _dbContext;

    public AuctionFinishedConsumer(AuctionsDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    public async Task Consume(ConsumeContext<AuctionFinished> context)
    {
        var auction = await _dbContext.Auctions.FindAsync(context.Message.AuctionId);

        if (context.Message.ItemSold)
        {
            auction.Winner = context.Message.Winner;
            auction.SoldAmount = context.Message.Amount;
        }

        auction.Status = auction.SoldAmount > auction.ReservePrice ? StatusEnum.Finished : StatusEnum.ReserveNotMet;

        await _dbContext.SaveChangesAsync();
    }
}
