namespace AuthorizationServer.OpenIddict.Application.Features.Commands.Auth.Codes.SendRegisterCode;

public class SendRegisterCodeCommandRequestValidator : AbstractValidator<SendRegisterCodeCommandRequest>
{
    public SendRegisterCodeCommandRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().Must(MailRegex.IsValidEmail);
    }
}
