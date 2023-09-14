namespace AuthorizationServer.OpenIddict.Application.Contracts.Requests.Clients;

public class CreateClientRequest
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string DisplayName { get; set; }
}
