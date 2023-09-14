namespace AuthorizationServer.OpenIddict.Application.Services.Abstractions;

public interface IClientManager
{
    ValueTask CreateAsync(CreateClientRequest request);
    ValueTask<ClientInfoResponse> GetClientInfoAsync(string clientId);
}
