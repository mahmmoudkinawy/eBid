namespace SearchApi.DatabaseInitializer;
public sealed class DatabaseSeeder
{
    public static async Task DbInitializer(WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(nameof(app));

        await DB.InitAsync(
            app.Configuration.GetValue<string>("MongoDbSettings:DatabaseName"),
            MongoClientSettings.FromConnectionString(app.Configuration.GetValue<string>("MongoDbSettings:ConnectionString")));

        await DB.Index<ItemModel>()
            .Key(i => i.Make, KeyType.Text)
            .Key(i => i.Model, KeyType.Text)
            .Key(i => i.Color, KeyType.Text)
            .CreateAsync();

        var auctionItemsToSeed = await File.ReadAllTextAsync("DatabaseInitializer/Seed.json");

        var items = JsonSerializer.Deserialize<IReadOnlyList<ItemModel>>(auctionItemsToSeed, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (items.Count > 0)
        {
            await DB.SaveAsync(items);
        }
    }
}
