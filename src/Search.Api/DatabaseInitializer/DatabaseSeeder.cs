using Search.Api.Models;

namespace Search.Api.DatabaseInitializer;
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

        if (await DB.CountAsync<ItemModel>() == 0)
        {
            var auctions = await File.ReadAllTextAsync("DatabaseInitializer/auctions.json");

            var items = JsonSerializer.Deserialize<IReadOnlyList<ItemModel>>(auctions);

            await items.SaveAsync();
        }
    }
}
