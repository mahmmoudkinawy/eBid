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
    }
}
