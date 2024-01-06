namespace SearchApi.Consumers;
public sealed class AuctionCreatedConsumer : IConsumer<AuctionCreated>
{
    private readonly IMapper _mapper;

    public AuctionCreatedConsumer(IMapper mapper)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task Consume(ConsumeContext<AuctionCreated> context)
    {
        Console.WriteLine($"--> Consuming auction with Id: {context.Message.Id}");

        var item = _mapper.Map<ItemModel>(context.Message);

        await item.SaveAsync();
    }
}
