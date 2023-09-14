namespace AuthorizationServer.OpenIddict.Application.Features.Commands.Auth.TFAs.Deactivate;

public class DeactivateTFACommandRequest : IRequest<ResponseModel<NoContentModel>>
{
    public string Code { get; set; }
}
