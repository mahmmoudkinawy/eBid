namespace Auctions.Api.Controllers;

[Route("api/auctions")]
[ApiController]
public sealed class AuctionsController : ControllerBase
{
    private readonly AuctionsDbContext _context;
    private readonly IMapper _mapper;

    public AuctionsController(AuctionsDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AuctionResponse>>> GetAuctions()
    {
        var auctions = await _context.Auctions
            .Include(a => a.Item)
            .OrderBy(i => i.Item.Make)
            .ToListAsync();

        return Ok(_mapper.Map<IReadOnlyList<AuctionResponse>>(auctions));
    }

    [HttpGet("{auctionId}", Name = "GetAuctionById")]
    public async Task<ActionResult<AuctionResponse>> GetAuctionById(
        [FromRoute] Guid auctionId)
    {
        var auction = await _context.Auctions
            .Include(a => a.Item)
            .FirstOrDefaultAsync(a => a.Id == auctionId);

        return auction != null
            ? Ok(_mapper.Map<AuctionResponse>(auction))
            : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuction(
        [FromBody] CreateAuctionRequest request)
    {
        var auction = _mapper.Map<AuctionEntity>(request);

        // TODO: set current username

        auction.Seller = "md kianwy";

        _context.Auctions.Add(auction);

        var result = await _context.SaveChangesAsync();

        return result > 0 ?
            CreatedAtAction(nameof(GetAuctionById), new { auctionId = auction.Id }, _mapper.Map<AuctionResponse>(auction))
            :
            BadRequest("Problem adding the item to db");
    }

    [HttpPut("{auctionId}")]
    public async Task<IActionResult> UpdateAuction(
        [FromRoute] Guid auctionId,
        [FromBody] UpdateAuctionRequest request)
    {
        var auction = await _context.Auctions
            .Include(a => a.Item)
            .FirstOrDefaultAsync(a => a.Id == auctionId);

        if (auction == null)
        {
            return NotFound();
        }

        // TODO: check seller == username

        auction.Item.Make = request.Make ?? auction.Item.Make;
        auction.Item.Model = request.Model ?? auction.Item.Model;
        auction.Item.Color = request.Color ?? auction.Item.Color;
        auction.Item.Mileage = request.Mileage ?? auction.Item.Mileage;
        auction.Item.Year = request.Year ?? auction.Item.Year;

        var result = await _context.SaveChangesAsync();

        return result > 0 ?
            NoContent()
            :
            BadRequest("Problem updating the item to db");
    }

    [HttpDelete("{auctionId}")]
    public async Task<IActionResult> DeleteAuction(
        [FromRoute] Guid auctionId)
    {
        var auction = await _context.Auctions
            .Include(a => a.Item)
            .FirstOrDefaultAsync(a => a.Id == auctionId);

        if (auction == null)
        {
            return NotFound();
        }

        _context.Auctions.Remove(auction);

        var result = await _context.SaveChangesAsync();

        return result > 0 ?
            NoContent()
            :
            BadRequest("Problem removing the item to db");
    }

}
