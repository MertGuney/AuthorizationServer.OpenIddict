namespace AuthorizationServer.OpenIddict.Infrastructure.Services;

public class ClientManager : IClientManager
{
    private readonly IOpenIddictApplicationManager _openIddictApplicationManager;

    public ClientManager(IOpenIddictApplicationManager openIddictApplicationManager)
    {
        _openIddictApplicationManager = openIddictApplicationManager;
    }

    public async ValueTask CreateAsync(CreateClientRequest request)
    {
        var client = await _openIddictApplicationManager.FindByClientIdAsync(request.ClientId);
        if (client is not null)
            throw new CustomApplicationException("Exist Client");

        await _openIddictApplicationManager.CreateAsync(new()
        {
            ClientId = request.ClientId,
            ClientSecret = request.ClientSecret,
            DisplayName = request.DisplayName,
            Permissions =
            {
                Permissions.Endpoints.Token,
                Permissions.GrantTypes.ClientCredentials,
                Permissions.Prefixes.Scope + "read",
                Permissions.Prefixes.Scope + "write",
            }
        });
    }

    public async ValueTask<ClientInfoResponse> GetClientInfoAsync(string clientId)
    {
        var client = await _openIddictApplicationManager.FindByClientIdAsync(clientId)
            ?? throw new InvalidOperationException("This clientId was not found");

        var clientIdResult = await _openIddictApplicationManager.GetClientIdAsync(client)
            ?? throw new InvalidOperationException();

        var displayNameResult = await _openIddictApplicationManager.GetDisplayNameAsync(client)
            ?? throw new InvalidOperationException();

        return new ClientInfoResponse { ClientId = clientIdResult, DisplayName = displayNameResult };
    }
}
