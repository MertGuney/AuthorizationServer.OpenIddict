namespace AuthorizationServer.OpenIddict.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IHostApplicationBuilder AddInfrastructureLayer(this IHostApplicationBuilder builder)
    {
        builder.Services.AddServices();

        return builder;
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITFAService, TFAService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<IMailService, MailService>()
                .AddScoped<ITokenManager, TokenManager>()
                .AddScoped<IClientManager, ClientManager>()
                .AddScoped<IQRCodeService, QRCodeService>();
    }
}