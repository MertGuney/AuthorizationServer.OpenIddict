namespace AuthorizationServer.OpenIddict.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        services.AddServices();
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
