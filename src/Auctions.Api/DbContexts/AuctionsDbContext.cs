namespace Auctions.Api.DbContexts;
public sealed class AuctionsDbContext : DbContext
{
    public AuctionsDbContext(DbContextOptions<AuctionsDbContext> options) : base(options) { }

    public DbSet<AuctionEntity> Auctions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ItemEntity>()
            .HasOne(i => i.Auction)
            .WithOne(a => a.Item)
            .HasForeignKey<ItemEntity>(i => i.AuctionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
