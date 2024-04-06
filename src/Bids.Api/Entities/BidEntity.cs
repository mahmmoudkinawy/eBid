namespace Bids.Api.Entities;

public sealed class BidEntity : Entity
{
	public string AuctionId { get; set; }
	public string Bidder { get; set; }
	public DateTime BidTime { get; set; } = DateTime.UtcNow;
	public decimal Amount { get; set; }
	public BidStatusEnum BidStatus { get; set; }
}
