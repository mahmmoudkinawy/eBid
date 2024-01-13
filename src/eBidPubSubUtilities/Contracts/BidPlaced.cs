namespace eBidPubSubUtilities.Contracts;

/// <summary>
/// Represents an event indicating that a bid has been placed in an auction.
/// </summary>
public sealed class BidPlaced
{
    /// <summary>
    /// Gets or sets the unique identifier of the bid.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the auction where the bid is placed.
    /// </summary>
    public string AuctionId { get; set; }

    /// <summary>
    /// Gets or sets the name of the bidder placing the bid.
    /// </summary>
    public string Bidder { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the bid was placed.
    /// </summary>
    public DateTime BidTime { get; set; }

    /// <summary>
    /// Gets or sets the amount of the bid.
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Gets or sets the status of the bid (e.g., accepted, rejected, pending).
    /// </summary>
    public string BidStatus { get; set; }
}
