var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AuctionsDbContext>(opts => opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMassTransit(opts =>
{
	opts.AddEntityFrameworkOutbox<AuctionsDbContext>(o =>
	{
		o.QueryDelay = TimeSpan.FromSeconds(15);

		o.UsePostgres();
		o.UseBusOutbox();
	});

	opts.AddConsumersFromNamespaceContaining<AuctionFinishedConsumer>();

	opts.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("auction", false));

	opts.UsingRabbitMq(
		(context, cfg) =>
		{
			cfg.Host(
				builder.Configuration["RabbitMq:Host"],
				"/",
				host =>
				{
					host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
					host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
				}
			);

			cfg.ConfigureEndpoints(context);
		}
	);
});

builder
	.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.Authority = builder.Configuration["AuctionsIdpUrl"];
		options.RequireHttpsMetadata = false;
		options.TokenValidationParameters.ValidateAudience = false;
		options.TokenValidationParameters.NameClaimType = "username";
	});

builder.Services.AddGrpc();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<GrpcAuctionService>();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AuctionsDbContext>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

await dbContext.Database.MigrateAsync();
await Seed.SeedAsync(dbContext);

app.Run();
