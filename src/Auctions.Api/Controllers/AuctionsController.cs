namespace Auctions.Api.Controllers;

[Route("api/auctions")]
[ApiController]
public sealed class AuctionsController : ControllerBase
{
    private readonly AuctionsDbContext _context;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;

    public AuctionsController(
        AuctionsDbContext context,
        IPublishEndpoint publishEndpoint,
        IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
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
    [Authorize]
    public async Task<IActionResult> CreateAuction(
        [FromBody] CreateAuctionRequest request)
    {
        var auction = _mapper.Map<AuctionEntity>(request);

        auction.Seller = User.Identity.Name;

        _context.Auctions.Add(auction);

        var auctionToReturn = _mapper.Map<AuctionResponse>(auction);

        await _publishEndpoint.Publish(_mapper.Map<AuctionCreated>(auctionToReturn));

        var result = await _context.SaveChangesAsync();

        return result > 0 ?
            CreatedAtAction(nameof(GetAuctionById), new { auctionId = auction.Id }, auctionToReturn)
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

        if (auction.Seller != User.Identity.Name)
        {
            return Forbid();
        }

        auction.Item.Make = request.Make ?? auction.Item.Make;
        auction.Item.Model = request.Model ?? auction.Item.Model;
        auction.Item.Color = request.Color ?? auction.Item.Color;
        auction.Item.Mileage = request.Mileage ?? auction.Item.Mileage;
        auction.Item.Year = request.Year ?? auction.Item.Year;

        await _publishEndpoint.Publish(_mapper.Map<AuctionUpdated>(auction));

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

        if (auction.Seller != User.Identity.Name)
        {
            return Forbid();
        }

        _context.Auctions.Remove(auction);

        await _publishEndpoint.Publish(new AuctionDeleted { Id = auctionId });

        var result = await _context.SaveChangesAsync();

        return result > 0 ?
            NoContent()
            :
            BadRequest("Problem removing the item to db");
    }

}
