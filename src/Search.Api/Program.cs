var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMassTransit(opts =>
{
    opts.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();

    opts.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));

    opts.UsingRabbitMq((context, cfg) =>
    {
        cfg.ReceiveEndpoint("search-auction-created", o =>
        {
            o.UseMessageRetry(r => r.Interval(5, 5));

            o.ConfigureConsumer<AuctionCreatedConsumer>(context);
        });

        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

using var scope = app.Services.CreateScope();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
try
{
    await DatabaseSeeder.DbInitializer(app);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error occured while database setup");
}

app.Run();
