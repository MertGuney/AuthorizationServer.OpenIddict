namespace AuthorizationServer.OpenIddict.Application.Features.Commands.Users.ChangeEmail;

public class ChangeEmailCommandRequest : IRequest<ResponseModel<NoContentModel>>
{
    public string Code { get; set; }
    public string NewEmail { get; set; }
}