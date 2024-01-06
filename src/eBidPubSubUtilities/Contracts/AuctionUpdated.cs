namespace eBidPubSubUtilities.Contracts;

/// <summary>
/// Represents an auction updated event with details about the modified auction.
/// </summary>
public sealed class AuctionUpdated
{
    /// <summary>
    /// Gets or sets the unique identifier for the updated auction.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the updated make of the auction item (e.g., car manufacturer).
    /// </summary>
    public string Make { get; set; }

    /// <summary>
    /// Gets or sets the updated model of the auction item.
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// Gets or sets the updated year of the auction item.
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Gets or sets the updated color of the auction item.
    /// </summary>
    public string Color { get; set; }

    /// <summary>
    /// Gets or sets the updated mileage of the auction item.
    /// </summary>
    public int Mileage { get; set; }
}