namespace AuthorizationServer.OpenIddict.Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationLayer(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMediator();
        builder.Services.AddServices();
        builder.Services.AddAutoMapper();
        builder.Services.AddValidators();
        builder.Services.AddFilterAttributes();
        builder.Services.AddExceptionHandling();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddLoggingPipelineBehaviours();
        builder.Services.AddValidationPipelineBehaviours();
        builder.Services.AddPerformancePipelineBehaviours();
        builder.Services.AddExceptionHandlerPipelineBehaviours();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
    }

    private static void AddExceptionHandling(this IServiceCollection services)
    {
        services.AddTransient<ExceptionHandlingMiddleware>();
    }

    private static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }

    private static void AddFilterAttributes(this IServiceCollection services)
    {
        services.AddScoped<UserNotFoundFilterAttribute>();
    }

    private static void AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
    }

    private static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }

    private static void AddValidationPipelineBehaviours(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }

    private static void AddLoggingPipelineBehaviours(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    }

    private static void AddPerformancePipelineBehaviours(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
    }

    private static void AddExceptionHandlerPipelineBehaviours(this IServiceCollection services)
    {
        services.AddTransient(typeof(IRequestExceptionHandler<,,>), typeof(ExceptionHandlingBehavior<,,>));
    }
}