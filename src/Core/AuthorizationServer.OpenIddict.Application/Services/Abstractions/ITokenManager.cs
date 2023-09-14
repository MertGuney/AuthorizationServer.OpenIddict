namespace AuthorizationServer.OpenIddict.Application.Services.Abstractions;

public interface ITokenManager
{
    ValueTask<ClaimsPrincipal> GetClaimsPrincipalAsync();
}
