namespace Search.Api.Consumers;
public sealed class AuctionUpdatedConsumer : IConsumer<AuctionUpdated>
{
    private readonly IMapper _mapper;

    public AuctionUpdatedConsumer(IMapper mapper)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    public async Task Consume(ConsumeContext<AuctionUpdated> context)
    {
        Console.WriteLine($"--> Consuming auction with Id: {context.Message.Id}");

        var item = _mapper.Map<ItemModel>(context.Message);

        await DB.Update<ItemModel>()
            .MatchID(item.ID)
            .Modify(a => a.Make, item.Make)
            .Modify(a => a.Model, item.Model)
            .Modify(a => a.Year, item.Year)
            .Modify(a => a.Color, item.Color)
            .Modify(a => a.Mileage, item.Mileage)
            .ExecuteAsync();
    }
}
