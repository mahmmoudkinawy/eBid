namespace eBidPubSubUtilities.Contracts;

/// <summary>
/// Represents an auction deleted event with details about the deleted auction.
/// </summary>
public sealed class AuctionDeleted
{
    /// <summary>
    /// Gets or sets the unique identifier for the deleted auction.
    /// </summary>
    public Guid Id { get; set; }
}
