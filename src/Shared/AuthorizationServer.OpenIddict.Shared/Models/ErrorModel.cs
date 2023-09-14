using AuthorizationServer.OpenIddict.Shared.Enums;

namespace AuthorizationServer.OpenIddict.Shared.Models;

public class ErrorModel
{
    public int Code { get; set; }
    public string Type { get; set; }
    public string Message { get; set; }
    public string TransactionId { get; set; }

    public ErrorModel(FailureTypes code, string message, string transactionId)
    {
        Code = (int)code;
        Message = message;
        Type = Enum.GetName(code);
        TransactionId = transactionId;
    }
}
