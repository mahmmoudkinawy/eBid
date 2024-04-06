namespace Bids.Api.Controllers;

[Route("api/bids")]
[ApiController]
public sealed class BidsController(IMapper mapper, IPublishEndpoint publishEndpoint) : ControllerBase
{
	[HttpPost]
	[Authorize]
	public async Task<IActionResult> PlaceBid([FromQuery] string auctionId, [FromQuery] decimal amount)
	{
		var auction = await DB.Find<AuctionEntity>().OneAsync(auctionId);

		if (auction == null)
		{
			// TODO: check with auction service if that auction exists
			return NotFound();
		}

		if (auction.Seller == User.Identity.Name)
		{
			return BadRequest("You cannot bid on yor own auction");
		}

		var bid = new BidEntity
		{
			AuctionId = auctionId,
			Amount = amount,
			BidTime = DateTime.UtcNow,
			Bidder = User.Identity.Name
		};

		if (auction.AuctionEnd < DateTime.UtcNow)
		{
			bid.BidStatus = BidStatusEnum.Finished;
		}
		else
		{
			var highBid = await DB.Find<BidEntity>().Match(b => b.AuctionId == auctionId).Sort(b => b.Descending(_ => _.Amount)).ExecuteFirstAsync();

			if (highBid != null && amount > highBid.Amount || highBid == null)
			{
				bid.BidStatus = amount > auction.ReservcePrice ? BidStatusEnum.Accepted : BidStatusEnum.AcceptedBellowReserve;
			}

			if (highBid != null && bid.Amount <= highBid.Amount)
			{
				bid.BidStatus = BidStatusEnum.TooLow;
			}
		}

		await publishEndpoint.Publish(mapper.Map<BidPlaced>(bid));
		await DB.SaveAsync(bid);

		return Ok(mapper.Map<BidResponse>(bid));
	}

	[HttpGet("{auctionId}")]
	public async Task<IActionResult> GetBidsForAuction([FromRoute] string auctionId)
	{
		var bids = await DB.Find<BidEntity>().Match(a => a.AuctionId == auctionId).Sort(b => b.Descending(_ => _.BidTime)).ExecuteAsync();

		return Ok(bids.Select(mapper.Map<BidResponse>));
	}
}
