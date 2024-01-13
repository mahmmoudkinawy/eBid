namespace eBidPubSubUtilities.Contracts;

/// <summary>
/// Represents an event indicating that an auction has finished.
/// </summary>
public sealed class AuctionFinished
{
    /// <summary>
    /// Gets or sets a value indicating whether the item was sold in the auction.
    /// </summary>
    public bool ItemSold { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the finished auction.
    /// </summary>
    public string AuctionId { get; set; }

    /// <summary>
    /// Gets or sets the name of the winning bidder in the auction.
    /// </summary>
    public string Winner { get; set; }

    /// <summary>
    /// Gets or sets the name of the seller who initiated the auction.
    /// </summary>
    public string Seller { get; set; }

    /// <summary>
    /// Gets or sets the amount at which the item was sold in the auction. 
    /// This property is nullable to handle cases where the item may not be sold.
    /// </summary>
    public int? Amount { get; set; }
}
