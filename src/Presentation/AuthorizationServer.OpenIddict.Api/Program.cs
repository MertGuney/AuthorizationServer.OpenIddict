var corsPolicyName = "IdentityServerCorsPolicy";

var builder = WebApplication.CreateBuilder(args);

var redis = await ConnectionMultiplexer.ConnectAsync(builder.Configuration.GetConnectionString("Redis"));
builder.Services.AddSingleton<IConnectionMultiplexer>(redis);

builder.Services.AddDataProtection()
                .PersistKeysToStackExchangeRedis(redis, "DataProtection-Keys")
                .SetApplicationName("AuthorizationServer");

builder.Services.ConfigureCors(corsPolicyName);

builder.AddApplicationLayer();

builder.AddPersistenceLayer();

builder.AddInfrastructureLayer();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDevelopmentIdentity();
}
else
{
    builder.Services.AddProductionIdentity(builder.Configuration, builder.Environment);
}

builder.Services.ConfigureVersioning();

builder.Services.ConfigureExternalAuth(builder.Configuration);

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(opts =>
    {
        opts.SuppressModelStateInvalidFilter = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDevelopmentDatabaseAsync();
}

app.UseHttpsRedirection();

app.UseCors(corsPolicyName);

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();