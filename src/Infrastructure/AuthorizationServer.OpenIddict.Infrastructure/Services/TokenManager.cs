namespace AuthorizationServer.OpenIddict.Infrastructure.Services;

public class TokenManager : ITokenManager
{
    private readonly IClientManager _clientManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TokenManager(IClientManager clientManager, IHttpContextAccessor httpContextAccessor)
    {
        _clientManager = clientManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<ClaimsPrincipal> GetClaimsPrincipalAsync()
    {
        var openIddictRequest = _httpContextAccessor.HttpContext.GetOpenIddictServerRequest();

        if (openIddictRequest?.IsClientCredentialsGrantType() is not null)
        {
            var identity = new ClaimsIdentity(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

            var clientInfo = await _clientManager.GetClientInfoAsync(openIddictRequest.ClientId);

            identity.AddClaim(Claims.Name, clientInfo.DisplayName);
            identity.AddClaim(Claims.Subject, clientInfo.ClientId);
            identity.AddClaim("custom-claim", "custom-claim-value");
            identity.AddClaim(JwtRegisteredClaimNames.Aud, "Example-OpenIddict");

            var claimsPrincipal = new ClaimsPrincipal(identity);
            claimsPrincipal.SetScopes(openIddictRequest.GetScopes());

            return claimsPrincipal;
        }
        throw new NotImplementedException("The specified grant type is not implemented.");
    }
}
