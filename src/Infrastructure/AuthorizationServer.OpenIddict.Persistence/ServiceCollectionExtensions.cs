namespace AuthorizationServer.OpenIddict.Persistence;

public static class ServiceCollectionExtensions
{
    public static void AddPersistenceLayer(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext(builder.Configuration);
        builder.Services.AddIdentity();
        builder.Services.AddServices();
        builder.Services.AddRepositories();
        builder.Services.AddEventDispatcher();
    }

    private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        NpgsqlConnectionStringBuilder connectionStringBuilder = new(configuration.GetConnectionString("DefaultConnection"))
        {
            Pooling = true,
            MinPoolSize = 5,
            MaxPoolSize = 100,
            ConnectionIdleLifetime = 300,
            ConnectionPruningInterval = 10
        };

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionStringBuilder.ConnectionString, npgsqlOpts =>
            {
                npgsqlOpts.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
                npgsqlOpts.CommandTimeout(30);
                npgsqlOpts.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
            options.UseOpenIddict();
        });
    }

    private static void AddEventDispatcher(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserCodesRepository, UserCodesRepository>();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICodeService, CodeService>();
    }

    private static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(opts =>
        {
            opts.User.RequireUniqueEmail = true;

            opts.Password.RequiredLength = 8;
            opts.Password.RequireDigit = false;

            opts.Lockout.AllowedForNewUsers = true;
            opts.Lockout.MaxFailedAccessAttempts = 3;
            opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        }).AddPasswordValidator<IdentityPasswordValidator>()
          .AddUserValidator<IdentityUserValidator>()
          .AddEntityFrameworkStores<AppDbContext>()
          .AddDefaultTokenProviders();
    }

    public static void AddDevelopmentIdentity(this IServiceCollection services)
    {
        services.AddOpenIddict()
                .AddCore(opts =>
                {
                    opts.UseEntityFrameworkCore()
                        .UseDbContext<AppDbContext>();
                }).AddServer(opts =>
                {
                    opts.SetTokenEndpointUris("/connect/token")
                        .SetAuthorizationEndpointUris("/connect/authorize")
                        .SetUserinfoEndpointUris("connect/userinfo");

                    opts.AllowPasswordFlow()
                        .AllowAuthorizationCodeFlow()
                        .AllowClientCredentialsFlow()
                        .RequireProofKeyForCodeExchange();

                    opts.AddEphemeralSigningKey()
                        .AddEphemeralEncryptionKey()
                        .DisableAccessTokenEncryption(); // for jwt.io

                    opts.UseAspNetCore()
                        .EnableTokenEndpointPassthrough()
                        .EnableAuthorizationEndpointPassthrough()
                        .EnableUserinfoEndpointPassthrough()
                        .EnableLogoutEndpointPassthrough();

                    opts.RegisterScopes("read", "write");
                });
    }

    public static void AddProductionIdentity(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
    }
}