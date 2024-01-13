namespace Search.Api.Controllers;

[Route("api/search")]
[ApiController]
public sealed class SearchController : ControllerBase
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public SearchController(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ItemModel>>> SearchAuctionItems(
        [FromQuery] AuctionItemParams auctionItemParams)
    {
        var query = DB.PagedSearch<ItemModel, ItemModel>();

        if (!string.IsNullOrEmpty(auctionItemParams.SearchTerm))
        {
            query.Match(MongoDB.Entities.Search.Full, auctionItemParams.SearchTerm).SortByTextScore();
        }

        if (!string.IsNullOrEmpty(auctionItemParams.Seller))
        {
            query.Match(ai => ai.Seller.Contains(auctionItemParams.Seller));
        }

        if (!string.IsNullOrEmpty(auctionItemParams.Winner))
        {
            query.Match(ai => ai.Seller.Contains(auctionItemParams.Winner));
        }

        query = auctionItemParams.OrderBy switch
        {
            "make" => query.Sort(ai => ai.Ascending(i => i.Make)),
            "new" => query.Sort(ai => ai.Descending(i => i.CreatedAt)),
            _ => query.Sort(ai => ai.Ascending(i => i.AuctionEnd))
        };

        var utcNow = _dateTimeProvider.UtcNow;

        query = auctionItemParams.FilterBy switch
        {
            "finished" => query.Match(ai => ai.AuctionEnd < utcNow),
            "endingSoon" => query.Match(ai => ai.AuctionEnd < DateTime.UtcNow.AddHours(6) && ai.AuctionEnd > utcNow),
            _ => query.Match(ai => ai.AuctionEnd > utcNow)
        };

        query
            .PageNumber(auctionItemParams.PageNumber)
            .PageSize(auctionItemParams.PageSize);

        var result = await query.ExecuteAsync();

        return Ok(new
        {
            results = result.Results,
            pageCount = result.PageCount,
            totalCount = result.TotalCount
        });
    }
}
