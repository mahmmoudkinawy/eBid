namespace eBidPubSubUtilities.Contracts;

/// <summary>
/// Represents an auction created event with details about the auction item.
/// </summary>
public sealed class AuctionCreated
{
    /// <summary>
    /// Gets or sets the unique identifier for the auction.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the reserve price for the auction item.
    /// </summary>
    public int ReservePrice { get; set; }

    /// <summary>
    /// Gets or sets the seller's name for the auction item.
    /// </summary>
    public string Seller { get; set; }

    /// <summary>
    /// Gets or sets the winner's name for the auction item.
    /// </summary>
    public string Winner { get; set; }

    /// <summary>
    /// Gets or sets the amount at which the item was sold.
    /// </summary>
    public int SoldAmount { get; set; }

    /// <summary>
    /// Gets or sets the current highest bid for the auction item.
    /// </summary>
    public int CurrentHighBid { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the auction was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the auction was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the auction is scheduled to end.
    /// </summary>
    public DateTime AuctionEnd { get; set; }

    /// <summary>
    /// Gets or sets the status of the auction (e.g., ongoing, completed).
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the make of the auction item (e.g., car manufacturer).
    /// </summary>
    public string Make { get; set; }

    /// <summary>
    /// Gets or sets the model of the auction item.
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// Gets or sets the year of the auction item.
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Gets or sets the color of the auction item.
    /// </summary>
    public string Color { get; set; }

    /// <summary>
    /// Gets or sets the mileage of the auction item.
    /// </summary>
    public int Mileage { get; set; }

    /// <summary>
    /// Gets or sets the URL of the image representing the auction item.
    /// </summary>
    public string ImageUrl { get; set; }
}
