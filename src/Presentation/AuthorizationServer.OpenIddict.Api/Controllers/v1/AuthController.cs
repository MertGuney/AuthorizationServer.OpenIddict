using AuthorizationServer.OpenIddict.Application.Contracts.Requests.Clients;

namespace AuthorizationServer.OpenIddict.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth/[action]")]
public class AuthController : BaseController
{
    private readonly ITokenManager _tokenManager;
    private readonly IClientManager _clientManager;
    private readonly SignInManager<User> _signInManager;

    public AuthController(IMediator mediator, ITokenManager tokenManager, IClientManager clientManager, SignInManager<User> signInManager) : base(mediator)
    {
        _tokenManager = tokenManager;
        _clientManager = clientManager;
        _signInManager = signInManager;
    }

    [HttpPost("~/connect/token")]
    public async Task<IActionResult> Login()
    {
        var claimsPrincipal = await _tokenManager.GetClaimsPrincipalAsync();

        return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient(CreateClientRequest request)
    {
        await _clientManager.CreateAsync(request);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterCommandRequest request)
        => ActionResultInstance(await _mediator.Send(request));

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordCommandRequest request)
        => ActionResultInstance(await _mediator.Send(request));

    [HttpPost("{provider}")]
    public IActionResult ExternalLogin(string provider, string returnUrl = null)
    {
        var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Auth", new { returnUrl });
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

        return Challenge(properties, provider);
    }

    [HttpGet]
    public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null)
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();

        var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false, true);

        return Ok();
    }
}
