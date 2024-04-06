namespace Bids.Api.Entities;

public sealed class AuctionEntity : Entity
{
	public DateTime AuctionEnd { get; set; }
	public string Seller { get; set; }
	public int ReservcePrice { get; set; }
	public bool Finished { get; set; }
}
