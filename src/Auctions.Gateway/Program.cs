var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder
	.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.Authority = builder.Configuration["AuctionsIdpUrl"];
		options.RequireHttpsMetadata = false;
		options.TokenValidationParameters.ValidateAudience = false;
		options.TokenValidationParameters.NameClaimType = "username";
	});

builder.Services.AddCors(opts =>
{
	opts.AddPolicy(
		"clientPolicy",
		policy =>
		{
			policy.AllowAnyHeader().AllowCredentials().AllowAnyMethod().WithOrigins(builder.Configuration.GetValue<string>("ClientApp"));
		}
	);
});

var app = builder.Build();

app.UseCors();

app.MapReverseProxy();

app.UseAuthentication();

app.UseAuthorization();

app.Run();
