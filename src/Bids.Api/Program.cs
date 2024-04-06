var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMassTransit(opts =>
{
	opts.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();

	opts.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("bids", false));

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await DB.InitAsync(
	builder.Configuration.GetValue<string>("ConnectionStrings:DatabaseName")!,
	MongoClientSettings.FromConnectionString(builder.Configuration.GetConnectionString("DefaultConnection"))
);

app.Run();
