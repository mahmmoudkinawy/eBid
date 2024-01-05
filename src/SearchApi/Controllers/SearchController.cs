namespace SearchApi.Controllers;

[Route("api/search")]
[ApiController]
public sealed class SearchController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ItemModel>>> SearchAuctionItems(
        [FromQuery] AuctionItemParams auctionItemParams)
    {
        var query = DB.PagedSearch<ItemModel, ItemModel>();

        if (!string.IsNullOrEmpty(auctionItemParams.SearchTerm))
        {
            query.Match(Search.Full, auctionItemParams.SearchTerm).SortByTextScore();
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

        query = auctionItemParams.FilterBy switch
        {
            "finished" => query.Match(ai => ai.AuctionEnd < DateTime.UtcNow),
            "endingSoon" => query.Match(ai => ai.AuctionEnd < DateTime.UtcNow.AddHours(6) && ai.AuctionEnd > DateTime.UtcNow),
            _ => query.Match(ai => ai.AuctionEnd > DateTime.UtcNow)
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
